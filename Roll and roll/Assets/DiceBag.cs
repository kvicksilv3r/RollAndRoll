using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DiceBag
{
    public List<DiceStats> bag = new List<DiceStats>();

    public List<DiceStats> Get()
    {
        return bag;
    }

    public void Add(DiceStats diceToAdd)
    {
        bag.Add(diceToAdd);
    }

    public void Add(DiceBag toAdd)
    {
        foreach (var dice in toAdd.bag)
        {
            Add(dice);
        }
    }

    public void Remove(DiceStats diceToRemove)
    {
        bag.Remove(diceToRemove);
    }

    public void Remove(int removeAt)
    {
        bag.RemoveAt(removeAt);
    }

    public int Count()
    {
        return bag.Count;
    }
}
