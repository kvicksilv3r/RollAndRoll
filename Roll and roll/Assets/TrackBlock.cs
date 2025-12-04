using System.Collections;
using UnityEngine;

public class TrackBlock : MonoBehaviour
{
    public Transform playerPlace;

    public Material myMat;

    [ColorUsage(true, true)]
    public Color glowingColor;
    [ColorUsage(true, true)]
    public Color defaultColor;
    [ColorUsage(true, true)]
    public Color discoveredColor;

    private Color preferredColor;

    //TODO
    //Make discovered block change color
    //Fix fucked colors

    private bool blockDiscovered = false;

    public float emissionFlashTime = 0.5f;

    public bool BlockDiscovered { get => blockDiscovered; private set => blockDiscovered = value; }

    private void Start()
    {
        myMat = GetComponent<Renderer>().material;
        GetComponent<Renderer>().material = myMat;

        if (blockDiscovered)
        {
            preferredColor = discoveredColor;
        }
        else
        {
            preferredColor = defaultColor;
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
            var c = Color.Lerp(glowingColor, preferredColor, timer / emissionFlashTime);
            myMat.SetColor("_EmissionColor", c);
        }

        yield return null;
    }
}
