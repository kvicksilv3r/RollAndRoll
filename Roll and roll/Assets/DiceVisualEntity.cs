using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DiceVisualEntity : MonoBehaviour, IPointerDownHandler
{
	public TextMeshProUGUI diceNameTMP;
	public DiceStats dice;

	public GameObject selected;

	public BagBuilding bagBuilder;

	public void SetupDice(DiceStats newDice, BagBuilding theBagBuilder)
	{
		dice = newDice;
		bagBuilder = theBagBuilder;

		diceNameTMP.text = dice.displayName;
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
		bagBuilder.ShowDiceInfo(dice);
		selected.SetActive(true);
	}

	private void SetDeselected()
	{
		bagBuilder.RemoveDiceInfo();
		selected.SetActive(false);
		//dice info hide dice
	}
}
