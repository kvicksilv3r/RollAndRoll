using System;
using System.Collections;
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

    public float rollAnimationTime = 0.5f;
    public float timeBetweenAnimationRolls = 0.05f;

    public GameObject deselectObject;

    private Color baseTextColor;

    private void Awake()
    {
        baseTextColor = diceNameTMP.color;
    }

    public void SetupDice(DiceStats newDice)
    {
        StopAllCoroutines();
        consumed = false;
        myDice = newDice;
        diceNameTMP.text = myDice.displayName;
        diceNameTMP.color = baseTextColor;
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
        var results = DiceEffectProcessor.Instance.ActivateDice(myDice);
        StartCoroutine(ResultsGoBrr(results));
    }

    IEnumerator ResultsGoBrr(int rollResult)
    {
        float currentTime = 0;

        diceNameTMP.color = myDice.rollResultColor;
        diceNameTMP.text = RandomNumber().ToString();

        while (currentTime < rollAnimationTime)
        {
            yield return new WaitForSeconds(timeBetweenAnimationRolls);
            currentTime += timeBetweenAnimationRolls;
            diceNameTMP.text = RandomNumber().ToString();
        }

        diceNameTMP.text = rollResult.ToString();

        yield return new WaitForSeconds(0.5f);

        MarkConsumed();
    }

    private int RandomNumber()
    {
        return myDice.sides[UnityEngine.Random.Range(0, myDice.sides.Length)];
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
        if (!myDice)
        {
            return;
        }

        consumed = true;

        //Exile dice
        if (myDice.exileOnPlay)
        {
            DiceRunController.Instance.RemoveDiceFromPlayerBag(myDice);
        }
    }

    public void MarkConsumed()
    {
        diceNameTMP.color = baseTextColor;
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