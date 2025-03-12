using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DiceVisualEntity : MonoBehaviour, IPointerDownHandler
{
    public TextMeshProUGUI diceNameTMP;
    public TextMeshProUGUI diceCountTMP;
    public DiceStats dice;

    public GameObject selected;
    public GameObject countPanel;

    public BagBuilding bagBuilder;

    private bool isSelected = false;

    public void SetupDice(DiceStats newDice, BagBuilding theBagBuilder, int amount = 1)
    {
        dice = newDice;
        bagBuilder = theBagBuilder;

        diceNameTMP.text = dice.displayName;

        SetAmount();
    }

    public void SetAmount(int amount = 1)
    {
        if (amount > 1)
        {
            countPanel.SetActive(true);
            diceCountTMP.text = "x" + amount;
        }

        else
        {
            countPanel.SetActive(false);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        print("bink bonk");

        if (selected.activeInHierarchy)
        {
            SetDeselected();
            return;
        }

        else
        {
            SetSelected();
            return;
        }
    }

    private void SetSelected()
    {
        bagBuilder.SetSelectedDice(dice);
        selected.SetActive(true);
        isSelected = true;
    }

    private void SetDeselected()
    {
        bagBuilder.DeselectDice();
        selected.SetActive(false);
        isSelected = false;
    }

    private void OnDestroy()
    {
        if (isSelected)
        {
            SetDeselected();
        }
    }
}
