using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public static PlayerMovementController Instance;
    public Player player;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }

        if (Instance != this)
        {
            print($"Too many {this}, killing myself");
            Destroy(this);
        }
    }

    public void InitiateMove(int steps)
    {
        player.AttemptStartMove(steps);
    }
}
