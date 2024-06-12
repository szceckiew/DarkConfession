using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class enemy_GFX : MonoBehaviour
{
    public AIPath aiPath;
    public Animator anim;
    float x;
    float y;

    // Update is called once per frame
    void Update()
    {
        x = aiPath.desiredVelocity.x;
        y = aiPath.desiredVelocity.y;
        if (y >= 0.01f || y <= -0.01f)
        {
            anim.SetFloat("y", y);
        }
        if (x >= 0.01f || x <= -0.01f)
        {
            anim.SetFloat("x", x);
        }
        
        if (aiPath.velocity.magnitude > 0.01f)
        {
            anim.SetBool("moving", true);
        } else
        {
            anim.SetBool("moving", false);
        }
    }
}
