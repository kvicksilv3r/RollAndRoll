using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TestRun : MonoBehaviour
{
    public List<DiceStats> dicePool = new List<DiceStats>();

    public List<DiceStats> dices = new List<DiceStats>();

    public DiceStats currentDie;

    public int maxDiceCount = 5;

    public int lastRoll = 0;
    public bool canUseRolledValue = false;

    public List<TextMeshProUGUI> diceTexts = new List<TextMeshProUGUI>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    //TODO Add dice bag. Add gold. Add lose state. Add saving. Add bag building 

    // Update is called once per frame
    void Update()
    {

    }

    public void DrawDice()
    {
        canUseRolledValue = false;
        dices.Clear();

        bool keepDrawing = true;
        while (keepDrawing)
        {
            keepDrawing = DrawDie();
            UpdateDiceText();
        }
    }

    public bool DrawDie()
    {
        if (dices.Count < maxDiceCount)
        {
            dices.Add(dicePool[Random.Range(0, dicePool.Count)]);
            return true;
        }

        return false;
    }

    public void RollDie()
    {
        if (dices.Count <= 0)
        {
            Debug.Log("No dice, nothing happens");
            return;
        }

        int roll = dices[0].sides[Random.Range(0, dices[0].sides.Length)];
        lastRoll = roll;

        Debug.Log("Rolled: " + roll + " / " + dices[0].sides.Last());

        dices.RemoveAt(0);
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

        for (int i = 0; i < dices.Count; i++)
        {
            diceTexts[i].text = dices[i].displayName;
        }
    }
}
