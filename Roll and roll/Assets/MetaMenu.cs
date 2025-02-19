using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MetaMenu : MonoBehaviour
{

    void Start()
    {
        InitPlayerPrefs();
    }

    private void InitPlayerPrefs()
    {
        var ppio = PlayerPrefsIO.Instance;
        var keys = ppio.keys;

        if (ppio.HasKey(keys.PLAYER_PREFS_INIT))
        {
            return;
        }

        ppio.WriteInt(keys.PLAYER_GOLD, 0);
        ppio.WriteInt(keys.TEMPORARY_GOLD, 0);

        ppio.WriteString(keys.PLAYER_DICE_BAG, "");

        ppio.WriteString(keys.UNLOCKED_DICE, "0825586db5b1f6a489fe5715c6f6eebf");

        ppio.WriteBool(keys.PLAYER_PREFS_INIT, true);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
}
