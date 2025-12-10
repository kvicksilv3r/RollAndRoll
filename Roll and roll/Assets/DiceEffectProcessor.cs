using System.Collections.Generic;
using UnityEngine;

public class DelayedDiceEffectEntity
{
    public DiceEffect effect;
    public int roll;
}

public class DiceEffectProcessor : MonoBehaviour
{
    public static DiceEffectProcessor Instance;
    public Queue<DelayedDiceEffectEntity> delayedDiceEffects = new Queue<DelayedDiceEffectEntity>();

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }

        if (Instance != this)
        {
            print($"Too many {this}, killing myself");
            Destroy(this);
        }
    }

    private void Start()
    {
        PurgatoryEventBus.Instance.m_ActivateEvent.AddListener(ConsumeQueuedEffect);
    }

    public int ActivateDice(DiceStats activatedDice, bool delay)
    {
        return ActivateDice(activatedDice, activatedDice.effects, delay);
    }

    public int ActivateDice(DiceStats activatedDice, DiceEffect[] effects, bool delay)
    {
        var roll = 0;
        foreach (var effect in effects)
        {
            roll = RollDie(activatedDice);

            if (delay)
            {
                DelayedDiceEffectEntity dde = new DelayedDiceEffectEntity();
                dde.effect = effect;
                dde.roll = roll;
                delayedDiceEffects.Enqueue(dde);
            }

            else
            {
                ActivateDiceEffect(effect, roll);
            }
        }

        return roll;
    }

    private void ConsumeQueuedEffect()
    {
        var e = delayedDiceEffects.Dequeue();
        ActivateDiceEffect(e.effect, e.roll);
    }

    private void ActivateDiceEffect(DiceEffect effect, int roll)
    {
        switch (effect)
        {
            case DiceEffect.Move:
                Move(roll);
                break;
            case DiceEffect.Death:
                Death(roll);
                break;
            case DiceEffect.Gold:
                Gold(roll);
                break;
            case DiceEffect.Heal:
                Heal(roll);
                break;
            case DiceEffect.None:
                roll = 0;
                break;
            default:
                print("Broken effect on some dice idk");
                break;
        }
    }

    private void Move(int roll)
    {
        PlayerMovementController.Instance.InitiateMove(roll);
    }

    private void Death(int roll)
    {
        RunHealthController.Instance.TakeDamage(roll);
    }

    private void Gold(int roll)
    {

    }

    private void Heal(int roll)
    {
        RunHealthController.Instance.TakeHealing(roll);
    }

    private int RollDie(DiceStats dice)
    {
        return dice.sides[Random.Range(0, dice.sides.Length)];
    }
}
