using UnityEngine;

[CreateAssetMenu(fileName = "PlayerPrefsKeys", menuName = "Scriptable Objects/PlayerPrefsKeys")]
public class PlayerPrefsKeys : ScriptableObject
{
    //Sound options
    public readonly string PLAYER_GOLD = "PlayerGold";
    public readonly string TEMPORARY_GOLD = "TempGold";

    public readonly string PLAYER_DICE_BAG = "PlayerDiceBag";

    public readonly string UNLOCKED_DICE = "PlayerUnlockedDice";

    public readonly string PLAYER_PREFS_INIT = "PrefsIni";
}
