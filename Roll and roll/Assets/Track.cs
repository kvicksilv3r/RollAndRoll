using JetBrains.Annotations;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem;

public class Track : MonoBehaviour
{
    public List<TrackBlock> trackBlocks = new List<TrackBlock>();

    public Track Instance;

    private void Awake()
    {
        Instance = this;
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

    // Update is called once per frame
    void Update()
    {

    }
}
