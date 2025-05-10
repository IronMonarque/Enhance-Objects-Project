using UnityEngine;

public class HealthSeedPickup : BirdPickup, BirdIDamageable
{
    [SerializeField] private float healthMin, healthMax;

    public override void onPicked()
    {
        base.onPicked();

        //Generate a random health value and add it to the player
        float health = Random.Range(healthMin, healthMax);

        var player = BirdGameManager.GetInstance().GetPlayer();
        player.health.AddHealth(health);

        Debug.Log($"Added {health} to {player.name}");
    }
   public void GetDamage(float damage)
    {
        onPicked();
    }
}
