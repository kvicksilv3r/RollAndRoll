using UnityEngine;
using UnityEngine.Events;

public class PurgatoryEventBus : MonoBehaviour
{
    public static PurgatoryEventBus Instance;

    public UnityEvent m_PassedGo;

    private void Awake()
    {
        Instance = this;
    }
}
