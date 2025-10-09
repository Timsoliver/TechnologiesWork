using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.VFX;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public Camera fpsCam;

    private PlayerControls controls;
    private InputAction shootAction;
    
    public ParticleSystem muzzleFlash;
    public GameObject hitEffectPrefab;

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
        muzzleFlash.Play();
        
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            SpawnHitEffect(hit);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
        
    }

    private void SpawnHitEffect(RaycastHit hit)
    {
        if (hitEffectPrefab != null)
        {
            VisualEffect vfx = Instantiate(hitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal)).GetComponent<VisualEffect>();
            vfx.Play();
        }
    }
}
