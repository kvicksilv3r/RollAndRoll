using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class IngameDiceVisualEntity : MonoBehaviour, IPointerDownHandler
{
    public TextMeshProUGUI diceNameTMP;
    public DiceStats dice;

    public GameObject selected;

    private bool isSelected = false;

    public bool consumed = true;

    public void SetupDice(DiceStats newDice)
    {
        consumed = false;
        dice = newDice;
        diceNameTMP.text = dice.displayName;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (consumed)
        {
            return;
        }

        print("bink bonk");

        //use pointer up for some reason pointer down is broken.

        //if (eventData.rawPointerPress != gameObject)
        //{
        //    SetDeselected();
        //    return;
        //}

        if (selected.activeInHierarchy)
        {
            UseDice();
            return;
        }

        else
        {
            SetSelected();
            return;
        }
    }

    private void UseDice()
    {
        SetConsumed();
        //beep boop
    }

    private void SetSelected()
    {
        selected.SetActive(true);
        isSelected = true;
    }

    private void SetDeselected()
    {
        selected.SetActive(false);
        isSelected = false;
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