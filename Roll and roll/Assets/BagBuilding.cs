using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

[System.Serializable]
public class BagBuildingTempCombo
{
    public BagBuildingTempCombo(DiceStats newDice, int newAmount)
    {
        dice = newDice;
        amount = newAmount;
    }

    public DiceStats dice;
    public int amount;
}

public class BagBuilding : MonoBehaviour
{
    public DiceBag bag = new DiceBag();

    public List<BagBuildingTempCombo> tempBag = new List<BagBuildingTempCombo>();

    public GameObject bagPanel;

    public GameObject currentBagPanel;
    public GameObject availableDicePanel;
    public GameObject addRemovePanel;
    public GameObject saveButton;
    public GameObject addDiceButton;

    public GameObject DiceEntity;

    public TextMeshProUGUI diceCountTMPRO;

    public DiceStats selectedDice;

    public DiceDescriptionController descriptionController;

    public int maxBagSize = 12;
    public int maxDiceVariants = 6;

    private void Awake()
    {
        SetupDescription();
        RemoveDiceInfo();
    }

    private void SetupDescription()
    {
        descriptionController.SetDisplayedInfo(true, true, true);
    }

    public void OpenBagBuilding()
    {
        if (bagPanel.activeInHierarchy)
        {
            return;
        }

        SetupAvailableDice();

        SetupCurrentBag();

        bagPanel.SetActive(true);
    }

    private void SetupCurrentBag()
    {
        var currentBag = DiceBagHelper.Instance.GetPlayerDiceBag();

        bag = currentBag;
        tempBag = BagToTempBag();

        foreach (Transform child in currentBagPanel.transform)
        {
            DestroyImmediate(child.gameObject);
        }

        UpdateVisualBag();
        UpdateDiceCountText();
    }

    private void SetupAvailableDice()
    {
        var available = DiceCollectionHelper.Instance.GetUnlockedDice();

        foreach (Transform child in availableDicePanel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var dice in available.bag)
        {
            var instantiatedDice = Instantiate(DiceEntity, availableDicePanel.transform);
            instantiatedDice.GetComponent<DiceVisualEntity>().SetupDice(dice, this);
        }
    }

    public void SetSelectedDice(DiceStats selectedDice)
    {
        this.selectedDice = selectedDice;
        ShowDiceInfo(selectedDice);
    }

    public void DeselectDice()
    {
        selectedDice = null;
        RemoveDiceInfo();
    }

    public void ShowDiceInfo(DiceStats dice)
    {
        descriptionController.SetDiceDescription(dice);

        SetAddRemoveButtons(true);
    }

    public void RemoveDiceInfo()
    {
        descriptionController.RemoveDiceDescription();

        SetAddRemoveButtons(false);
    }

    public void SetAddRemoveButtons(bool active)
    {
        foreach (var button in addRemovePanel.GetComponentsInChildren<Button>())
        {
            button.interactable = active;
        }

        if (active)
        {
            addDiceButton.GetComponent<Button>().interactable = GetDiceCount() < maxBagSize;
        }
    }

    private DiceBag TempBagToDiceBag()
    {
        var returnbag = new DiceBag();

        foreach (var tempCombo in tempBag)
        {
            for (int i = 0; i < tempCombo.amount; i++)
            {
                returnbag.bag.Add(tempCombo.dice);
            }
        }

        return returnbag;
    }

    private List<BagBuildingTempCombo> BagToTempBag()
    {
        var returnTempBag = new List<BagBuildingTempCombo>();
        bool found = false;

        for (int i = 0; i < bag.bag.Count; i++)
        {
            found = false;

            for (int o = 0; o < returnTempBag.Count; o++)
            {
                if (returnTempBag[o].dice.UID == bag.bag[i].UID)
                {
                    returnTempBag[o].amount++;
                    found = true;
                }
            }

            if (!found)
            {
                returnTempBag.Add(new BagBuildingTempCombo(bag.bag[i], 1));
            }
        }

        return returnTempBag;
    }

    public void TryAddDice()
    {
        if (GetDiceCount() >= maxBagSize)
        {
            return;
        }

        if (GetDiceVariations() >= maxDiceVariants)
        {
            return;
        }

        foreach (var tempCombo in tempBag)
        {
            if (tempCombo.dice.UID == selectedDice.UID)
            {
                tempCombo.amount++;
                UpdateDiceCountText();
                UpdateVisualBag();
                SetCanSaveBag();
                SetAddRemoveButtons(true);
                return;
            }
        }

        var newTempCombo = new BagBuildingTempCombo(selectedDice, 1);
        tempBag.Add(newTempCombo);
        UpdateDiceCountText();
        UpdateVisualBag();
        SetCanSaveBag();
        SetAddRemoveButtons(true);
    }

    public void TryRemoveDice()
    {
        if (GetDiceCount() <= 0)
        {
            return;
        }

        foreach (var tempCombo in tempBag)
        {
            if (tempCombo.dice.UID == selectedDice.UID)
            {
                tempCombo.amount--;

                if (tempCombo.amount == 0)
                {
                    tempBag.Remove(tempCombo);
                    UpdateDiceCountText();
                    UpdateVisualBag();
                    SetCanSaveBag();
                    SetAddRemoveButtons(true);
                    return;
                }

                UpdateDiceCountText();
                UpdateVisualBag();
                SetCanSaveBag();
                SetAddRemoveButtons(true);
                return;
            }
        }
    }
    public int GetDiceCount()
    {
        var diceAmount = 0;

        foreach (var tempCombo in tempBag)
        {
            diceAmount += tempCombo.amount;
        }

        return diceAmount;
    }

    public int GetDiceVariations()
    {
        return tempBag.Count;
    }

    private void UpdateDiceCountText()
    {
        diceCountTMPRO.text = $"{GetDiceCount()} / {maxBagSize}";
    }

    private void UpdateVisualBag()
    {
        foreach (var tempDice in tempBag)
        {
            bool found = false;
            foreach (var shownDice in GetVisualDice())
            {
                if (tempDice.dice.UID == shownDice.dice.UID)
                {
                    found = true;

                    shownDice.SetAmount(tempDice.amount);

                    if (tempDice.amount == 0)
                    {
                        Destroy(shownDice.gameObject);
                    }

                    break;
                }
            }

            if (!found)
            {
                var d = Instantiate(DiceEntity, currentBagPanel.transform);
                d.GetComponent<DiceVisualEntity>().SetupDice(tempDice.dice, this, tempDice.amount);
            }
        }

        foreach (var shownDice in GetVisualDice())
        {
            var match = false;
            foreach (var tempDice in tempBag)
            {
                if (shownDice.dice.UID == tempDice.dice.UID)
                {
                    match = true;
                }
            }

            if (!match)
            {
                Destroy(shownDice.gameObject);
            }
        }
    }

    private DiceVisualEntity[] GetVisualDice()
    {
        return currentBagPanel.GetComponentsInChildren<DiceVisualEntity>();
    }

    public void SaveBag()
    {
        DiceBagHelper.Instance.SaveDiceBag(TempBagToDiceBag());
    }

    private void SetCanSaveBag()
    {
        saveButton.GetComponent<Button>().interactable = GetDiceCount() == maxBagSize;
    }
}
