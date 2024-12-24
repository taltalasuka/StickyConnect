using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class GamePlay : MonoBehaviour
{
    public Object playerChess;
    public Object computerChess;
    public Vector2 firstRightPos = new Vector2(0.693f, -3.695f);
    public Vector2 firstLeftPos = new Vector2(-0.693f, -3.695f);
    public BackGroundMusic backGroundMusic;
    public GameObject endCanvas;
    public bool hasEndRound;
    public Image[] starsImage;         // 0 -> 2: player        3 -> 5: computer  
    public Sprite[] starsSprite;       //0: empty       1: star
    public int playerRoundWin;
    public int computerRoundWin;
    public bool isDisabled;
    public bool[,] ChessGrid;
    public bool isPlayerTurn;
    private bool _isPlayerWin;
    public GameObject winImage;
    public GameObject finalWinImage;
    public GameObject loseImage;
    public GameObject drawImage;
    public GameObject bonusImage;
    public TextMeshProUGUI bonusText;
    public Sprite[] playAgainSprites;           //0: retry            1: next
    public Image playAgainImage;
    public Image avatar;
    public Sprite[] avatarSprites;
    public Sprite[] musicSprites;         //0: music          1: muted
    public Image musicImage;
    public GameObject playerTurn;
    public GameObject computerTurn;
    public bool canCallComputer;

    public void PlayAgain()
    {
        backGroundMusic.PlaySound(backGroundMusic.buttonSound);
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        if (playerRoundWin == 3 || computerRoundWin == 3)
        {
            playerRoundWin = 0;
            computerRoundWin = 0;
            foreach (Image image in starsImage)
            {
                image.sprite = starsSprite[0];
            }
        }
        endCanvas.SetActive(false);
        hasEndRound = false;
        isDisabled = false;
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                ChessGrid[i, j] = false;
            }
        }
        isPlayerTurn = true;
        canCallComputer = false;
    }

    public void MusicToggle()
    {
        if (backGroundMusic.isMuted)
        {
            backGroundMusic.isMuted = false;
            backGroundMusic.audioSource.mute = false;
            musicImage.sprite = musicSprites[0];
            backGroundMusic.PlaySound(backGroundMusic.buttonSound);
        }
        else
        {
            backGroundMusic.isMuted = true;
            backGroundMusic.audioSource.mute = true;
            musicImage.sprite = musicSprites[1];
        }
    }

    public void BackToMenu()
    {
        StartCoroutine(BackToMenuSound());
    }

    private IEnumerator BackToMenuSound()
    {
        backGroundMusic.PlaySound(backGroundMusic.buttonSound);
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(0);
    }

    public void CheckWin()
    {
        for (int i = 0; i < 6; i++)
        {
            RaycastHit2D[] res = new RaycastHit2D[6];
            int hits = Physics2D.RaycastNonAlloc(firstRightPos + new Vector2(i * 0.693f, i * 0.693f),
                new Vector2(-0.5f, 0.5f), res, Mathf.Infinity);
            if (hits >= 4)
            {
                float firstPPos = 20f;
                float lastPPos = -20f;
                int pCounter = 0;
                float firstCPos = 20f;
                float lastCPos = -20f;
                int cCounter = 0;
                foreach (var hit in res)
                {
                    if (hit)
                    {
                        if (hit.transform.CompareTag("PlayerChess"))
                        {
                            if (hit.transform.position.y < firstPPos)
                            {
                                firstPPos = hit.transform.position.y;
                            }
                            if (hit.transform.position.y > lastPPos)
                            {
                                lastPPos = hit.transform.position.y;
                            }
                            pCounter++;
                        } 
                        if (hit.transform.CompareTag("ComputerChess"))
                        {
                            if (hit.transform.position.y < firstCPos)
                            {
                                firstCPos = hit.transform.position.y;
                            } 
                            if (hit.transform.position.y > lastCPos)
                            {
                                lastCPos = hit.transform.position.y;
                            }
                            cCounter++;
                        }
                    }
                }
                if (pCounter >= 4)
                {
                    if (Mathf.Approximately(pCounter - 1,Mathf.Abs((lastPPos - firstPPos) / 0.693f)))
                    {
                        hasEndRound = true;
                        starsImage[playerRoundWin].sprite = starsSprite[1];
                        playerRoundWin++;
                        _isPlayerWin = true;
                        DisplayEndRound();
                        return;
                    }
                } 
                if (pCounter == 5)
                {
                    if (Mathf.Approximately((lastCPos + 0.23f) / 0.693f, 0f) ||
                        Mathf.Approximately((lastCPos + 0.23f) / 0.693f, -3f))
                    {
                        hasEndRound = true;
                        starsImage[playerRoundWin].sprite = starsSprite[1];
                        playerRoundWin++;
                        _isPlayerWin = true;
                        DisplayEndRound();
                        return;
                    }
                }
                if (cCounter >= 4)
                {
                    if (Mathf.Approximately(cCounter - 1,Mathf.Abs((lastCPos - firstCPos) / 0.693f)))
                    {
                        hasEndRound = true;
                        starsImage[computerRoundWin + 3].sprite = starsSprite[1];
                        computerRoundWin++;
                        _isPlayerWin = false;
                        DisplayEndRound();
                        return;
                    }
                }
                if (cCounter == 5)
                {
                    if (Mathf.Approximately((lastPPos + 0.23f) / 0.693f, 0f) ||
                        Mathf.Approximately((lastPPos + 0.23f) / 0.693f, -3f))
                    {
                        hasEndRound = true;
                        starsImage[computerRoundWin + 3].sprite = starsSprite[1];
                        computerRoundWin++;
                        _isPlayerWin = false;
                        DisplayEndRound();
                        return;
                    }
                }
                
            }
        }
        for (int i = 0; i < 6; i++)
        {
            RaycastHit2D[] res = new RaycastHit2D[6];
            int hits = Physics2D.RaycastNonAlloc(firstLeftPos + new Vector2(-i * 0.693f, i * 0.693f), new Vector2(0.5f, 0.5f), res, Mathf.Infinity);
            if (hits >= 4)
            {
                float firstPPos = 20f;
                float lastPPos = -20f;
                int pCounter = 0;
                float firstCPos = 20f;
                float lastCPos = -20f;
                int cCounter = 0;
                foreach (var hit in res)
                {
                    if (hit)
                    {
                        if (hit.transform.CompareTag("PlayerChess"))
                        {
                            if (hit.transform.position.y < firstPPos)
                            {
                                firstPPos = hit.transform.position.y;
                            }
                            if (hit.transform.position.y > lastPPos)
                            {
                                lastPPos = hit.transform.position.y;
                            }
                            pCounter++;
                        } 
                        if (hit.transform.CompareTag("ComputerChess"))
                        {
                            if (hit.transform.position.y < firstCPos)
                            {
                                firstCPos = hit.transform.position.y;
                            }
                            if (hit.transform.position.y > lastCPos)
                            {
                                lastCPos = hit.transform.position.y;
                            }
                            cCounter++;
                        }
                    }
                }
                if (pCounter >= 4)
                {
                    if (Mathf.Approximately(pCounter - 1,Mathf.Abs((lastPPos - firstPPos) / 0.693f)))
                    {
                        hasEndRound = true;
                        starsImage[playerRoundWin].sprite = starsSprite[1];
                        playerRoundWin++;
                        _isPlayerWin = true;
                        DisplayEndRound();
                        return;
                    }
                }
                if (pCounter == 5)
                {
                    if (Mathf.Approximately((lastCPos + 0.23f) / 0.693f, 0f) ||
                        Mathf.Approximately((lastCPos + 0.23f) / 0.693f, -3f))
                    {
                        hasEndRound = true;
                        starsImage[playerRoundWin].sprite = starsSprite[1];
                        playerRoundWin++;
                        _isPlayerWin = true;
                        DisplayEndRound();
                        return;
                    }
                }

                if (cCounter >= 4)
                {
                    if (Mathf.Approximately(cCounter - 1,Mathf.Abs((lastCPos - firstCPos) / 0.693f)))
                    {
                        hasEndRound = true;
                        starsImage[computerRoundWin + 3].sprite = starsSprite[1];
                        computerRoundWin++;
                        _isPlayerWin = false;
                        DisplayEndRound();
                        return;
                    }
                }
                if (cCounter == 5)
                {
                    if (Mathf.Approximately((lastPPos + 0.23f) / 0.693f, 0f) ||
                        Mathf.Approximately((lastPPos + 0.23f) / 0.693f, -3f))
                    {
                        hasEndRound = true;
                        starsImage[computerRoundWin + 3].sprite = starsSprite[1];
                        computerRoundWin++;
                        _isPlayerWin = false;
                        DisplayEndRound();
                        return;
                    }
                }
            }
        }
        canCallComputer = true;
    }

    private void DisplayDraw()
    {
        backGroundMusic.PlaySound(backGroundMusic.loseSound);
        playAgainImage.sprite = playAgainSprites[1];
        endCanvas.SetActive(true);
        isDisabled = true;
        winImage.SetActive(false);
        finalWinImage.SetActive(false);
        loseImage.SetActive(false);
        bonusImage.SetActive(false);
        drawImage.SetActive(true);
    }

    private void DisplayEndRound()
    {
        if (playerRoundWin == 3 || computerRoundWin == 3)
        {
            playAgainImage.sprite = playAgainSprites[0];
        }
        else
        {
            playAgainImage.sprite = playAgainSprites[1];
        }
        endCanvas.SetActive(true);
        isDisabled = true;
        drawImage.SetActive(false);
        if (_isPlayerWin)
        {
            backGroundMusic.PlaySound(backGroundMusic.winSound);
            bonusImage.SetActive(true);
            if (playerRoundWin == 3)
            {
                finalWinImage.SetActive(true);
                winImage.SetActive(false);
                bonusText.text = "+" + backGroundMusic.winReward * 2;
                backGroundMusic.playerD.totalCoin += backGroundMusic.winReward * 2;
                SaveSystem.SaveData(backGroundMusic.playerD);
            }
            else
            {
                winImage.SetActive(true);
                finalWinImage.SetActive(false);
                bonusText.text = "+" + backGroundMusic.winReward;
                backGroundMusic.playerD.totalCoin += backGroundMusic.winReward;
                SaveSystem.SaveData(backGroundMusic.playerD);
            }
            loseImage.SetActive(false);
        }
        else
        {
            backGroundMusic.PlaySound(backGroundMusic.loseSound);
            winImage.SetActive(false);
            finalWinImage.SetActive(false);
            loseImage.SetActive(true);
            bonusImage.SetActive(false);
        }
    }
    
    
    private void Awake()
    {
        ChessGrid = new bool[6,6];    // false: empty      true: has object
        isPlayerTurn = true;
        playerTurn.SetActive(false);
        computerTurn.SetActive(false);
        endCanvas.SetActive(false);
        backGroundMusic = GameObject.FindGameObjectWithTag("BackGroundMusic").GetComponent<BackGroundMusic>();
        if (backGroundMusic.usedAvatar != -1)
        {
            avatar.sprite = avatarSprites[backGroundMusic.usedAvatar];
        }
        musicImage.sprite = musicSprites[backGroundMusic.isMuted ? 1 : 0];
        backGroundMusic.PlaySound(backGroundMusic.buttonSound);
    }

    private void Update()
    {
        if (!hasEndRound)
        {
            if (transform.childCount == 36)
            {
                Debug.Log("Draw");
                hasEndRound = true;
                DisplayDraw();
            }
        }
    }
}
