using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BirdLevelLoader : MonoBehaviour
{
    public static int Score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Create a player object. This is creating a copy from the Player class script
        BirdPlayer player = new BirdPlayer();

        //Create two enemies
        BirdEnemy enemy1 = new BirdEnemy();
        BirdEnemy enemy2 = new BirdEnemy();

        //Create 3 weapons
        BirdWeapon gun = new BirdWeapon();
        //Weapon rifle = new Weapon("Rifle", 2.0f);
        BirdWeapon machineGun = new BirdWeapon();

        //Enums
        BirdEnemyType enemyType1 = new BirdEnemyType();
        enemyType1 = BirdEnemyType.BirdMelee;

        //Enemy.TestPublicEnum testEnum = Enemy.TestPublicEnum.option2;

        //Aggregation relationship
        player.weapon = gun;
        //enemy1.weapon = rifle;
        enemy2.weapon = machineGun;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
