using UnityEngine;

public class NukePlayerPrefs : MonoBehaviour
{
    public void NukeIt()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
}
