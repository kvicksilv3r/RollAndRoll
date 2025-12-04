using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public TrackBlock currentBlock;
    public Track track;

    public bool canMove = true;

    public float jumpTime = 0.34f;

    public bool isTweening = false;

    void Start()
    {
        currentBlock = track.trackBlocks[0];

        transform.position = currentBlock.playerPlace.position;

        DOTween.Init();
    }

    public void MoveStep(int moves, bool forward)
    {
        moves = Mathf.Abs(moves);

        if (moves <= 0)
        {
            return;
        }

        TrackBlock nextBlock = null;

        if (forward)
        {
            nextBlock = track.GetNextBlock(currentBlock);
        }

        else
        {
            nextBlock = track.GetPreviousBlock(currentBlock);
            if (!nextBlock.BlockDiscovered)
            {
                return;
            }
        }

        transform.DOJump(nextBlock.playerPlace.position, 1, 1, jumpTime, false).OnComplete(() => MoveStep(moves - 1, forward));
        transform.DOLookAt(nextBlock.playerPlace.position, jumpTime, AxisConstraint.Y);
        currentBlock = nextBlock;
        BonkBlock();
    }

    private void BonkBlock()
    {
        currentBlock.Bonk();
    }

    public void AttemptStartMove(int moveCount)
    {
        //    if (!canMove || !run.canUseRolledValue)
        //    {
        //        return;
        //    }

        var moves = moveCount;

        MoveStep(moves, moves > 0);
    }
}
