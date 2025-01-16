using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    public PlayerData playerD;
    public int usedAvatar;
    public int winReward;
    public bool isMuted;
    public AudioClip buttonSound;
    public AudioClip buySound;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip moveSound;
    public AudioClip dropSound;
    public AudioSource audioSource;
    public int mode;

    public void PlaySound(AudioClip sound)
    {
        if (!isMuted)
        {
            audioSource.PlayOneShot(sound);
        }
    }
    private void Awake()
    {
        Application.targetFrameRate = 120;
        GameObject[] goj = GameObject.FindGameObjectsWithTag("BackGroundMusic");
        if (goj.Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        if (!SaveSystem.IsExists())
        {
            PlayerData temp = new PlayerData(0, false, false, false, false, false, false, false, false);
            SaveSystem.SaveData(temp);
        }
        playerD = SaveSystem.LoadData();
        usedAvatar = -1;
        audioSource = GetComponent<AudioSource>();
    }
}
