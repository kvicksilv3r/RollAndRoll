using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject player;

    public Vector3 offset;

    public float deltaModMin = 0.1f;
    public float deltaModMax = 1f;

    public float maxDelta = 0.1f;

    public float maxDistanceFromPlayer = 1f;
    public float minDistanceFromPlayer = 0.1f;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var distFromPlayer = Mathf.Clamp(
            (transform.position - (player.transform.position + offset)).magnitude,
            minDistanceFromPlayer,
            maxDistanceFromPlayer);
        var maxDeltaModifier = Mathf.Lerp(deltaModMin, deltaModMax, distFromPlayer);
        var v = Vector3.MoveTowards(transform.position, player.transform.position + offset, maxDelta * Time.deltaTime * maxDeltaModifier);

        Debug.Log(maxDeltaModifier);

        var posDelta = v - transform.position;

        posDelta.y /= 3f;

        transform.position = transform.position + posDelta;

    }
}
