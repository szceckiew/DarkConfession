using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public GameObject interactibleObject;
    public float interactValue;
    public float interactRange = 0.7f;
    public bool canInteract = true;
    public float interactCooldown = 0.5f;
    public float interactTimer;

    public LayerMask InteractableLayer;

    public Sprite[] doorSprites;


    void Start()
    {
        interactTimer = interactCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        interactValue = Input.GetAxisRaw("Interact");
        if (!canInteract)
        {
            interactTimer -= Time.deltaTime;
            if (interactTimer < 0)
            {
                canInteract = true;
                interactTimer = interactCooldown;
            }
        }

    }

    void FixedUpdate()
    {
        if (interactValue > 0 && canInteract)
        {
            canInteract = false;
            StartCoroutine(Interact());

        }
    }

    IEnumerator Interact()
    {
        Collider2D[] interactables = Physics2D.OverlapCircleAll(transform.position, interactRange, InteractableLayer);

        foreach (Collider2D interactable in interactables)
        {
            // pickUp logic
            

            InteractSuccessful(interactable);

        }
        yield return null;
    }

    // does nothing
    public enum InteractablesEnum
    {
        greenDot_0,

    }

    // different interactables items
    void InteractSuccessful(Collider2D interactable)
    {
        switch (interactable.GetComponent<SpriteRenderer>().sprite.name)
        {
            case "greenDot_0":
                interactable.GetComponent<Renderer>().enabled = false;
                interactable.GetComponent<Collider2D>().enabled = false;
                Debug.Log("You interacted with a green Dot: It disappeared");
                break;

            case "simpleDoorSheet_0":
                interactable.GetComponent<SpriteRenderer>().sprite = doorSprites[1];
                interactable.GetComponent<Collider2D>().isTrigger = true;

                Debug.Log("You opened a door");

                break;
            case "simpleDoorSheet_1":
                interactable.GetComponent<SpriteRenderer>().sprite = doorSprites[0];
                interactable.GetComponent<Collider2D>().isTrigger = false;

                Debug.Log("You closed a door");

                break;


            default:
                Debug.Log("ERROR: Interacted with something unknown");
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }

}
