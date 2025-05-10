using UnityEngine;

public abstract class BirdPickup : MonoBehaviour
{
   public virtual void onPicked()
    {
        Destroy(gameObject);
    }
}
