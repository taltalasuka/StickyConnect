using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private BackGroundMusic _backGroundMusic;
    public TextMeshProUGUI playerCoin;
    public GameObject difficulty;
    public GameObject shop;
    public Button[] buyButtons;
    public Button[] useButtons;
    public Button[] levelButtons;
    public GameObject botPanel;
    public GameObject diffPanel;

    public void BotModeButton()
    {
        _backGroundMusic.PlaySound(_backGroundMusic.buttonSound);
        botPanel.SetActive(true);
        diffPanel.SetActive(false);
    }

    public void PlayerModeButton()
    {
        _backGroundMusic.mode = 4;
        SceneManager.LoadScene(1);
    }

    private void LoadGame(int difficultLevel)
    {
        if (difficultLevel == 0)
        {
            _backGroundMusic.winReward = 100;
            _backGroundMusic.mode = 1;
        } else if (difficultLevel == 1)
        {
            _backGroundMusic.winReward = 200;
            _backGroundMusic.mode = 2;
        }
        else
        {
            _backGroundMusic.winReward = 300;
            _backGroundMusic.mode = 3;
        }
        SceneManager.LoadScene(1);
    }

    private void BuyButton(int index)
    {
        if (_backGroundMusic.playerD.totalCoin >= 3000)
        {
            _backGroundMusic.PlaySound(_backGroundMusic.buySound);
            _backGroundMusic.playerD.totalCoin -= 3000;
            playerCoin.text = _backGroundMusic.playerD.totalCoin.ToString();
            buyButtons[index].gameObject.SetActive(false);
            useButtons[index].gameObject.SetActive(true);
            if (index == 0)
            {
                _backGroundMusic.playerD.hasGoblin = true;
            } else if (index == 1)
            {
                _backGroundMusic.playerD.hasDevil = true;
            } else if (index == 2)
            {
                _backGroundMusic.playerD.hasSanta = true;
            } else if (index == 3)
            {
                _backGroundMusic.playerD.hasSnowMan = true;
            } else if (index == 4)
            {
                _backGroundMusic.playerD.hasDoggo = true;
            } else if (index == 5)
            {
                _backGroundMusic.playerD.hasCate = true;
            } else if (index == 6)
            {
                _backGroundMusic.playerD.hasReindeer = true;
            } else if (index == 7)
            {
                _backGroundMusic.playerD.hasBlackCat = true;
            }
            SaveSystem.SaveData(_backGroundMusic.playerD);
        }
    }

    private void UseButton(int index)
    {
        _backGroundMusic.PlaySound(_backGroundMusic.buttonSound);
        if (_backGroundMusic.usedAvatar == index)
        {
            useButtons[index].image.color = new Color(76f / 255f, 209f / 255f, 55f / 255f);
            useButtons[index].GetComponentInChildren<TextMeshProUGUI>().text = "Use";
            _backGroundMusic.usedAvatar = -1;
        }
        else
        {
            if (_backGroundMusic.usedAvatar != -1)
            {
                useButtons[_backGroundMusic.usedAvatar].image.color = new Color(76f / 255f, 209f / 255f, 55f / 255f);
                useButtons[_backGroundMusic.usedAvatar].GetComponentInChildren<TextMeshProUGUI>().text = "Use";
            }
            useButtons[index].image.color = new Color(106 / 255f, 176 / 255f, 76 / 255f);
            useButtons[index].GetComponentInChildren<TextMeshProUGUI>().text = "Using";
            _backGroundMusic.usedAvatar = index;
        }
    }

    public void ShopButton()
    {
        _backGroundMusic.PlaySound(_backGroundMusic.buttonSound);
        shop.SetActive(true);
        difficulty.SetActive(false);
    }

    public void BackButton()
    {
        _backGroundMusic.PlaySound(_backGroundMusic.buttonSound);
        shop.SetActive(false);
        difficulty.SetActive(true);
        botPanel.SetActive(false);
        diffPanel.SetActive(true);
    }
    
    private void Start()
    {
        _backGroundMusic = GameObject.FindGameObjectWithTag("BackGroundMusic").GetComponent<BackGroundMusic>();
        playerCoin.text = _backGroundMusic.playerD.totalCoin.ToString();
        useButtons[0].gameObject.SetActive(_backGroundMusic.playerD.hasGoblin);
        buyButtons[0].gameObject.SetActive(!_backGroundMusic.playerD.hasGoblin);
        useButtons[1].gameObject.SetActive(_backGroundMusic.playerD.hasDevil);
        buyButtons[1].gameObject.SetActive(!_backGroundMusic.playerD.hasDevil);
        useButtons[2].gameObject.SetActive(_backGroundMusic.playerD.hasSanta);
        buyButtons[2].gameObject.SetActive(!_backGroundMusic.playerD.hasSanta);
        useButtons[3].gameObject.SetActive(_backGroundMusic.playerD.hasSnowMan);
        buyButtons[3].gameObject.SetActive(!_backGroundMusic.playerD.hasSnowMan);
        useButtons[4].gameObject.SetActive(_backGroundMusic.playerD.hasDoggo);
        buyButtons[4].gameObject.SetActive(!_backGroundMusic.playerD.hasDoggo);
        useButtons[5].gameObject.SetActive(_backGroundMusic.playerD.hasCate);
        buyButtons[5].gameObject.SetActive(!_backGroundMusic.playerD.hasCate);
        useButtons[6].gameObject.SetActive(_backGroundMusic.playerD.hasReindeer);
        buyButtons[6].gameObject.SetActive(!_backGroundMusic.playerD.hasReindeer);
        useButtons[7].gameObject.SetActive(_backGroundMusic.playerD.hasBlackCat);
        buyButtons[7].gameObject.SetActive(!_backGroundMusic.playerD.hasBlackCat);
        useButtons[0].onClick.AddListener(delegate{UseButton(0);});
        buyButtons[0].onClick.AddListener(delegate{BuyButton(0);});
        useButtons[1].onClick.AddListener(delegate{UseButton(1);});
        buyButtons[1].onClick.AddListener(delegate{BuyButton(1);});
        useButtons[2].onClick.AddListener(delegate{UseButton(2);});
        buyButtons[2].onClick.AddListener(delegate{BuyButton(2);});
        useButtons[3].onClick.AddListener(delegate{UseButton(3);});
        buyButtons[3].onClick.AddListener(delegate{BuyButton(3);});
        useButtons[4].onClick.AddListener(delegate{UseButton(4);});
        buyButtons[4].onClick.AddListener(delegate{BuyButton(4);});
        useButtons[5].onClick.AddListener(delegate{UseButton(5);});
        buyButtons[5].onClick.AddListener(delegate{BuyButton(5);});
        useButtons[6].onClick.AddListener(delegate{UseButton(6);});
        buyButtons[6].onClick.AddListener(delegate{BuyButton(6);});
        useButtons[7].onClick.AddListener(delegate{UseButton(7);});
        buyButtons[7].onClick.AddListener(delegate{BuyButton(7);});
        levelButtons[0].onClick.AddListener(delegate{LoadGame(0);});
        levelButtons[1].onClick.AddListener(delegate{LoadGame(1);});
        levelButtons[2].onClick.AddListener(delegate{LoadGame(2);});
        if (_backGroundMusic.usedAvatar != -1)
        {
            useButtons[_backGroundMusic.usedAvatar].image.color = new Color(106 / 255f, 176 / 255f, 76 / 255f);
            useButtons[_backGroundMusic.usedAvatar].GetComponentInChildren<TextMeshProUGUI>().text = "Using";
        }
        difficulty.SetActive(true);
        shop.SetActive(false);
        diffPanel.SetActive(true);
        botPanel.SetActive(false);
    }
    
    //NEW
    public void OnlineButton()
    {
        SceneManager.LoadScene(2);
    }
}
