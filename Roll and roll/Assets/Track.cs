using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Track : MonoBehaviour
{
    public List<TrackBlock> trackBlocks = new List<TrackBlock>();

    public static Track Instance;

    public UnityEvent globalBonk;
    public TrackBlock latestBonk;

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

    public void BlockBonked(TrackBlock bonkedBlock)
    {
        latestBonk = bonkedBlock;
        globalBonk.Invoke();
    }

    public TrackBlock GetNextBlock(TrackBlock currentBlock)
    {
        var currIndex = trackBlocks.IndexOf(currentBlock);

        return trackBlocks[(currIndex + 1) % trackBlocks.Count];
    }

    public TrackBlock GetPreviousBlock(TrackBlock currentBlock)
    {
        var currIndex = trackBlocks.IndexOf(currentBlock);

        return trackBlocks[(currIndex + trackBlocks.Count - 1) % trackBlocks.Count];
    }

    void Start()
    {

    }
    void Update()
    {

    }
}
