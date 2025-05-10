using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BirdMachineGun : BirdEnemy
{
    [SerializeField] private float attackRange;
    [SerializeField] private float attackTime = 0;
    [SerializeField] private float weaponDamage = 2;
    [SerializeField] private float bulletSpeed = 10;
    [SerializeField] private BulletSeed bulletPrefab;

    BirdEnemy birdEnemy;
    private Animator machineGunAnimator;

    private float timer = 0;

    private float setSpeed = 0;

    protected override void Start()
    {
        base.Start();
        health = new HealthSeed(1, 0, 1);
        setSpeed = speed;

        machineGunAnimator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (target == null)
            return;

        if (Vector2.Distance(transform.position, target.position) < attackRange)
        {
            speed = 0;
            Attack(attackTime);
        }
        else
        {
            speed = setSpeed;
        }
    }

    public void Shoot(float interval)
    {
        if (timer <= interval)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 3;
            target.GetComponent<BirdIDamageable>().GetDamage(weapon.GetDamage());
            weapon.Shoot(bulletPrefab, this, "BirdPlayer");
            Debug.Log($"Shooter enemy damage ; {weapon.GetDamage()}");
        }
                
    }

    public void SetMachineGunEnemy(float attackRange, float attackTime)
    {
        this.attackRange = attackRange;
        this.attackTime = attackTime;
    }
}
