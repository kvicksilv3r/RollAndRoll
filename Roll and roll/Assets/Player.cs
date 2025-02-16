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

    Sequence mySequence = DOTween.Sequence();

    void Start()
    {
        currentBlock = track.trackBlocks[0];

        transform.position = currentBlock.playerPlace.position;

        DOTween.Init();
    }

    public void MoveStep(int moves)
    {
        if (moves <= 0)
        {
            return;
        }

        var nextBlock = track.GetNextBlock(currentBlock);
        transform.DOJump(nextBlock.playerPlace.position, 1, 1, jumpTime, false).OnComplete(() => MoveStep(moves - 1));
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

        MoveStep(moves);
    }

}
