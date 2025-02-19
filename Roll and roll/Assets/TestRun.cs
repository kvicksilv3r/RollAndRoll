using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TestRun : MonoBehaviour
{
    public DiceBag dicePool = new DiceBag();

    public DiceBag dices = new DiceBag();

    public DiceStats currentDie;

    public int maxDiceCount = 5;

    public int lastRoll = 0;
    public bool canUseRolledValue = false;

    public List<TextMeshProUGUI> diceTexts = new List<TextMeshProUGUI>();

    public bool useRealData = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (useRealData)
        {
            dicePool = DiceBagHelper.Instance.GetPlayerDiceBag();
        }
    }

    //TODO Add lose state. Add bag building. Add get gold. 

    public void DrawDice()
    {
        canUseRolledValue = false;
        dices.bag.Clear();

        bool keepDrawing = true;
        while (keepDrawing)
        {
            keepDrawing = DrawDie();
            UpdateDiceText();
        }
    }

    public bool DrawDie()
    {
        if (dices.bag.Count < maxDiceCount)
        {
            dices.bag.Add(dicePool.bag[Random.Range(0, dicePool.bag.Count)]);
            return true;
        }

        return false;
    }

    public void RollDie()
    {
        if (dices.bag.Count <= 0)
        {
            Debug.Log("No dice, nothing happens");
            return;
        }

        int roll = dices.bag[0].sides[Random.Range(0, dices.bag[0].sides.Length)];
        lastRoll = roll;

        Debug.Log("Rolled: " + roll + " / " + dices.bag[0].sides.Last());

        dices.bag.RemoveAt(0);
        canUseRolledValue = true;

        UpdateDiceText();
    }

    public void ClaimRoll()
    {
        canUseRolledValue = false;
        lastRoll = 0;
    }

    private void UpdateDiceText()
    {
        foreach (var tmpro in diceTexts)
        {
            tmpro.text = "";
        }

        for (int i = 0; i < dices.bag.Count; i++)
        {
            diceTexts[i].text = dices.bag[i].displayName;
        }
    }
}
