using UnityEngine;

public class Player : MonoBehaviour
{
    
	AudioManager audioManager;
	public int fallBoundary = -20;

	[SerializeField]
	private StatusIndicator statusIndicator;
	private PlayerStats stats;

	void Start()
	{
		stats=PlayerStats.instance;
		stats.curHealth = stats.maxHealth;
		if (statusIndicator == null)
		{
			Debug.LogError("No status indicator referenced on Player");
		}
		else
		{
			statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
		}
		audioManager=AudioManager.instance;
		InvokeRepeating("regenHealth",1f/stats.HealthregenRate,stats.HealthregenRate);
	}
	void regenHealth()
    {
		stats.curHealth += 1;
		statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
    }
	void Update()
	{
		if (transform.position.y <= fallBoundary)
			DamagePlayer(9999999);
	}

	public void DamagePlayer(int damage)
	{
		stats.curHealth -= damage;
		if (stats.curHealth <= 0)
		{
			audioManager.PlaySound("DeathSound");
			GameManger.KillPlayer(this);
		}
        else
        {
			audioManager.PlaySound("Grunt");

		}
		statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
	}

}