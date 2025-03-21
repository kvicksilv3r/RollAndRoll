using UnityEngine;

public class DiceEffectProcessor : MonoBehaviour
{
    public static DiceEffectProcessor Instance;

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

    public void ActivateDice(DiceStats activatedDice)
    {
        foreach (var effect in activatedDice.effects)
        {
            switch (effect)
            {
                case DiceEffect.Move:
                    Move(activatedDice);
                    break;
                case DiceEffect.Death:
                    Death(activatedDice);
                    break;
                case DiceEffect.Gold:
                    Gold(activatedDice);
                    break;
                case DiceEffect.Heal:
                    Heal(activatedDice);
                    break;
                default:
                    print("Broken effect on " + activatedDice.name);
                    return;
            }
        }
    }

    private void Move(DiceStats dice)
    {
        var roll = RollDie(dice);
        PlayerMovementController.Instance.InitiateMove(roll);
    }

    private void Death(DiceStats dice)
    {
        var roll = RollDie(dice);
        RunHealthController.Instance.TakeDamage(roll);
    }

    private void Gold(DiceStats dice)
    {
        var roll = RollDie(dice);
    }

    private void Heal(DiceStats dice)
    {
        var roll = RollDie(dice);
        RunHealthController.Instance.TakeHealing(roll);
    }

    private int RollDie(DiceStats dice)
    {
        return dice.sides[Random.Range(0, dice.sides.Length)];
    }
}
