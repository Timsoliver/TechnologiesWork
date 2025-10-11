using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public Camera mainCam;
    public float interactDistance = 2f;
    
    public GameObject interactionUI;
    public TextMeshProUGUI interactionText;
    
    private PlayerControls controls;
    private InputAction interactAction;

    private void OnEnable()
    {
        interactAction = controls.GroundMovement.Interact;
        interactAction.Enable();

        interactAction.performed += OnInteractPerformed;
    }

    private void OnDisable()
    {
        interactAction.Disable();
    }

    private void Update()
    {
        InteractionRay();
    }
    
    private IInteractable currentInteractable; 

    void InteractionRay()
    {
        Ray ray = mainCam.ViewportPointToRay(Vector3.one/2f);
        RaycastHit hit;
        bool hitSomething = false;

        if (Physics.Raycast(ray, out hit, interactDistance)) 
        {
            //Debug.Log("Hit object: " + hit.collider.name);
            
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                hitSomething = true;
                currentInteractable = interactable;
                interactionText.text = interactable.GetDescription();
            }
            else
            {
                currentInteractable = null;
            }
        }
        else
        {
            currentInteractable = null;
        }
        interactionUI.SetActive(hitSomething);
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }
    
    private void OnDrawGizmos()
    {
        if (mainCam == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(mainCam.transform.position, mainCam.transform.forward * interactDistance);
    }

}
