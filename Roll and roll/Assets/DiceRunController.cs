using System.Collections.Generic;
using TMPro;
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

    public List<IngameDiceVisualEntity> visualDiceEntities = new List<IngameDiceVisualEntity>();

    public TextMeshProUGUI diceDescriptionTMP;
    public TextMeshProUGUI diceOutcomesTMP;

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

        if (dicePool.bag.Count == 0)
        {
            RefillBag();
            RefillHand();
        }

        for (int i = 0; i < maximumDiceToPlay; i++)
        {
            DrawDice();
        }
    }

    private void EmptyHand()
    {
        diceToPlayBag.bag.Clear();

        foreach (var visualDie in visualDiceEntities)
        {
            visualDie.SetConsumed();
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
            visualDiceEntities[diceIndex].SetupDice(diceToPlayBag.bag[diceIndex]);
        }
    }

    public void ActiveRedraw()
    {
        //idk tbh
    }

    public void DiceClickedOn(IngameDiceVisualEntity gotClicked)
    {
        foreach (var visualDie in visualDiceEntities)
        {
            visualDie.SetDeselected();
        }
    }

    public void SetDiceDescription(DiceStats dice)
    {
        diceDescriptionTMP.text = dice.description;

        diceOutcomesTMP.text = string.Join("  ", dice.sides);
    }

    public void RemoveDiceDescription()
    {
        diceDescriptionTMP.text = "";

        diceOutcomesTMP.text = "";
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
