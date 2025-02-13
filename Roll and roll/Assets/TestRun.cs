using System.Collections.Generic;
using UnityEngine;

public class TestRun : MonoBehaviour
{
    public DiceStats defaultDice;

    public List<DiceStats> dices = new List<DiceStats>();

    public DiceStats currentDie;

    public int maxDiceCount = 5;

    public int lastRoll = 0;
    public bool canUseRoll = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DrawDice()
    {
        bool keepDrawing = true;
        while (keepDrawing)
        {
            keepDrawing = DrawDie();
        }
    }

    public bool DrawDie()
    {
        if (dices.Count < maxDiceCount)
        {
            dices.Add(defaultDice);
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

        dices.RemoveAt(0);
        canUseRoll = true;
    }
}
