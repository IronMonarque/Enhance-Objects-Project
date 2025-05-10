using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BirdShooter : BirdEnemy
{
    [SerializeField] private float attackRange;
    [SerializeField] private float attackTime = 1;
    [SerializeField] private float weaponDamage = 2;
    [SerializeField] private float bulletSpeed = 10;
    [SerializeField] private BulletSeed bulletPrefab;

    BirdEnemy birdEnemy;
    private Animator shooterAnimator;

    private float timer = 0;

    private float setSpeed = 2;

    protected override void Start()
    {
        base.Start();
        health = new HealthSeed(1, 0, 1);
        setSpeed = speed;

        shooterAnimator = GetComponent<Animator>();
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

            // Trigger shooting when within range
            Shoot(bulletPrefab, this, "BirdPlayer", attackTime);
        }
        else
        {
            speed = setSpeed;
        }

        
    }

    private void Shoot(BulletSeed bullet, Playable_Object shooter, string targetTag,float interval)
    {
        if (timer <= interval)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            target.GetComponent<BirdIDamageable>().GetDamage(weapon.GetDamage());
            BulletSeed tempBullet = GameObject.Instantiate(bullet, shooter.transform.position, shooter.transform.rotation);
            tempBullet.SetBullet(weaponDamage, targetTag, bulletSpeed);

            weapon.Shoot(bulletPrefab, this, "BirdPlayer");
            Debug.Log($"Shooter enemy damage ; {weapon.GetDamage()}");
        }                
    }
    
    public void SetShooterEnemy(float attackRange, float attackTime)
    {
        this.attackRange = attackRange;
        this.attackTime = attackTime;
    }
}
