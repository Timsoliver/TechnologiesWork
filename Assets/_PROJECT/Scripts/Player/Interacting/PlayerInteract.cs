using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public Camera mainCam;
    public float interactDistance = 2f;
    
    public GameObject interactionUI;
    public TextMeshProUGUI interactionText;
    
    private InputAction interactAction;
    private IInteractable currentInteractable;
    
    private void OnEnable()
    {
        var controls = InputManager.Instance.Controls;
        interactAction = controls.GroundMovement.Interact;
        interactAction.performed += OnInteractPerformed;
    }

    private void OnDisable()
    {
        if (interactAction != null)
         interactAction.performed -= OnInteractPerformed;
    }

    private void Update()
    {
        InteractionRay();
    }
    

    void InteractionRay()
    {
        Ray ray = mainCam.ViewportPointToRay(Vector3.one/2f);
        RaycastHit hit;
        bool hitSomething = false;

        if (Physics.Raycast(ray, out hit, interactDistance)) 
        {
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
        else
        {
            Debug.Log("No interaction found");
        }
    }
    
    private void OnDrawGizmos()
    {
        if (mainCam == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(mainCam.transform.position, mainCam.transform.forward * interactDistance);
    }

}
