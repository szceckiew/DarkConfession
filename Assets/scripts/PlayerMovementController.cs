using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float moveSpeed = 4;
    public Rigidbody2D rb2d;
    private Vector2 moveInput;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

    }

    void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + moveInput * moveSpeed * Time.fixedDeltaTime);        
    }
}
