using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiceCollectionHelper : MonoBehaviour
{
    protected List<DiceStats> diceCollection = new List<DiceStats>();
    public static DiceCollectionHelper Instance;

    private void Awake()
    {
        SetupCollection();

        Instance = this;
    }

    public List<DiceStats> GetDiceCollection()
    {
        return diceCollection;
    }

    private void SetupCollection()
    {
        if (diceCollection.Count > 0)
        {
            return;
        }

        diceCollection = Resources.LoadAll<DiceStats>("DiceData").ToList();
    }
}
