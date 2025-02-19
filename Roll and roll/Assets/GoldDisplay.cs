using TMPro;
using UnityEngine;

public class GoldDisplay : MonoBehaviour
{
    public TextMeshProUGUI goldTMPRO;

    void Start()
    {
        UpdateGold();
    }

    public void UpdateGold()
    {
        var gold = GoldHelper.Instance.GetPlayerGold();

        goldTMPRO.text = gold.ToString();
    }
}
