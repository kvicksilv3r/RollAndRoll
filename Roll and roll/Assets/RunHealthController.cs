using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;


public class RunHealthController : MonoBehaviour
{
    public int startingHealth = 5;
    public int currentHealth;

    public static RunHealthController Instance;

    public TextMeshProUGUI lifeCountTMP;

    public Texture2D brokenHeart;
    public Texture2D heart;

    public RawImage heartVisual;

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

    public void SetupHealth()
    {
        //TODO:
        //Read starting health from config.
        currentHealth = startingHealth;
        UpdateHpVisual();
    }

    public void TakeDamage(int damage = 1)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);
        UpdateHpVisual();
        CheckDeath();
    }

    public void TakeHealing(int healing = 1)
    {
        currentHealth = Mathf.Clamp(currentHealth + healing, 1, startingHealth);
        UpdateHpVisual();
        CheckDeath();
    }

    public void UpdateHpVisual()
    {
        lifeCountTMP.text = currentHealth.ToString();
    }

    private void CheckDeath()
    {
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        heartVisual.texture = brokenHeart;
        DiceRunController.Instance.StopTheShow();

    }
}
