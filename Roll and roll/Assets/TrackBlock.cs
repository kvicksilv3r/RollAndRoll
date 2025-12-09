using System.Collections;
using UnityEngine;

public enum TrackBlockType
{
    Normal,
    Gold,
    Extract,
    Proceed
}

public class TrackBlock : MonoBehaviour
{
    public Transform playerPlace;

    public Material myMat;

    [ColorUsage(true, true)]
    public Color glowingColor;
    [ColorUsage(true, true)]
    public Color discoveredColor;

    [ColorUsage(true, true)]
    public Color normalBlockColor;

    [ColorUsage(true, true)]
    public Color normalBlockDiscoveredColor;

    [ColorUsage(true, true)]
    public Color goldBlockColor;

    [ColorUsage(true, true)]
    public Color goldBlockDiscoveredColor;

    public GameObject goldBlockBling;

    public TrackBlockType blockType;
    private TrackBlockType savedBlockType = TrackBlockType.Normal;

    public Transform holster;

    private bool blockDiscovered = false;

    public float emissionFlashTime = 0.5f;

    public bool BlockDiscovered { get => blockDiscovered; private set => blockDiscovered = value; }

    private void Start()
    {
        myMat = GetComponent<Renderer>().material;
        GetComponent<Renderer>().material = myMat;

        switch (blockType)
        {
            case TrackBlockType.Normal:
                discoveredColor = normalBlockDiscoveredColor;
                break;
            case TrackBlockType.Gold:
                discoveredColor = goldBlockDiscoveredColor;
                break;
            case TrackBlockType.Extract:
                break;
            case TrackBlockType.Proceed:
                break;
            default:
                break;
        }
    }

    public void Bonk()
    {
        Track.Instance.BlockBonked(this);
        StopAllCoroutines();
        StartCoroutine(BlinkEmission());
        blockDiscovered = true;
    }

    private IEnumerator BlinkEmission()
    {
        yield return new WaitForSeconds(0.3f);

        myMat.SetColor("_EmissionColor", glowingColor);

        float timer = 0;
        while (timer < emissionFlashTime)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            var c = Color.Lerp(glowingColor, discoveredColor, timer / emissionFlashTime);
            myMat.SetColor("_EmissionColor", c);
        }

        yield return null;
    }

    private void OnValidate()
    {
        if (savedBlockType == blockType)
        {
            return;
        }

        savedBlockType = blockType;

        if (!myMat)
        {
            myMat = GetComponent<Renderer>().material;
        }

        switch (blockType)
        {
            case TrackBlockType.Normal:
                SetBaseColor(normalBlockColor);
                break;
            case TrackBlockType.Gold:
                SetBaseColor(goldBlockColor);
                break;
            case TrackBlockType.Extract:
                break;
            case TrackBlockType.Proceed:
                break;
        }
    }

    private void SetBaseColor(Color color)
    {
        myMat.SetColor("_BaseColor", color);
    }
}
