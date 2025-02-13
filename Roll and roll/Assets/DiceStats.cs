using UnityEngine;

[CreateAssetMenu(fileName = "DiceStats", menuName = "Scriptable Objects/DiceStats")]
public class DiceStats : ScriptableObject
{
    public string displayName;
    public string description;
    public int[] sides;
    public Rarity rarity;
}
