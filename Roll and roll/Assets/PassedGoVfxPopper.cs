using UnityEngine;

public class PassedGoVfxPopper : MonoBehaviour
{
    public ParticleSystem vfx;

    private void Start()
    {
        if (vfx == null)
        {
            vfx = GetComponentInChildren<ParticleSystem>();
        }

        if (vfx != null)
        {
            print("Popper has no VFX");
        }

        PurgatoryEventBus.Instance.m_PassedGo.AddListener(PopVfx);
    }

    public void PopVfx()
    {
        if (vfx == null)
        {
            return;
        }

        vfx.Play();
    }
}
