using UnityEngine;
using UnityEngine.Events;

public class GoldHelper : MonoBehaviour
{
    public static GoldHelper Instance;

    public bool tryMakeTempGoldPerm = false;

    public UnityEvent goldValueUpdated;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (tryMakeTempGoldPerm)
        {
            TryMakeTempGoldPermanent();
        }
    }

    public void AddTemporaryGold(int addedTempGold)
    {
        var tempGold = GetTemporaryGold();
        tempGold += addedTempGold;
        SetTemporaryGold(tempGold);
    }

    private void SetTemporaryGold(int tempGold)
    {
        PlayerPrefsIO.Instance.WriteInt(PlayerPrefsIO.Instance.keys.TEMPORARY_GOLD, tempGold);
    }

    public int GetTemporaryGold()
    {
        return PlayerPrefsIO.Instance.GetInt(PlayerPrefsIO.Instance.keys.TEMPORARY_GOLD);
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

        goldValueUpdated.Invoke();

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
