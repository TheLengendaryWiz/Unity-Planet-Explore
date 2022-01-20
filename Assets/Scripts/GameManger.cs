using System.Collections;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public GameObject upgradeMenu;
    public static GameManger gm;
    public Transform PlayerPrefab;
    public Transform SpawnPoint;
    public Transform spawnPrefab;
    public CameraShake cameraShake;
    public GameObject GameOverUI;
    AudioManager audioManager;
    private void Awake()
    {
        if(gm==null)
            gm = this;
    }
    public int maxLives=3;
    private static int remainingLives = 3;
    public static int money;
    public int startMoney;
    private void Start()
    {
        remainingLives = maxLives;
        money = startMoney;
        audioManager = AudioManager.instance;
    }
    public static int RemainingLives
    {
        get { return remainingLives; }
    }
    public IEnumerator RespawnPlayer()
    {
        audioManager.PlaySound("RespawnCountdown");
        yield return new WaitForSeconds(3.3f);
        audioManager.PlaySound("Spawn");
        Instantiate(PlayerPrefab, SpawnPoint.position, SpawnPoint.rotation);
        GameObject clone = Instantiate(spawnPrefab, SpawnPoint.position, SpawnPoint.rotation).gameObject;
        Destroy(clone, 3f);
        print("Respawn");
    }
    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
        remainingLives -= 1;
        if (remainingLives<=0)
        {
            gm.EndGame();
            return;
        }
        gm.StartCoroutine(gm.RespawnPlayer());
    }
    public static void KillEnemy(Enemy enemy)
    {
        gm._KillEnemy(enemy);
    }
    public void _KillEnemy(Enemy _enemy)
    {
        audioManager.PlaySound(_enemy.deathSound);
        money += _enemy.moneyDrop;
        audioManager.PlaySound("Money");
        Destroy(_enemy.gameObject);
        cameraShake.Shake(_enemy.SkeAmt,_enemy.ShkeLenth);
        GameObject clone = Instantiate(_enemy.EnemyDeathParticles, _enemy.transform.position, Quaternion.identity).gameObject;
        Destroy(clone, 5f);
    }
    public void EndGame()
    {
        audioManager.PlaySound("GameOver");
        print("Game Over");
        GameOverUI.SetActive(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            ToggleUpdateMenu();
        }
    }
    void ToggleUpdateMenu()
    {
        upgradeMenu.SetActive(!upgradeMenu.activeSelf);
        if (upgradeMenu.activeSelf==true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}