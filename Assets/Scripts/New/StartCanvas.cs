using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class StartCanvas : MonoBehaviour
{
    public Button hostButton;
    public Button clientButton;

    private void Awake()
    {
        hostButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            gameObject.SetActive(false);
        });
        clientButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
            gameObject.SetActive(false);
        });
    }
}
