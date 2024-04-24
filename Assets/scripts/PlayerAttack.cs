using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public GameObject Circle;

    //melee attack
    [SerializeField] float swing;
    float atkDuration = 0.3f;
    float atkTimer = 0f;
    bool isAttacking = false;

    //ranged attack
    [SerializeField] float fire;
    public Transform Aim;
    public GameObject bullet;
    public float fireForce = 10f;
    float shootCooldown = 0.25f;
    float shootTimer = 0.5f;

    public void Attack()
    {
        if (!isAttacking)
        {
            Circle.SetActive(true);
            isAttacking = true;
            //animation here
        }
    }

    public void Fire()
    {
        if (shootTimer > shootCooldown)
        {
            shootTimer = 0f;
            GameObject intBullet = Instantiate(bullet, Aim.position, Aim.rotation);
            intBullet.GetComponent<Rigidbody2D>().AddForce(-Aim.up * fireForce, ForceMode2D.Impulse);
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
        checkAtkTimer();
        shootTimer += Time.deltaTime;

        swing = Input.GetAxisRaw("Fire1");
        fire = Input.GetAxisRaw("Fire2");

    }

    void FixedUpdate()
    {
        if (swing > 0)
        {
            Attack();
        }

        if (fire > 0)
        {
            Fire();
        }
    }



}
