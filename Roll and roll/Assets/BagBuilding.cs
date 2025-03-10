using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;

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

    public GameObject DiceEntity;

    public TextMeshProUGUI diceNameTMPRO;
    public TextMeshProUGUI diceDescriptionTMPRO;
    public TextMeshProUGUI diceCountTMPRO;

    public DiceStats selectedDice;

    public int maxBagSize = 12;

    private void Awake()
    {
        RemoveDiceInfo();
    }

    public void MakeAndSaveBag()
    {
        bag = DiceCollectionHelper.Instance.GetUnlockedDice();

        DiceBagHelper.Instance.SaveDiceBag(bag);
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
            Destroy(child.gameObject);
        }

        foreach (var dice in currentBag.bag)
        {
            if (DiceAlreadyActiveInBag(dice))
            {
                AddCountToDice(dice);
            }

            else
            {
                var instantiatedDice = Instantiate(DiceEntity, currentBagPanel.transform);
                instantiatedDice.GetComponent<DiceVisualEntity>().SetupDice(dice, this);
            }
        }
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

    private void AddCountToDice(DiceStats dice)
    {
        var currentActiveBagDice = currentBagPanel.GetComponentsInChildren<DiceVisualEntity>();

        foreach (var equippedDice in currentActiveBagDice)
        {
            if (equippedDice.dice.UID == dice.UID)
            {
                //equippedDice.SetAmount()
            }
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
        diceNameTMPRO.text = dice.displayName;
        diceDescriptionTMPRO.text = dice.description;

        SetAddRemoveButtons(true);
    }

    public void RemoveDiceInfo()
    {
        diceNameTMPRO.text = "";
        diceDescriptionTMPRO.text = "";

        SetAddRemoveButtons(false);
    }

    public void SetAddRemoveButtons(bool active)
    {
        foreach (var button in addRemovePanel.GetComponentsInChildren<Button>())
        {
            button.interactable = active;
        }
    }

    private bool DiceAlreadyActiveInBag(DiceStats dice)
    {
        var currentActiveBagDice = currentBagPanel.GetComponentsInChildren<DiceVisualEntity>();

        if (currentActiveBagDice.Length <= 0)
        {
            return false;
        }

        foreach (var equippedDice in currentActiveBagDice)
        {
            if (equippedDice.dice.UID == dice.UID)
            {
                return true;
            }
        }

        return false;
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
                if (tempBag[o].dice.UID == bag.bag[i].UID)
                {
                    tempBag[o].amount++;
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

        foreach (var tempCombo in tempBag)
        {
            if (tempCombo.dice.UID == selectedDice.UID)
            {
                tempCombo.amount++;
                UpdateDiceCountText();
                return;
            }
        }

        var newTempCombo = new BagBuildingTempCombo(selectedDice, 1);
        tempBag.Add(newTempCombo);
        UpdateDiceCountText();
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
                    return;
                }

                UpdateDiceCountText();
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

    private void UpdateDiceCountText()
    {
        diceCountTMPRO.text = $"{GetDiceCount()} / {maxBagSize}";
    }

    private void UpdateVisualBag()
    {

    }

}
