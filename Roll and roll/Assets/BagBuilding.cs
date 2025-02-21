using UnityEngine;
using TMPro;

public class BagBuilding : MonoBehaviour
{
	public DiceBag bag = new DiceBag();

	public DiceBag tempEquippedBag = new DiceBag();

	public GameObject bagPanel;

	public GameObject currentBagPanel;
	public GameObject availableDicePanel;

	public GameObject DiceEntity;

	public TextMeshProUGUI diceNameTMPRO;
	public TextMeshProUGUI diceDescriptionTMPRO;

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

		foreach (Transform child in currentBagPanel.transform)
		{
			Destroy(child.gameObject);
		}

		foreach (var dice in currentBag.bag)
		{
			var instantiatedDice = Instantiate(DiceEntity, currentBagPanel.transform);
			instantiatedDice.GetComponent<DiceVisualEntity>().SetupDice(dice, this);
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

	public void ShowDiceInfo(DiceStats dice)
	{
		diceNameTMPRO.text = dice.displayName;
		diceDescriptionTMPRO.text = dice.description;
	}

	public void RemoveDiceInfo()
	{
		diceNameTMPRO.text = "";
		diceDescriptionTMPRO.text = "";
	}
}
