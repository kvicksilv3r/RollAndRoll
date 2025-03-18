using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class DiceRunController : MonoBehaviour
{
    public DiceBag playerBag;
    public DiceBag dicePool;
    public DiceBag diceToPlayBag;

    //Death dice to be added 
    public int deathDiceCount;
    // How many more death dice gets added per refill
    public int deathDieIncrease;
    public DiceStats deathDie;

    public DiceStats drawnDice;

    public int maximumDiceToPlay = 4;

    public static DiceRunController Instance;

    public TextMeshProUGUI diceInBagTMP;

    public List<GameObject> visualDice = new List<GameObject>();

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        StartTheShow();
    }

    public void StartTheShow()
    {
        SetupBags();
        RefillBag();
        SetupHealth();
    }

    private void RefillBag()
    {
        SetupBags();
        dicePool.Add(playerBag);
        AddDeathDice();
    }

    private void AddDeathDice()
    {
        for (int i = 0; i < deathDiceCount; i++)
        {
            dicePool.bag.Add(deathDie);
        }

        deathDiceCount += deathDieIncrease;
    }

    public void StopTheShow()
    {
        //gg
    }

    private void SetupBags()
    {
        playerBag = DiceBagHelper.Instance.GetPlayerDiceBag();
        dicePool = new DiceBag();
    }

    public void RefillHand()
    {
        EmptyHand();

        for (int i = 0; i < maximumDiceToPlay; i++)
        {
            DrawDice();
        }


    }

    private void EmptyHand()
    {
        diceToPlayBag.bag.Clear();

        foreach (var dice in visualDice)
        {
            dice.GetComponent<IngameDiceVisualEntity>().SetConsumed();
        }
    }

    public void DrawDice()
    {
        if (diceToPlayBag.bag.Count >= maximumDiceToPlay || dicePool.bag.Count == 0)
        {
            return;
        }

        var chosen = dicePool.Get()[Random.Range(0, dicePool.Count())];
        diceToPlayBag.Add(chosen);
        dicePool.Remove(chosen);

        UpdateVisualDice(diceToPlayBag.Count());
        UpdateDiceAmountText();
    }

    private void UpdateVisualDice(int diceIndex = -1)
    {
        if (diceIndex == -1)
        {
            for (int i = 0; i < diceToPlayBag.Count(); i++)
            {
                UpdateVisualDice(i);
            }
        }

        else
        {
            diceIndex--;
            visualDice[diceIndex].GetComponent<IngameDiceVisualEntity>().SetupDice(diceToPlayBag.bag[diceIndex]);
        }
    }

    public void ActiveRedraw()
    {
        //idk tbh
    }

    public void UpdateDiceAmountText()
    {
        diceInBagTMP.text = dicePool.Count().ToString();
    }

    public void SetupHealth()
    {
        RunHealthController.Instance.SetupHealth();
    }
}
