using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponCollision : MonoBehaviour
{
    public enum WeaponType { Melee, Bullet}
    public WeaponType weaponType;
    public int bulletDestrlayerNumber;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.layer == bulletDestrlayerNumber)
        {
            Destroy(gameObject);
        }


        Guard guard = collision.GetComponent<Guard>();
        if (guard != null)
        {
            guard.Die();
            if (weaponType == WeaponType.Bullet)
            {
                Destroy(gameObject);
            }

        }
    }

}
