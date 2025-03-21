using System.Collections;
using UnityEngine;

public class TrackBlock : MonoBehaviour
{
    public Transform playerPlace;

    public Material myMat;

    [ColorUsage(true, true)]
    public Color c1;
    [ColorUsage(true, true)]
    public Color c2;

    public float emissionFlashTime = 0.5f;

    private void Start()
    {
        myMat = GetComponent<Renderer>().material;
        GetComponent<Renderer>().material = myMat;
    }

    public void Bonk()
    {
        Track.Instance.BlockBonked(this);
        StopAllCoroutines();
        StartCoroutine(BlinkEmission());
    }

    private IEnumerator BlinkEmission()
    {
        yield return new WaitForSeconds(0.3f);

        myMat.SetColor("_EmissionColor", c1);

        float timer = 0;
        while (timer < emissionFlashTime)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            var c = Color.Lerp(c1, c2, timer / emissionFlashTime);
            myMat.SetColor("_EmissionColor", c);
        }

        yield return null;
    }
}
