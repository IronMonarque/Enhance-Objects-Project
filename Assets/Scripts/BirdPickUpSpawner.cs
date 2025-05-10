using System.Collections.Generic;
using UnityEngine;

public class BirdPickUpSpawner : MonoBehaviour
{
    [SerializeField] private BirdPickUpSpawn[] pickups;

    [Range(0,1)]
    [SerializeField] private float pickupProbability;

    private List<BirdPickup> pickupPool = new List<BirdPickup> ();
    BirdPickup chosenPickup;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (BirdPickUpSpawn spawn in pickups)
        {
            for (int i = 0; i < spawn.spawnAmount; i++)
            {
                pickupPool.Add(spawn.pickup);
            }
        }
    }

    public void SpawnPickup(Vector2 spawnPos)
    {
        if (pickupPool.Count == 0) { return; }

        if (Random.Range(0.0f, 1.0f) < pickupProbability)
        {
            chosenPickup = pickupPool[Random.Range(0, pickupPool.Count)];
            Instantiate(chosenPickup, spawnPos, Quaternion.identity);
        }
            
    }
}

[System.Serializable]
public struct BirdPickUpSpawn
{
    public BirdPickup pickup;
    public int spawnAmount;
}
