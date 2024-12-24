using UnityEngine;

public class PlayerChess : MonoBehaviour
{
    public int left;
    public int right;

    private void Awake()
    {
        left = -1;
        right = -1;
    }
}
