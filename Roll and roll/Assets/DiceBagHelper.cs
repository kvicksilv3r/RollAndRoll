using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiceBagHelper : MonoBehaviour
{
    public DiceBag testBag;

    public DiceBag returnBag;

    public string bagString;


    public void SaveDiceBag()
    {
        List<string> bagContent = new List<string>();

        foreach (var die in testBag.bag)
        {
            bagContent.Add(die.UID);
        }

        bagString = string.Join(',', bagContent);

        PlayerPrefsIO.Instance.WriteString(PlayerPrefsIO.Instance.keys.PLAYER_DICE_BAG, bagString);
    }

    public DiceBag GetPlayerDiceBag()
    {

        if (returnBag == null || returnBag.bag.IsEmpty())
        {
            LoadDiceBag();
        }

        return returnBag;
    }

    public void LoadDiceBag()
    {
        string savedBag = FetchSavedPlayerBag();

        DiceBagAssembler(savedBag);
    }

    private static string FetchSavedPlayerBag()
    {
        return PlayerPrefsIO.Instance.GetString(PlayerPrefsIO.Instance.keys.PLAYER_DICE_BAG);
    }

    private void DiceBagAssembler(string savedBag)
    {
        returnBag = new DiceBag();

        var splitSave = savedBag.Split(",");

        var diceCollection = DiceCollectionHelper.Instance.GetDiceCollection();

        foreach (var savedGuid in splitSave)
        {
            returnBag.bag.Add(diceCollection.Find(d => d.UID == savedGuid));
        }
    }
}
