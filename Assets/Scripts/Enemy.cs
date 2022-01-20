using UnityEngine;

public class Enemy : MonoBehaviour
{
    [System.Serializable]
    public class EnemyStats
    {
        public int maxHealth = 100;
        private int _curHealth;
        public int curHealth
        {
            get { return _curHealth; }
            set {_curHealth= Mathf.Clamp(value, 0, maxHealth); }
        }
        public int damage = 40;
        public void Init()
        {
            curHealth = maxHealth;
        }
    }
    public EnemyStats enemyStats = new EnemyStats();
    public float SkeAmt = 0.1f;
    public float ShkeLenth = 0.1f;
    public string deathSound="Explosion";
    public Transform EnemyDeathParticles;
    public StatusIndicator statusIndicator;
    public int moneyDrop = 10;
    private void Start()
    {
        enemyStats.Init();
        if (statusIndicator!=null)
        {
            statusIndicator.SetHealth(enemyStats.curHealth, enemyStats.maxHealth);
        }
    }

    public void DamageEnemy(int damage)
    {
        enemyStats.curHealth -= damage;
        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(enemyStats.curHealth, enemyStats.maxHealth);
        }
        if (enemyStats.curHealth <= 0)
        {
            GameManger.KillEnemy(this);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag=="Player")
        {
            collision.collider.GetComponent<Player>().DamagePlayer(enemyStats.damage);
            DamageEnemy(9999);
        }
    }
}
