using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopDiceEntity : MonoBehaviour, IPointerClickHandler
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
            ShopController.Instance.DiceClickedOn(this);
            return;
        }

        print("bink bonk");
        print(eventData.rawPointerPress.gameObject);

        if (eventData.rawPointerPress == deselectObject)
        {
            return;
        }

        if (isSelected)
        {
            SetDeselected();
            return;
        }

        else
        {
            ShopController.Instance.DiceClickedOn(this);
            SetSelected();
            return;
        }
    }

    private void SetSelected()
    {
        transform.SetSiblingIndex(0);
        selected.SetActive(true);
        ShopController.Instance.SetDiceDescription(myDice);
        isSelected = true;
    }

    public void SetDeselected()
    {
        selected.SetActive(false);
        isSelected = false;
        ShopController.Instance.RemoveDiceDescription();
    }

    public void SetConsumed()
    {
        consumed = true;
        diceNameTMP.text = "Sold\nOut";
    }

    private void OnDestroy()
    {
        if (isSelected)
        {
            SetDeselected();
        }
    }
}