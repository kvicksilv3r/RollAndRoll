using UnityEngine;
using TMPro;

public class DiceDescriptionController : MonoBehaviour
{
    public TextMeshProUGUI diceNameTMP;
    public TextMeshProUGUI diceDescriptionTMP;
    public TextMeshProUGUI diceOutcomesTMP;

    public GameObject nameObject;
    public GameObject descriptionObject;
    public GameObject outcomeObject;

    public GameObject exileOnPlay;
    public GameObject exileOnDiscard;

    public void SetDisplayedInfo(bool name, bool description, bool outcomes)
    {
        nameObject.SetActive(name);
        descriptionObject.SetActive(description);
        outcomeObject.SetActive(outcomes);
    }

    public void SetDiceDescription(DiceStats dice)
    {
        diceNameTMP.text = dice.displayName;

        diceDescriptionTMP.text = dice.description;

        diceOutcomesTMP.text = string.Join("  ", dice.sides);

        exileOnPlay.SetActive(dice.exileOnPlay);
        exileOnDiscard.SetActive(dice.exileOnDiscard);
    }

    public void ClearDiceDescription()
    {
        diceNameTMP.text = "";

        diceDescriptionTMP.text = "";

        diceOutcomesTMP.text = "";

        exileOnPlay.SetActive(false);
        exileOnDiscard.SetActive(false);
    }
}
