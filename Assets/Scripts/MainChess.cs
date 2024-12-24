using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MainChess : MonoBehaviour
{
    public GamePlay gamePlay;
    private Vector2 _position;
    private Vector2 _originalPos;
    public int posIndex;            //2x:  left       1x: right
    public bool hasCallComputer;
    
    private void Awake()
    {
        _originalPos = new Vector2(0f, -4.35f);
    }

    private IEnumerator ComputerMove()
    {
        hasCallComputer = true;
        yield return new WaitForSeconds(1f);
        bool hasAdd = false;
        for (int i = 0; i < 6; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(gamePlay.firstRightPos + new Vector2(i * 0.693f, i * 0.693f), new Vector2(-0.5f, 0.5f));
            if (hit)
            {
                if (hit.transform.CompareTag("PlayerChess"))
                {
                    RaycastHit2D[] res = new RaycastHit2D[7];
                    int hits = Physics2D.RaycastNonAlloc(gamePlay.firstRightPos + new Vector2(i * 0.693f, i * 0.693f),
                        new Vector2(-0.5f, 0.5f), res, Mathf.Infinity);
                    if (hits < 6)
                    {
                        transform.position = gamePlay.firstRightPos + new Vector2(i * 0.693f, i * 0.693f);
                        AddChess(gamePlay.computerChess, false, true, i);
                        hasAdd = true;
                        transform.position = _originalPos;
                        break;
                    }
                }
            }
        }
        if (!hasAdd)
        {
            for (int i = 0; i < 6; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(gamePlay.firstLeftPos + new Vector2(-i * 0.693f, i * 0.693f), new Vector2(0.5f, 0.5f));
                if (hit)
                {
                    if (hit.transform.CompareTag("PlayerChess"))
                    {
                        RaycastHit2D[] res = new RaycastHit2D[7];
                        int hits = Physics2D.RaycastNonAlloc(gamePlay.firstLeftPos + new Vector2(-i * 0.693f, i * 0.693f),
                            new Vector2(0.5f, 0.5f), res, Mathf.Infinity);
                        if (hits < 6)
                        {
                            transform.position = gamePlay.firstLeftPos + new Vector2(-i * 0.693f, i * 0.693f);
                            AddChess(gamePlay.computerChess, false, false, i);
                            hasAdd = true;
                            transform.position = _originalPos;
                            break;
                        }
                    }
                }
            }
        }
        if (!hasAdd)
        {
            transform.position = gamePlay.firstRightPos + new Vector2(5 * 0.693f, 5 * 0.693f);
            AddChess(gamePlay.computerChess, false, true, 5);
            transform.position = _originalPos;
        }
        yield return new WaitForSeconds(1f);
        gamePlay.isPlayerTurn = true;
    }
    
    private IEnumerator ComputerMoveAlt1()
    {
        hasCallComputer = true;
        yield return new WaitForSeconds(1f);
        bool hasAdd = false;
        for (int i = 0; i < 6; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(gamePlay.firstRightPos + new Vector2(5 * 0.693f, 5 * 0.693f) - new Vector2(i * 0.693f, i * 0.693f), new Vector2(-0.5f, 0.5f));
            if (hit)
            {
                if (hit.transform.CompareTag("PlayerChess"))
                {
                    RaycastHit2D[] res = new RaycastHit2D[7];
                    int hits = Physics2D.RaycastNonAlloc(gamePlay.firstRightPos + new Vector2(5 * 0.693f, 5 * 0.693f) - new Vector2(i * 0.693f, i * 0.693f),
                        new Vector2(-0.5f, 0.5f), res, Mathf.Infinity);
                    if (hits < 6)
                    {
                        transform.position = gamePlay.firstRightPos + new Vector2(5 * 0.693f, 5 * 0.693f) - new Vector2(i * 0.693f, i * 0.693f);
                        AddChess(gamePlay.computerChess, false, true, 5 - i);
                        hasAdd = true;
                        transform.position = _originalPos;
                        break;
                    }
                }
            }
        }
        if (!hasAdd)
        {
            for (int i = 0; i < 6; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(gamePlay.firstLeftPos + new Vector2(-5 * 0.693f, 5 * 0.693f) - new Vector2(-i * 0.693f, i * 0.693f), new Vector2(0.5f, 0.5f));
                if (hit)
                {
                    if (hit.transform.CompareTag("PlayerChess"))
                    {
                        RaycastHit2D[] res = new RaycastHit2D[7];
                        int hits = Physics2D.RaycastNonAlloc(gamePlay.firstLeftPos + new Vector2(-5 * 0.693f, 5 * 0.693f) - new Vector2(-i * 0.693f, i * 0.693f),
                            new Vector2(0.5f, 0.5f), res, Mathf.Infinity);
                        if (hits < 6)
                        {
                            transform.position = gamePlay.firstLeftPos + new Vector2(-5 * 0.693f, 5 * 0.693f) - new Vector2(-i * 0.693f, i * 0.693f);
                            AddChess(gamePlay.computerChess, false, false, 5 - i);
                            hasAdd = true;
                            transform.position = _originalPos;
                            break;
                        }
                    }
                }
            }
        }
        if (!hasAdd)
        {
            transform.position = gamePlay.firstRightPos + new Vector2(5 * 0.693f, 5 * 0.693f);
            AddChess(gamePlay.computerChess, false, true, 5);
            transform.position = _originalPos;
        }
        yield return new WaitForSeconds(1f);
        gamePlay.isPlayerTurn = true;
    }
    
    private IEnumerator ComputerMoveAlt2()
    {
        hasCallComputer = true;
        yield return new WaitForSeconds(1f);
        bool hasAdd = false;
        for (int i = 0; i < 6; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(gamePlay.firstLeftPos + new Vector2(-5 * 0.693f, 5 * 0.693f) - new Vector2(-i * 0.693f, i * 0.693f), new Vector2(0.5f, 0.5f));
            if (hit)
            {
                if (hit.transform.CompareTag("PlayerChess"))
                {
                    RaycastHit2D[] res = new RaycastHit2D[7];
                    int hits = Physics2D.RaycastNonAlloc(gamePlay.firstLeftPos + new Vector2(-5 * 0.693f, 5 * 0.693f) - new Vector2(-i * 0.693f, i * 0.693f),
                        new Vector2(0.5f, 0.5f), res, Mathf.Infinity);
                    if (hits < 6)
                    {
                        transform.position = gamePlay.firstLeftPos + new Vector2(-5 * 0.693f, 5 * 0.693f) - new Vector2(-i * 0.693f, i * 0.693f);
                        AddChess(gamePlay.computerChess, false, false, 5 - i);
                        hasAdd = true;
                        transform.position = _originalPos;
                        break;
                    }
                }
            }
        }
        if (!hasAdd)
        {
            for (int i = 0; i < 6; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(gamePlay.firstRightPos + new Vector2(5 * 0.693f, 5 * 0.693f) - new Vector2(i * 0.693f, i * 0.693f), new Vector2(-0.5f, 0.5f));
                if (hit)
                {
                    if (hit.transform.CompareTag("PlayerChess"))
                    {
                        RaycastHit2D[] res = new RaycastHit2D[7];
                        int hits = Physics2D.RaycastNonAlloc(gamePlay.firstRightPos + new Vector2(5 * 0.693f, 5 * 0.693f) - new Vector2(i * 0.693f, i * 0.693f),
                            new Vector2(-0.5f, 0.5f), res, Mathf.Infinity);
                        if (hits < 6)
                        {
                            transform.position = gamePlay.firstRightPos + new Vector2(5 * 0.693f, 5 * 0.693f) - new Vector2(i * 0.693f, i * 0.693f);
                            AddChess(gamePlay.computerChess, false, true, 5 -i);
                            hasAdd = true;
                            transform.position = _originalPos;
                            break;
                        }
                    }
                }
            }
        }
        if (!hasAdd)
        {
            transform.position = gamePlay.firstRightPos + new Vector2(5 * 0.693f, 5 * 0.693f);
            AddChess(gamePlay.computerChess, false, true, 5);
            transform.position = _originalPos;
        }
        yield return new WaitForSeconds(1f);
        gamePlay.isPlayerTurn = true;
    }
    
    private IEnumerator ComputerMoveAlt3()
    {
        hasCallComputer = true;
        yield return new WaitForSeconds(1f);
        bool hasAdd = false;
        for (int i = 0; i < 6; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(gamePlay.firstLeftPos + new Vector2(-i * 0.693f, i * 0.693f), new Vector2(0.5f, 0.5f));
            if (hit)
            {
                if (hit.transform.CompareTag("PlayerChess"))
                {
                    RaycastHit2D[] res = new RaycastHit2D[7];
                    int hits = Physics2D.RaycastNonAlloc(gamePlay.firstLeftPos + new Vector2(-i * 0.693f, i * 0.693f),
                        new Vector2(0.5f, 0.5f), res, Mathf.Infinity);
                    if (hits < 6)
                    {
                        transform.position = gamePlay.firstLeftPos + new Vector2(-i * 0.693f, i * 0.693f);
                        AddChess(gamePlay.computerChess, false, false, i);
                        hasAdd = true;
                        transform.position = _originalPos;
                        break;
                    }
                }
            }
        }
        if (!hasAdd)
        {
            for (int i = 0; i < 6; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(gamePlay.firstRightPos + new Vector2(i * 0.693f, i * 0.693f), new Vector2(-0.5f, 0.5f));
                if (hit)
                {
                    if (hit.transform.CompareTag("PlayerChess"))
                    {
                        RaycastHit2D[] res = new RaycastHit2D[7];
                        int hits = Physics2D.RaycastNonAlloc(gamePlay.firstRightPos + new Vector2(i * 0.693f, i * 0.693f),
                            new Vector2(-0.5f, 0.5f), res, Mathf.Infinity);
                        if (hits < 6)
                        {
                            transform.position = gamePlay.firstRightPos + new Vector2(i * 0.693f, i * 0.693f);
                            AddChess(gamePlay.computerChess, false, true, i);
                            hasAdd = true;
                            transform.position = _originalPos;
                            break;
                        }
                    }
                }
            }
        }
        if (!hasAdd)
        {
            transform.position = gamePlay.firstRightPos + new Vector2(5 * 0.693f, 5 * 0.693f);
            AddChess(gamePlay.computerChess, false, true, 5);
            transform.position = _originalPos;
        }
        yield return new WaitForSeconds(1f);
        gamePlay.isPlayerTurn = true;
    }
    
    private void ChosePosChess(Object ob)
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 pos = touch.position;
                _position = Camera.main.ScreenToWorldPoint(pos);
                if (_position.y <= 0.463f)
                {
                    if (_position.x >= 3.465f)
                    {
                        transform.position = new Vector3(4.158f, -0.23f, 0f);
                        posIndex = 16;
                    }
                    else if (_position.x >= 2.772f)
                    {
                        transform.position = new Vector3(3.465f, -0.923f, 0f);
                        posIndex = 15;
                    }
                    else if (_position.x >= 2.079f)
                    {
                        transform.position = new Vector3(2.772f, -1.616f, 0f);
                        posIndex = 14;
                    }
                    else if (_position.x >= 1.386f)
                    {
                        transform.position = new Vector3(2.079f, -2.309f, 0f);
                        posIndex = 13;
                    }
                    else if (_position.x >= 0.693f)
                    {
                        transform.position = new Vector3(1.386f, -3.002f, 0f);
                        posIndex = 12;
                    }
                    else if (_position.x >= 0.1f)
                    {
                        transform.position = new Vector3(0.693f, -3.695f, 0f);
                        posIndex = 11;
                    } 
                    else if (_position.x >= -0.1f)
                    {
                        transform.position = _originalPos;
                        posIndex = 0;
                    }
                    else if (_position.x >= -0.693f)
                    {
                        transform.position = new Vector3(-0.693f, -3.695f, 0f);
                        posIndex = 21;
                    }
                    else if (_position.x >= -1.386f)
                    {
                        transform.position = new Vector3(-1.386f, -3.002f, 0f);
                        posIndex = 22;
                    }
                    else if (_position.x >= -2.079f)
                    {
                        transform.position = new Vector3(-2.079f, -2.309f, 0f);
                        posIndex = 23;
                    }
                    else if (_position.x >= -2.772f)
                    {
                        transform.position = new Vector3(-2.772f, -1.616f, 0f);
                        posIndex = 24;
                    }
                    else if (_position.x >= -3.465f)
                    {
                        transform.position = new Vector3(-3.465f, -0.923f, 0f);
                        posIndex = 25;
                    }
                    else
                    {
                        transform.position = new Vector3(-4.158f, -0.23f, 0f);
                        posIndex = 26;
                    }
                }
                else
                {
                    transform.position = _originalPos;
                    posIndex = 0;
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                if (!Mathf.Approximately(transform.position.y, -4.35f))
                {
                    RaycastHit2D[] res = new RaycastHit2D[7];
                    int hits = Physics2D.RaycastNonAlloc(transform.position, new Vector2((posIndex / 10 == 1)? -0.5f : 0.5f, 0.5f), res, Mathf.Infinity);
                    if (hits < 7)
                    {
                        AddChess(ob,true,false, 0);
                        transform.position = _originalPos;
                        posIndex = 0;
                        gamePlay.isPlayerTurn = !gamePlay.isPlayerTurn;
                        hasCallComputer = false;
                        gamePlay.canCallComputer = false;
                    }
                    else
                    {
                        Debug.Log("Cant");
                        transform.position = _originalPos;
                        posIndex = 0;
                    }
                }
                else
                {
                    transform.position = _originalPos;
                    posIndex = 0;
                }
            }
        }
    }
    
    private void AddChess(Object ob, bool isP, bool isR, int ind)
    {
        gamePlay.backGroundMusic.PlaySound(gamePlay.backGroundMusic.moveSound);
        Object temp = Instantiate(ob, transform.position, Quaternion.identity);
        Transform tempTrans = temp.GetComponent<Transform>();
        tempTrans.parent = gamePlay.transform;
        int num = posIndex % 10;
        if (isP)
        {
            if (posIndex / 10 == 1) //right
            {
                StartCoroutine(PushChess(true, num - 1));
            
            } else if (posIndex / 10 == 2)  //left
            {
                StartCoroutine(PushChess(false, num - 1));
            }
        }
        else
        {
            StartCoroutine(PushChess(isR, ind));
        }
    }
    
    private IEnumerator PushChess(bool isR, int index)
    {
        if (isR)
        {
            RaycastHit2D[] res = new RaycastHit2D[7];
            Physics2D.RaycastNonAlloc(transform.position, new Vector2(-0.5f, 0.5f), res, Mathf.Infinity);
            foreach (var hit in res)
            {
                if (hit && hit.transform.GetComponent<PlayerChess>())
                {
                    PlayerChess temp = hit.transform.GetComponent<PlayerChess>();
                    temp.left++;
                    StartCoroutine(ChessBehavior(true, hit.transform, index, hit.transform.GetComponent<PlayerChess>().left));
                }
            }
        }
        else
        {
            RaycastHit2D[] res = new RaycastHit2D[7];
            Physics2D.RaycastNonAlloc(transform.position, new Vector2(0.5f, 0.5f), res, Mathf.Infinity);
            foreach (var hit in res)
            {
                if (hit && hit.transform.GetComponent<PlayerChess>())
                {
                    PlayerChess temp = hit.transform.GetComponent<PlayerChess>();
                    temp.right++;
                    StartCoroutine(ChessBehavior(false, hit.transform, index, hit.transform.GetComponent<PlayerChess>().right));
                }
            }
        }
        yield return new WaitForSeconds(0.5f);
        gamePlay.CheckWin();
    }

    private IEnumerator ChessBehavior(bool isRight, Transform trans, int index, int otherIndex)
    {
        if (isRight)
        {
            StartCoroutine(MovingAnim(0.25f, trans,new Vector2(trans.position.x - 0.693f, trans.position.y + 0.693f)));
            yield return new WaitForSeconds(0.25f);
            int endIndex = -1;
            for (int i = 0; i < index; i++)
            {
                if (!gamePlay.ChessGrid[otherIndex, i])
                {
                    endIndex = i;
                    break;
                }
            }

            if (endIndex != -1)
            {
                StartCoroutine(MovingAnim(0.25f, trans, new Vector2(0.693f * endIndex - 0.693f * otherIndex, -3.002f + 0.693f * endIndex + 0.693f * otherIndex)));
                yield return new WaitForSeconds(0.25f);
                gamePlay.backGroundMusic.audioSource.PlayOneShot(gamePlay.backGroundMusic.dropSound);
                gamePlay.ChessGrid[otherIndex, endIndex] = true;
                trans.GetComponent<PlayerChess>().right = endIndex;
            }
            else
            {
                gamePlay.ChessGrid[otherIndex, index] = true;
                trans.GetComponent<PlayerChess>().right = index;
            }
        }
        else
        {
            StartCoroutine(MovingAnim(0.25f, trans,new Vector2(trans.position.x + 0.693f, trans.position.y + 0.693f)));
            yield return new WaitForSeconds(0.25f);
            int endIndex = -1;
            for (int i = 0; i < index; i++)
            {
                if (!gamePlay.ChessGrid[i, otherIndex])
                {
                    endIndex = i;
                    break;
                }
            }

            if (endIndex != -1)
            {
                StartCoroutine(MovingAnim(0.25f, trans, new Vector2( - 0.693f * endIndex + 0.693f * otherIndex, -3.002f + 0.693f * endIndex + 0.693f * otherIndex)));
                yield return new WaitForSeconds(0.25f);
                gamePlay.backGroundMusic.audioSource.PlayOneShot(gamePlay.backGroundMusic.dropSound);
                gamePlay.ChessGrid[endIndex, otherIndex] = true;
                trans.GetComponent<PlayerChess>().left = endIndex;
            }
            else
            {
                gamePlay.ChessGrid[index, otherIndex] = true;
                trans.GetComponent<PlayerChess>().left = index;
            }
        }
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
    

    private void Update()
    {
        if (!gamePlay.isDisabled)
        {
            if (gamePlay.isPlayerTurn)
            {
                gamePlay.playerTurn.SetActive(true);
                gamePlay.computerTurn.SetActive(false);
                ChosePosChess(gamePlay.playerChess);
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
