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
    }

    public void InitiateMove(int steps)
    {
        player.AttemptStartMove(steps);
    }
}
