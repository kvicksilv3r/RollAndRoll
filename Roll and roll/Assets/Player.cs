using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public TrackBlock currentBlock;
    public Track track;
    public TestRun run;

    public bool canMove = true;

    public float jumpTime = 0.34f;

    public AnimationCurve jumpCurve;

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
        }

        transform.DOJump(nextBlock.playerPlace.position, 1, 1, jumpTime, false).OnComplete(() => MoveStep(moves - 1, forward));
        transform.DOLookAt(nextBlock.playerPlace.position, jumpTime);
        currentBlock = nextBlock;
    }

    public void AttemptStartMove()
    {
        if (!canMove || !run.canUseRolledValue)
        {
            return;
        }

        var moves = run.lastRoll;
        run.ClaimRoll();

        MoveStep(moves, moves > 0);
    }
}
