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

    public int ActivateDice(DiceStats activatedDice)
    {
        int roll = 0;
        foreach (var effect in activatedDice.effects)
        {
            switch (effect)
            {
                case DiceEffect.Move:
                   roll = Move(activatedDice);
                    break;
                case DiceEffect.Death:
                    roll = Death(activatedDice);
                    break;
                case DiceEffect.Gold:
                    roll = Gold(activatedDice);
                    break;
                case DiceEffect.Heal:
                    roll = Heal(activatedDice);
                    break;
                default:
                    print("Broken effect on " + activatedDice.name);
                    return 0;
            }
        }

        return roll;
    }

    private int Move(DiceStats dice)
    {
        var roll = RollDie(dice);
        PlayerMovementController.Instance.InitiateMove(roll);
        return roll;
    }

    private int Death(DiceStats dice)
    {
        var roll = RollDie(dice);
        RunHealthController.Instance.TakeDamage(roll);
        return roll;
    }

    private int Gold(DiceStats dice)
    {
        var roll = RollDie(dice);
        return roll;
    }

    private int Heal(DiceStats dice)
    {
        var roll = RollDie(dice);
        RunHealthController.Instance.TakeHealing(roll);
        return roll;
    }

    private int RollDie(DiceStats dice)
    {
        return dice.sides[Random.Range(0, dice.sides.Length)];
    }
}
