using UnityEngine;

public class BirdWeapon 
{
    private string name;
    private float damage;
    private float bulletSpeed;

    public BirdWeapon(string name, float damage, float bulletSpeed)
    {
        this.name = name;
        this.damage = damage;
        this.bulletSpeed = bulletSpeed;
    }

    public BirdWeapon()
    {

    }
    public void Shoot(BulletSeed bullet, Playable_Object player, string targetTag, float timeToDie = 5)
    {
        
        Debug.Log($"Shooting from weapon");
        BulletSeed tempBullet = GameObject.Instantiate(bullet, player.transform.position, player.transform.rotation);
        tempBullet.SetBullet(damage, targetTag, bulletSpeed);

        GameObject.Destroy(tempBullet.gameObject, timeToDie);
    }

    public float GetDamage()
    {
        return damage;
    }
}
