using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiceBagHelper : MonoBehaviour
{
    public DiceBag playerDiceBag;
    public static DiceBagHelper Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SaveDiceBag(DiceBag playerBag)
    {
        List<string> bagContent = new List<string>();

        foreach (var die in playerBag.bag)
        {
            bagContent.Add(die.UID);
        }

        var bagString = string.Join(',', bagContent);

        PlayerPrefsIO.Instance.WriteString(PlayerPrefsIO.Instance.keys.PLAYER_DICE_BAG, bagString);
    }

    public DiceBag GetPlayerDiceBag()
    {
        if (playerDiceBag == null || playerDiceBag.bag.IsEmpty())
        {
            LoadPlayerDiceBag();
        }

        return playerDiceBag;
    }

    public void LoadPlayerDiceBag()
    {
        string savedBag = FetchSavedPlayerBag();

        playerDiceBag = DiceBagAssembler(savedBag);
    }

    private static string FetchSavedPlayerBag()
    {
        return PlayerPrefsIO.Instance.GetString(PlayerPrefsIO.Instance.keys.PLAYER_DICE_BAG);
    }

    public DiceBag DiceBagAssembler(string savedBag)
    {
        var returnBag = new DiceBag();

        var splitSave = savedBag.Split(",");

        var diceCollection = DiceCollectionHelper.Instance.GetAllDice().bag;

        foreach (var savedGuid in splitSave)
        {
            returnBag.bag.Add(diceCollection.Find(d => d.UID == savedGuid));
        }

        return returnBag;
    }
}
