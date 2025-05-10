using UnityEngine;

public class BulletSeed : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;

    private string targetTag;

    public void SetBullet(float damage, string targetTag, float speed = 10)
    {
        this.damage = damage;
        this.speed = speed;
        this.targetTag = targetTag;
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void Damage(BirdIDamageable damageable)
    {
        if (damageable != null) 
        {
            damageable.GetDamage(damage);
            Debug.Log("Damaged something!");

            //Use our singleton from Game Manager to access the score manager
            //BirdGameManager.GetInstance().scoreManager.IncrementScore();
            Destroy(gameObject);
        }        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);

        //Check for the target
        if (!other.gameObject.CompareTag(targetTag))
            return;

        BirdIDamageable damageable = other.GetComponent<BirdIDamageable>();
        Damage(damageable);
    }
}
