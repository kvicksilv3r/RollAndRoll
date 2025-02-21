using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RarityColorMap", menuName = "Scriptable Objects/RarityColorMap")]
public class RarityColorMap : ScriptableObject
{
    public List<RarityColor> map = new List<RarityColor>();
}

public class RarityColor
{
    public Rarity rarity;
    public Color color;
}
