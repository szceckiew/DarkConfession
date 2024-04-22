using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("weaponCollision");
        Guard guard = collision.GetComponent<Guard>();
        if (guard != null)
        {
            guard.Die();
        }
    }

}
