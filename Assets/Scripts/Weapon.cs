using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float firerate = 0;
    public Transform muzzleFlashPrefab;
    public int damage = 10;
    public string weaponShootSound = "DefaultShot";
    public LayerMask WhatToHit;
    float TimeToFire = 0;
    Transform firePoint;
    public float ShakeAmt=0.05f;
    CameraShake cameraShake;
    public Transform bulletTrailPrefab;
    public float TimeToSpawnEffect=0;
    public float effectRate=10;
    AudioManager audioManager;
    public Transform hitPrefab;
    private void Awake()
    {
        firePoint = transform.Find("FirePoint");
    }
    void Shoot()
    {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPos = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D raycastHit= Physics2D.Raycast(firePointPos, mousePosition - firePointPos, 100f,WhatToHit);
        if (raycastHit.collider!=null)
        {
            Enemy enemy = raycastHit.collider.GetComponent<Enemy>();
            if (enemy!=null)
            {
                enemy.DamageEnemy(damage);
            }
        }
        if (Time.time >= TimeToSpawnEffect)
        {
            Vector3 hitPos;
            Vector3 hitNormal;
            if (raycastHit.collider==null)
            {
                hitPos = (mousePosition - firePointPos) * 30;
                hitNormal = new Vector3(999, 999, 999);
            }
            else
            {
                hitPos = raycastHit.point;
                hitNormal = raycastHit.normal;
            }
            Effect(hitPos,hitNormal);
            TimeToSpawnEffect = Time.time + 1 / effectRate;
        }
    }
    void Effect(Vector3 hitPos,Vector3 hitNormal)
    {
        Transform trail=Instantiate(bulletTrailPrefab, firePoint.position, firePoint.rotation);
        LineRenderer lr = trail.GetComponent<LineRenderer>();
        if (lr!=null)
        {
            lr.SetPosition(0, firePoint.position);
            lr.SetPosition(1, hitPos);
        }
        Destroy(trail.gameObject, 0.04f);
        if (hitNormal!=new Vector3(999,999,999))
        {
            GameObject hitParticles = Instantiate(hitPrefab,hitPos,Quaternion.FromToRotation(Vector3.right,hitNormal)).gameObject;
            Destroy(hitParticles, 1f);
        }
        Transform clone = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation,firePoint);
        float size = Random.Range(0.7f, 0.9f);
        clone.localScale = new Vector3(size, size, size);
        Destroy(clone.gameObject,0.02f);
        cameraShake.Shake(ShakeAmt, 0.1f);
        audioManager.PlaySound(weaponShootSound);
    }
    void Update()
    {
        if (GameManger.gm.upgradeMenu.activeSelf)
        {
            return;
        }
        if (firerate==0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1")&& Time.time > TimeToFire)
            {
                TimeToFire = Time.time + 1 / firerate;
                Shoot();
            }
        }
    }
    private void Start()
    {
        cameraShake = GameManger.gm.GetComponent<CameraShake>();
        audioManager = AudioManager.instance;
    }
}
