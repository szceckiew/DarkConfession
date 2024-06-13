using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public Animator anim;
    public Animator gunAnim;
    public SpriteRenderer gunRenderer;
    [SerializeField] private string gunLayerForground;
    [SerializeField] private string gunLayerBackground;

    [SerializeField] private FieldOfView fieldOfView;
    public float moveSpeed = 2.5f;
    public Rigidbody2D rb2d;
    private Vector2 moveInput;

    public bool isMoving = false;
    public Transform attackPoint;
    public Vector2 lastMoveDirection;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        if (moveInput.x == 0 && moveInput.y == 0)
        {
            isMoving = false;

            Vector3 attackPointVec = Vector3.left * lastMoveDirection.x + Vector3.down * lastMoveDirection.y;
            attackPoint.rotation = Quaternion.LookRotation(Vector3.forward, attackPointVec);

        }
        else if (moveInput.x != 0 ||  moveInput.y != 0)
        {
            isMoving = true;
        }

        // animation
        if (isMoving)
        {
            //character and gun rotation
            anim.SetFloat("x", moveInput.x);
            gunAnim.SetFloat("x", moveInput.x);
            anim.SetFloat("y", moveInput.y);
            gunAnim.SetFloat("y", moveInput.y);

            //gun layer
            if (moveInput.y > 0.01f)
            {
                gunRenderer.sortingLayerName = gunLayerBackground;
            } else
            {
                gunRenderer.sortingLayerName = gunLayerForground;
            }
        }
        anim.SetBool("moving", isMoving);


        fieldOfView.SetOrigin(transform.position);

    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            lastMoveDirection = moveInput;

            Vector3 attackPointVec = Vector3.left * moveInput.x + Vector3.down * moveInput.y;
            attackPoint.rotation = Quaternion.LookRotation(Vector3.forward, attackPointVec);
        }
        rb2d.MovePosition(rb2d.position + moveInput * moveSpeed * Time.fixedDeltaTime);        
    }

  
}
