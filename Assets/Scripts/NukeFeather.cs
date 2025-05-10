using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Required for UI updates

public class NukeFeather : BirdPickup
{
    private Bird_UI_Manager uiManager;

    private bool canActivateNuke = true;
    private float nukeCooldown = 5.0f;

    //public Text nukeCounterUI; // UI element to display nuke count    

    private void Start()
    {
        uiManager = FindObjectOfType<Bird_UI_Manager>();
    }
    

    private IEnumerator NukeCooldownRoutine()
    {
        canActivateNuke = false;
        yield return new WaitForSeconds(nukeCooldown);
        canActivateNuke = true;
    }


    

    public void CollectNuke()
    {
        BirdGameManager.GetInstance().nukeCount++;
        uiManager.UpdateNukeCountUI();
        Debug.Log("Nuke Count after collection: " + uiManager.nukeCount);
        //UpdateNukeUI();
    }  

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object colliding with the power-up is the player
        if (other.CompareTag("BirdPlayer"))
        {
            CollectNuke();
            Destroy(gameObject); // Destroy the power-up object itself
        }
    }
}