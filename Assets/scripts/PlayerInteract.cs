using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public GameObject interactibleObject;
    public float interactValue;
    public float interactRange = 0.7f;

    public LayerMask InteractableLayer;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        interactValue = Input.GetAxisRaw("Interact");
    }

    void FixedUpdate()
    {
        if (interactValue > 0)
        {
            Interact();
        }
    }

    void Interact()
    {
        Collider2D[] interactables = Physics2D.OverlapCircleAll(transform.position, interactRange, InteractableLayer);

        foreach (Collider2D interactable in interactables)
        {
            // pickUp logic
            interactable.GetComponent<Renderer>().enabled = false;
            interactable.GetComponent<Collider2D>().enabled = false;

            InteractSuccessful(interactable.GetComponent<Renderer>().name);

        }
    }

    // does nothing
    public enum InteractablesEnum
    {
        greenDot_0,

    }

    // different interactables items
    void InteractSuccessful(string name)
    {
        switch (name)
        {
            case string greenDot_0:
                Debug.Log("You interacted with a green Dot: It disappeared");
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
