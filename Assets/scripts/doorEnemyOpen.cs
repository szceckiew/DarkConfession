using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class doorEnemyOpen : MonoBehaviour
{

    public LayerMask autoDoorOpenMask;
    public Sprite[] doorSprites;
    public GameObject layerChanging;


    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //bit operation to check if the colliding object is int the mask autoDoorOpenMask
        if ((autoDoorOpenMask.value & (int)Math.Pow(2, collision.gameObject.layer)) > 0)
        {
            GetComponentInParent<SpriteRenderer>().sprite = doorSprites[1];
            
            GetComponentInParent<ShadowCaster2D>().enabled = false;

            GetComponentInParent<BoxCollider2D>().isTrigger = true;
            GetComponentInChildren<BoxCollider2D>().isTrigger = true;
            layerChanging.layer = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((autoDoorOpenMask.value & (int)Math.Pow(2, collision.gameObject.layer)) > 0)
        {
            GetComponentInParent<SpriteRenderer>().sprite = doorSprites[0];

            GetComponentInParent<ShadowCaster2D>().enabled = true;

            GetComponentInParent<BoxCollider2D>().isTrigger = false;
            GetComponentInChildren<BoxCollider2D>().isTrigger = true;
            layerChanging.layer = 12;

        }
    }

}
