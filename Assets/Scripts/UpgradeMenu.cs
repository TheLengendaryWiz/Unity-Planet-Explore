using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    public float healthMultiplier=1.3f;
    public float speedMultiplier = 1.3f;
    public Text healthText;
    public Text speedText;
    PlayerStats stats;
    public int upgradeCost = 50;
    private void Start()
    {
        
        UpdateValues();
    }
    void UpdateValues()
    {
        if (stats==null)
        {
            print("why null?");
        }
        healthText.text = "health : " + stats.maxHealth;
        speedText.text = "speed : " + stats.movementSpeed;

    }
    private void OnEnable()
    {
        UpdateValues();
    }
    private void Awake()
    {
        stats = PlayerStats.instance;
    }
    public void UpgradeHealth()
    {
        if (GameManger.money<upgradeCost)
        {
            AudioManager.instance.PlaySound("NoMoney");
            return;
        }
        if (stats.maxHealth>=1500)
        {
            StartCoroutine(QuickUpdate(healthText));
            return;
        }
        stats.maxHealth = (int)(stats.maxHealth*healthMultiplier);
        stats.curHealth = stats.maxHealth;
        GameManger.money -= upgradeCost;
        UpdateValues();
        AudioManager.instance.PlaySound("Money");
    }
    public void UpgradeSpeed()
    {
        if (GameManger.money < upgradeCost)
        {
            AudioManager.instance.PlaySound("NoMoney");
            return;
        }
        if (stats.movementSpeed >= 25)
        {
            StartCoroutine(QuickUpdate(speedText));
            return;
        }
        stats.movementSpeed = (int)(stats.movementSpeed * speedMultiplier);
        GameManger.money -= upgradeCost;
        UpdateValues();
        AudioManager.instance.PlaySound("Money");
    }
    IEnumerator QuickUpdate(Text text)
    {
        text.text = "limit reached";
        yield return new WaitForSeconds(3.5f);
        UpdateValues();
    }
}