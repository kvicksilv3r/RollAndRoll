using UnityEngine;

[CreateAssetMenu(fileName = "DiceStats", menuName = "Scriptable Objects/DiceStats")]
public class DiceStats : ScriptableObject
{
    public string displayName;
    public string description;
    public int[] sides;
    public Rarity rarity;
    public DiceEffect[] effects;
    public string UID;
    [ColorUsage(false)]
    public Color rollResultColor;

    public DiceEffect[] discardEffects;

    public bool exileOnPlay = false;
    public bool exileOnDiscard = false;

    private void OnValidate()
    {
#if UNITY_EDITOR
        if (UID == "")
        {
            UID = UnityEditor.GUID.Generate().ToString();
            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif
    }
}
