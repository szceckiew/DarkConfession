using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public GameObject itemPickup;
    public float pickupValue;
    public float pickupRange = 0.7f;

    public LayerMask pickUpsLayer;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pickupValue = Input.GetAxisRaw("Pickup");
    }

    void FixedUpdate()
    {
        if (pickupValue > 0)
        {
            Pickup();
        }
    }

    void Pickup()
    {
        Collider2D[] pickUps = Physics2D.OverlapCircleAll(transform.position, pickupRange, pickUpsLayer);

        foreach (Collider2D pickUp in pickUps)
        {
            // pickUp logic
            pickUp.GetComponent<Renderer>().enabled = false;
            pickUp.GetComponent<Collider2D>().enabled = false;

            PickupSuccessful(pickUp.GetComponent<Renderer>().name);

        }
    }

    public enum PickUpsEnum
    {
        greenDot_0,

    }

    // different pickup items
    void PickupSuccessful(string name)
    {
        switch (name)
        {
            case string greenDot_0:
                Debug.Log("You picked up a green Dot");
                break;
            default:
                Debug.Log("ERROR: Picked something unknown");
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }

}
