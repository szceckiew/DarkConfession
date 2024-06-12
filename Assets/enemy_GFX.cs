using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class enemy_GFX : MonoBehaviour
{
    public AIPath aiPath;
    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        if(aiPath.desiredVelocity.y >= 0.01f || aiPath.desiredVelocity.y <= -0.01f)
        {
            anim.SetFloat("y", aiPath.desiredVelocity.y);
        }
        if (aiPath.desiredVelocity.x >= 0.01f || aiPath.desiredVelocity.x <= -0.01f)
        {
            anim.SetFloat("x", aiPath.desiredVelocity.x);
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
