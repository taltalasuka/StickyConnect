using System.Collections;
using Unity.Netcode;
using UnityEngine;


public class MainChessSync : NetworkBehaviour
{
    private Vector2 _leftToRightCastDir = Vector2.one;
    private Vector2 _rightToLeftCastDir = new(-1f, 1f);
    private Vector2 _originalPos = new Vector2(0f, -4.35f);
    public GamePlayNew gamePlay;
    public bool hasCallComputer;
    
    private void OnMouseDrag()
    {
        if (!gamePlay.isPlayerTurn)
        {
            return;
        }
        Vector2 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (temp.y <= 0.463f)
        {
            RaycastHit2D test = Physics2D.Raycast(temp, temp.x < 0 ? _leftToRightCastDir : _rightToLeftCastDir, Mathf.Infinity, 1 << 7);
            if (test)
            {
                transform.position = test.transform.position;
            }
        }
        else
        {
            transform.position = _originalPos;
        }
    }

    private void OnMouseUp()
    {
        if (!gamePlay.isPlayerTurn)
        {
            return;
        }
        if (!Mathf.Approximately(transform.position.x, _originalPos.x))
        {
            if (CanAdd(transform.position))
            {
                StartCoroutine(AddChess(transform.position, gamePlay.playerChess));
                gamePlay.isPlayerTurn = !gamePlay.isPlayerTurn;
                hasCallComputer = false;
                gamePlay.canCallComputer = false;
            }
            else
            {
                Debug.Log("cant");
            }
        }
        transform.position = _originalPos;
    }

    private bool CanAdd(Vector2 pos)
    {
        RaycastHit2D[] res = new RaycastHit2D[7];
        int hits = Physics2D.RaycastNonAlloc(pos, pos.x < 0 ? _leftToRightCastDir : _rightToLeftCastDir, res,
            Mathf.Infinity, 1 << 6);
        return hits < 6;
    }
    
    // [Rpc(SendTo.Server)]
    private void TestRpc(Vector2 pos, Transform ob)
    {
        // Transform temp = Instantiate(ob, pos, Quaternion.identity);
        // temp.parent = gamePlay.transform;
        // temp.GetComponent<NetworkObject>().Spawn(true);
    }
    
    private IEnumerator AddChess(Vector2 pos , Transform ob)
    {
        gamePlay.backGroundMusic.PlaySound(gamePlay.backGroundMusic.moveSound);
        TestRpc(pos, ob);
        RaycastHit2D[] res = new RaycastHit2D[6];
        int hits = Physics2D.RaycastNonAlloc(pos, pos.x < 0 ? _leftToRightCastDir : _rightToLeftCastDir, res,
            Mathf.Infinity, 1 << 6);
        for (int i = 0; i < hits; i++)
        {
            StartCoroutine(ChessBehavior(res[i].transform, pos.x < 0));
        }
        yield return new WaitForSeconds(0.7f);
        gamePlay.CheckWin();
    }

    private IEnumerator ChessBehavior(Transform tran, bool isFromLeft)
    {
        StartCoroutine(MovingAnim(0.25f, tran, tran.position + new Vector3(isFromLeft ? 0.693f : -0.693f, 0.693f, 0f)));
        yield return new WaitForSeconds(0.25f);
        tran.GetComponent<CircleCollider2D>().enabled = false;
        RaycastHit2D hit = Physics2D.Raycast(tran.position, isFromLeft ? new Vector2(1f, -1f) : new Vector2(-1f, -1f));
        if (hit)
        {
            if (tran.position.y - 0.693f - hit.transform.position.y > 0.01f) //!Mathf.Approximately(tran.position.y - 0.693f, hit.transform.position.y)
            {
                StartCoroutine(MovingAnim(0.25f, tran, hit.transform.position + new Vector3(isFromLeft ? -0.693f : 0.693f, 0.693f, 0f)));
                yield return new WaitForSeconds(0.25f);
                gamePlay.backGroundMusic.audioSource.PlayOneShot(gamePlay.backGroundMusic.dropSound);
            }
        }
        tran.GetComponent<CircleCollider2D>().enabled = true;
    }
    
