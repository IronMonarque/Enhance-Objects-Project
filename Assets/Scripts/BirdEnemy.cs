using System;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using static UnityEngine.GraphicsBuffer;

public class BirdEnemy : Playable_Object
{
    //private string name;
    [SerializeField] protected float speed;
    protected Transform target;
    [SerializeField] AudioSource squawk;
    private BirdEnemyType enemyType;

    protected virtual void Start()
    {
        //handling exception
        try
        {
            target = GameObject.FindWithTag("BirdPlayer").transform;
        }
        catch(NullReferenceException ex) 
        {
            Debug.Log("There is no player in the scene. Enemy selfdestruct" + ex);
            Destroy(gameObject);
            BirdGameManager.GetInstance().SetEnemySpawnState(false);
        }  
        
    }

    protected virtual void Update()
    {
        if (target != null)
        {
            Move(target.position);
        }
        else
        {
            Move(speed);
        }
    }

    /// <summary>
    /// A generic move method for direction and target
    /// </summary>
    /// <param name="direction"> vector2 direction</param>
    /// <param name="target"> vector2 target</param>
    public override void Move(Vector2 direction, Vector2 target)
    {
        //Debug.Log("Enemy is moving");
    }

    public override void Move(float speed) //To be used if there is no player in the scene
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    public override void Move(Vector2 direction) //Move the enemy in the direction of the player
    {
        direction.x -= transform.position.x;
        direction.y -= transform.position.y;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    public override void Shoot()
    {
        Debug.Log("Shooting a bullet");
    }

    public override void Attack(float interval)
    {
        Debug.Log($"Attacking at {interval} interval");
    }

    /// <summary>
    /// Enemy die method
    /// </summary>
    public override void Die()
    {
        Debug.Log("Enemy died");
        BirdGameManager.GetInstance().NotifyDeath(this);
        Destroy(gameObject);
        squawk.Play();
    }

    public void SetEnemyType(BirdEnemyType enemyType)
    {
        this.enemyType = enemyType;
    }

    public override void GetDamage(float damage)
    {
        Debug.Log($"Enemy damaged!");
        health.DeductHealth(damage);
        BirdGameManager.GetInstance().scoreManager.IncrementScore();

        if (health.GetHealth() <= 0)
            Die();
    }
}
