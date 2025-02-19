using UnityEngine;

public class GoldHelper : MonoBehaviour
{
    public static GoldHelper Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        TryMakeTempGoldPermanent();
    }

    public void TryMakeTempGoldPermanent()
    {
        var tempGold = PlayerPrefsIO.Instance.GetInt(PlayerPrefsIO.Instance.keys.TEMPORARY_GOLD);

        if (tempGold == 0)
        {
            return;
        }

        AddPlayerGold(tempGold);

        PlayerPrefsIO.Instance.WriteInt(PlayerPrefsIO.Instance.keys.TEMPORARY_GOLD, 0);

        //play animation or whatever, idk

    }

    public void AddPlayerGold(int addedGold)
    {
        SetPlayerGold(GetPlayerGold() + addedGold);
    }

    public void RemovePlayerGold(int removedGold)
    {
        SetPlayerGold(GetPlayerGold() - removedGold);
    }

    public void SetPlayerGold(int newGold)
    {
        PlayerPrefsIO.Instance.WriteInt(PlayerPrefsIO.Instance.keys.PLAYER_GOLD, newGold);
    }

    public int GetPlayerGold()
    {
        return PlayerPrefsIO.Instance.GetInt(PlayerPrefsIO.Instance.keys.PLAYER_GOLD);
    }
}
