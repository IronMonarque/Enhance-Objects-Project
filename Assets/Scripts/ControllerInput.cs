using UnityEngine;

public class ControllerInput : MonoBehaviour
{
    private BirdPlayer player;
    private NukeFeather nuke;
    private float horizontal, vertical;
    private Vector2 lookTarget;
    private Bird_UI_Manager uiManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<BirdPlayer>();

        uiManager = FindObjectOfType<Bird_UI_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!BirdGameManager.GetInstance().IsPlaying())
            return;

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        lookTarget = Input.mousePosition;

        if (Input.GetMouseButtonDown(0)) // numbers are related to the mouse buttons
        {
            player.Shoot();
        }

        if (Input.GetMouseButtonDown(1) && BirdGameManager.GetInstance().nukeCount > 0)
        {
            ActivateNuke();
        }
    }

    public void ActivateNuke()
    {
        // Destroy all entities with tags "Enemy" and "Pickup"
        DestroyAllWithTag("BirdEnemy");
        DestroyAllWithTag("Pickup");

        // Reduce nuke count and update UI
        BirdGameManager.GetInstance().nukeCount--;
        uiManager.UpdateNukeUI();

        /*if (!canActivateNuke) return;
        StartCoroutine(NukeCooldownRoutine());
        // Nuke activation logic here...*/
    }

    private void DestroyAllWithTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }
    }

    private void FixedUpdate()
    {
        player.Move(new Vector2(horizontal, vertical), lookTarget);
    }
}
