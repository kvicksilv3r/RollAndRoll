using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class RunHealthController : MonoBehaviour
{
    public int startingHealth = 5;
    public int currentHealth;

    public static RunHealthController Instance;

    public Texture2D brokenHeart;
    public Texture2D heart;

    public List<GameObject> heartObjects = new List<GameObject>();

    private void Awake()
    {
        if (Instance != null)
        {
            print("TOO MANY " + this.name);
            Destroy(this);
        }

        Instance = this;
    }

    public void SetupHealth()
    {
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
        for (int i = 0; i < heartObjects.Count; i++)
        {
            heartObjects[i].GetComponent<RawImage>().texture = i < currentHealth ? heart : brokenHeart;
        }
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
        DiceRunController.Instance.StopTheShow();
    }
}
