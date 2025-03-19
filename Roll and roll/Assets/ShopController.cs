using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ShopController : MonoBehaviour
{
    public TextAsset pricesCSV;

    public int uidIndex;
    public int priceIndex;

    public List<DiceStats> availableDice = new List<DiceStats>();
    public List<ShopDiceEntity> visualShopDice = new List<ShopDiceEntity>();

    public static ShopController Instance;

    public TextMeshProUGUI diceTitleTMP;
    public TextMeshProUGUI diceDescriptionTMP;

    public TextMeshProUGUI costTMP;
    public TextMeshProUGUI ownedGoldTMP;

    public GameObject shopPanel;

    public DiceStats selectedDice;

    public void OpenShop()
    {
        if (shopPanel.activeInHierarchy)
        {
            return;
        }

        SetupShop();

        shopPanel.SetActive(true);
    }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
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
        var ownedDice = DiceBagHelper.Instance.GetPlayerDiceBag();
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
        //if can buy, buy else nah
        //update gold 
    }

    internal void DiceClickedOn(ShopDiceEntity shopDiceEntity)
    {
        foreach (var shopDie in visualShopDice)
        {
            shopDie.SetDeselected();
        }
    }

    internal void SetDiceDescription(DiceStats myDice)
    {
        diceTitleTMP.text = myDice.name;
        diceDescriptionTMP.text = myDice.description;
    }

    public void RemoveDiceDescription()
    {
        diceTitleTMP.text = "";
        diceDescriptionTMP.text = "";
    }
}
