using System;
using UnityEngine;

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
        ppio.WriteInt(keys.PLAYER_GOLD, 0);

        ppio.WriteString(keys.PLAYER_DICE_BAG, "");
    }

    void Update()
    {

    }
}
