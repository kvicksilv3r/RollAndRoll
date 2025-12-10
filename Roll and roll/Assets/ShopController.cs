using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;

public class ShopController : MonoBehaviour
{
    public TextAsset pricesCSV;

    public int uidIndex;
    public int priceIndex;

    public List<DiceStats> availableDice = new List<DiceStats>();
    public List<ShopDiceEntity> visualShopDice = new List<ShopDiceEntity>();

    public static ShopController Instance;

    public DiceDescriptionController descriptionController;

    public TextMeshProUGUI costTMP;
    public TextMeshProUGUI ownedGoldTMP;

    public GameObject shopPanel;

    public DiceStats selectedDice;

    private int currentCost = 0;

    public void OpenShop()
    {
        if (shopPanel.activeInHierarchy)
        {
            return;
        }

        SetupShop();
        SetupDescription();
        RemoveDiceDescription();

        shopPanel.SetActive(true);
    }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }

        if (Instance != this)
        {
            print($"Too many {this}, killing myself");
            Destroy(this);
        }
    }

    private void SetupShop()
    {
        SetupWares();
        UpdateGold();
    }

    private void UpdateGold()
    {
        ownedGoldTMP.text = GoldHelper.Instance.GetPlayerGold().ToString();
    }

    private void SetupWares()
    {
        var ownedDice = DiceCollectionHelper.Instance.GetUnlockedDice();
        var numOfDice = Mathf.Min(availableDice.Count, visualShopDice.Count);

        for (int i = 0; i < numOfDice; i++)
        {
            visualShopDice[i].SetupDice(availableDice[i]);

            foreach (var ownedDie in ownedDice.bag)
            {
                if (ownedDie.UID == availableDice[i].UID)
                {
                    visualShopDice[i].SetConsumed();
                }
            }
        }
    }

    public void Buy()
    {
        if (!selectedDice)
        {
            return;
        }

        if (GoldHelper.Instance.GetPlayerGold() < currentCost)
        {
            return;
        }

        GoldHelper.Instance.RemovePlayerGold(currentCost);

        DiceCollectionHelper.Instance.UnlockNewDice(selectedDice);

        DiceClickedOn();

        DiceDeselected();

        RemoveDiceDescription();

        UpdateCost();

        UpdateGold();

        SetupWares();
    }

    public int FetchCost()
    {
        var raw = pricesCSV.text.Split('\n');

        for (int i = 1; i < raw.Length; i++)
        {
            var choppedData = raw[i].Split(',');
            if (choppedData[uidIndex] == selectedDice.UID)
            {
                return int.Parse(choppedData[priceIndex]);
            }
        }

        return 9999;
    }

    public void UpdateCost()
    {
        if (selectedDice)
        {
            costTMP.text = currentCost.ToString();
        }

        else
        {
            costTMP.text = "-";
        }
    }

    internal void DiceClickedOn()
    {
        foreach (var shopDie in visualShopDice)
        {
            shopDie.SetDeselected();
        }
    }

    private void SetupDescription()
    {
        descriptionController.SetDisplayedInfo(true, true, true);
    }

    public void SetSelectedDice(DiceStats myDice)
    {
        selectedDice = myDice;
        SetDiceDescription(myDice);
        currentCost = FetchCost();
        UpdateCost();
    }

    public void SetDiceDescription(DiceStats myDice)
    {
        descriptionController.SetDiceDescription(myDice);
    }

    public void RemoveDiceDescription()
    {
        descriptionController.ClearDiceDescription();
    }

    internal void DiceDeselected()
    {
        selectedDice = null;
        UpdateCost();
        RemoveDiceDescription();
    }
}
