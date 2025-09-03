using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.HighDefinition;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public Camera fpsCam;

    private PlayerControls controls;
    private InputAction shootAction;

    void Awake()
    {
        controls = new PlayerControls();
    }

    void OnEnable()
    {
        shootAction = controls.GroundMovement.Shoot;
        shootAction.Enable();

        shootAction.performed += ctx => Shoot();
    }

    void OnDisable()
    {
        shootAction.Disable();
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
        
    }
}
