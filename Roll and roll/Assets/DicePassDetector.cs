using UnityEngine;

public class DicePassDetector : MonoBehaviour
{
    public TrackBlock first;
    public TrackBlock second;
    public Track track;
    public TrackBlock currentBonk;
    public TrackBlock lastBonk;

    private void Start()
    {
        track = Track.Instance;
        track.globalBonk.AddListener(CheckBonk);
    }

    public void CheckBonk()
    {
        if (currentBonk)
        {
            lastBonk = currentBonk;
        }

        currentBonk = track.latestBonk;

        if (currentBonk == second && lastBonk == first)
        {
            DiceRunController.Instance.PassedGo();
        }
    }

    private void OnDrawGizmosSelected()
    {
        var gizmoSize = Vector3.one * 1.1f;

        if (first)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(first.gameObject.transform.position, gizmoSize);
        }

        if (second)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(second.gameObject.transform.position, gizmoSize);
        }
    }
}