    private IEnumerator MovingAnim(float dur,Transform trans, Vector2 end)
    {
        Vector2 start = trans.position;
        float elapsed = 0f;
        while (elapsed <= dur)
        {
            float t = elapsed / dur;
            trans.position = Vector2.Lerp(start, end, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        trans.position = end;
    }

    private IEnumerator ComputerMove()
    {
        hasCallComputer = true;
        yield return new WaitForSeconds(1f);
        bool hasAdd = false;
        for (int i = 0; i < 6; i++)
        {
            if (CanAdd(gamePlay.firstRightPos + new Vector2(i * 0.693f, i * 0.693f)))
            {
                RaycastHit2D hit = Physics2D.Raycast(gamePlay.firstRightPos + new Vector2(i * 0.693f, i * 0.693f), new Vector2(-0.5f, 0.5f), Mathf.Infinity, 1 << 6);
                if (hit)
                {
                    if (hit.transform.CompareTag("PlayerChess"))
                    {
                        StartCoroutine(AddChess(gamePlay.firstRightPos + new Vector2(i * 0.693f, i * 0.693f), gamePlay.computerChess));
                        hasAdd = true;
                        break;
                    }
                }       
            }
        }
        if (!hasAdd)
        {
            for (int i = 0; i < 6; i++)
            {
                if (CanAdd(gamePlay.firstLeftPos + new Vector2(-i * 0.693f, i * 0.693f)))
                {
                    RaycastHit2D hit = Physics2D.Raycast(gamePlay.firstLeftPos + new Vector2(-i * 0.693f, i * 0.693f), new Vector2(-0.5f, 0.5f), Mathf.Infinity, 1 << 6);
                    if (hit)
                    {
                        if (hit.transform.CompareTag("PlayerChess"))
                        {
                            StartCoroutine(AddChess(gamePlay.firstLeftPos + new Vector2(-i * 0.693f, i * 0.693f), gamePlay.computerChess));
                            hasAdd = true;
                            break;
                        }
                    }       
                }
            }
        }
        if (!hasAdd)
        {
            StartCoroutine(AddChess(gamePlay.firstRightPos + new Vector2(5 * 0.693f, 5 * 0.693f), gamePlay.computerChess));
        }
        yield return new WaitForSeconds(1f);
        gamePlay.isPlayerTurn = true;
    }

    private IEnumerator ComputerMoveAlt1()
    {
        yield return null;
    }
    
    private IEnumerator ComputerMoveAlt2()
    {
        yield return null;
    }
    
    private IEnumerator ComputerMoveAlt3()
    {
        yield return null;
    }
    
    private void Start()
    {
        transform.position = _originalPos;
    }
    
    private void Update()
    {
        if (!gamePlay.isDisabled)
        {
            if (gamePlay.isPlayerTurn)
            {
                gamePlay.playerTurn.SetActive(true);
                gamePlay.computerTurn.SetActive(false);
            }
            else
            {
                if (gamePlay.canCallComputer)
                {
                    if (!hasCallComputer)
                    {
                        gamePlay.computerTurn.SetActive(true);
                        gamePlay.playerTurn.SetActive(false);
                        int t;
                        t = Random.Range(0, 4);
                        if (t == 0)
                        {
                            StartCoroutine(ComputerMove());
                        } else if (t == 1)
                        {
                            StartCoroutine(ComputerMoveAlt1());
                        } else if (t == 2)
                        {
                            StartCoroutine(ComputerMoveAlt2());
                        }
                        else
                        {
                            StartCoroutine(ComputerMoveAlt3());
                        }
                    }
                }
            }
            // ChosePosChess(gamePlay.isPlayerTurn ? gamePlay.playerChess : gamePlay.computerChess);
        }
    }
}
