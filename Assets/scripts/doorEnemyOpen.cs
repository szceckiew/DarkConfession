using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class doorEnemyOpen : MonoBehaviour
{

    public LayerMask autoDoorOpenMask;
    public Sprite[] doorSprites;


    private void Start()
    {

    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        //bit operation to check if the colliding object is int the mask autoDoorOpenMask
        if ((autoDoorOpenMask.value & (int)Math.Pow(2, collision.gameObject.layer)) > 0)
        {
            GetComponentInParent<SpriteRenderer>().sprite = doorSprites[1];
            GetComponentInParent<BoxCollider2D>().isTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((autoDoorOpenMask.value & (int)Math.Pow(2, collision.gameObject.layer)) > 0)
        {
            GetComponentInParent<SpriteRenderer>().sprite = doorSprites[0];
            GetComponentInParent<BoxCollider2D>().isTrigger = false;

        }
    }

}
