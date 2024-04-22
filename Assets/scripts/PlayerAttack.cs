using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public GameObject Circle;
    [SerializeField] float fire;
    float atkDuration = 0.3f;
    float atkTimer = 0f;
    bool isAttacking = false;


    public void Attack()
    {
        if (!isAttacking)
        {
            Circle.SetActive(true);
            isAttacking = true;
            //animation here
        }
    }
    void checkAtkTimer()
    {
        if (isAttacking)
        {
            atkTimer += Time.deltaTime;
            if (atkTimer >= atkDuration)
            {
                atkTimer = 0f;
                isAttacking= false;
                Circle.SetActive(false);
            }
        }
    }


    void Update()
    {
        fire = Input.GetAxisRaw("Fire1");

    }

    void FixedUpdate()
    {
        if (fire > 0)
        {
            Attack();
        }
    }



}
