using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class IngameDiceVisualEntity : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI diceNameTMP;
    public DiceStats myDice;

    public GameObject selected;

    private bool isSelected = false;

    public bool consumed = true;

    public GameObject deselectObject;

    public void SetupDice(DiceStats newDice)
    {
        consumed = false;
        myDice = newDice;
        diceNameTMP.text = myDice.displayName;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (consumed)
        {
            DiceRunController.Instance.DiceClickedOn(this);
            return;
        }

        print("bink bonk");
        print(eventData.rawPointerPress.gameObject);

        if (eventData.rawPointerPress == deselectObject)
        {
            SetDeselected();
            return;
        }

        if (isSelected)
        {
            UseDice();
            return;
        }

        else
        {
            DiceRunController.Instance.DiceClickedOn(this);
            SetSelected();
            return;
        }
    }

    private void UseDice()
    {
        SetConsumed();
        SetDeselected();
        DiceEffectProcessor.Instance.ActivateDice(myDice);
    }

    private void SetSelected()
    {
        transform.SetSiblingIndex(0);
        selected.SetActive(true);
        DiceRunController.Instance.SetDiceDescription(myDice);
        isSelected = true;
    }

    public void SetDeselected()
    {
        selected.SetActive(false);
        isSelected = false;
        DiceRunController.Instance.RemoveDiceDescription();
    }

    public void SetConsumed()
    {
        consumed = true;
        diceNameTMP.text = "Consumed";
    }

    private void OnDestroy()
    {
        if (isSelected)
        {
            SetDeselected();
        }
    }
}