using DG.Tweening.Core.Easing;
using UnityEngine;
using UnityEngine.Events;

public class EventBus : MonoBehaviour
{
    public static EventBus Instance;

    public UnityEvent m_AssembleBagEquippedDiceClicked;

    private void Awake()
    {
        Instance = this;
    }
}
