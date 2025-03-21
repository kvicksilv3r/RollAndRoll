using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiceCollectionHelper : MonoBehaviour
{
    protected DiceBag allDice = new DiceBag();
    protected DiceBag unlockedDice = new DiceBag();
    public static DiceCollectionHelper Instance;

    private void Awake()
    {
        Instance = this;

        SetupAlLDice();
    }

    private void Start()
    {
        StartCoroutine(SetupDelay());
    }

    IEnumerator SetupDelay()
    {
        yield return new WaitForSeconds(0);

        SetupUnlockedDice();
    }

    public DiceBag GetAllDice()
    {
        return allDice;
    }

    public DiceBag GetUnlockedDice()
    {
        return unlockedDice;
    }

    private void SetupAlLDice()
    {
        if (allDice.bag.Count > 0)
        {
            return;
        }

        allDice.bag = Resources.LoadAll<DiceStats>("DiceData").ToList();
    }

    public void UnlockNewDice(DiceStats newDice)
    {
        unlockedDice.Add(newDice);

        List<string> bagContent = new List<string>();

        foreach (var die in unlockedDice.bag)
        {
            bagContent.Add(die.UID);
        }

        var bagString = string.Join(',', bagContent);

        PlayerPrefsIO.Instance.WriteString(PlayerPrefsIO.Instance.keys.UNLOCKED_DICE, bagString);
    }

    private void SetupUnlockedDice()
    {
        if (unlockedDice.bag.Count > 0)
        {
            return;
        }

        var savedUnlockeDice = PlayerPrefsIO.Instance.GetString(PlayerPrefsIO.Instance.keys.UNLOCKED_DICE);

        unlockedDice = DiceBagHelper.Instance.DiceBagAssembler(savedUnlockeDice);
    }
}
