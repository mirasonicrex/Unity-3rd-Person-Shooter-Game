using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag != "Player")
            return;
        Pickup(collider.transform);
    }

    public virtual void OnPickup(Transform item)
    {

    }


    void Pickup(Transform item)
    {
        OnPickup(item);
    }
    

}
