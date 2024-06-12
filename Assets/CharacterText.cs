using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterText : MonoBehaviour
{
    public Sprite message;
    public Sprite nomessage;
    public Sprite bulletsText;
    public SpriteRenderer sr;

    public float waitTime = 3f;

    public bool winnable = false;
    public bool said = false;


    IEnumerator Message()
    {
        sr.sprite = message;
        yield return new WaitForSeconds(waitTime);
        sr.sprite = nomessage;
    }


    IEnumerator BulletsText()
    {
        sr.sprite = bulletsText;
        yield return new WaitForSeconds(waitTime);
        sr.sprite = nomessage;
    }


    private void Start()
    {
        StartCoroutine(BulletsText());
    }


    void Update()
    {
        if (winnable)
        {
            if (!said)
            {
                said = true;
                StartCoroutine(Message());
            }
        }
    }
}
