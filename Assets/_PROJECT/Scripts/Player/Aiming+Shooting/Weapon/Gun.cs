using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public Camera fpsCam;
    
    public ParticleSystem muzzleFlash;
    public GameObject hitEffectPrefab;

    private GameObject hitEffectInstance;
    private ParticleSystem hitEffectPS;
    
    private InputAction shootAction;

    void Start()
    {
        if (hitEffectPrefab != null)
        {
            hitEffectInstance = Instantiate(hitEffectPrefab);
            hitEffectInstance.SetActive(false);
            hitEffectPS = hitEffectInstance.GetComponent<ParticleSystem>();
        }
    }
    void OnEnable()
    {
        var controls = InputManager.Instance.Controls;
        shootAction = controls.GroundMovement.Shoot;
        shootAction.performed += ctx => Shoot();
    }

    void OnDisable()
    {
        if(shootAction != null) 
            shootAction.performed -= ctx => Shoot();
    }

    void Shoot()
    {
        if (muzzleFlash != null)
         muzzleFlash.Play();
        
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out RaycastHit hit, range))
        {
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
            hitEffectInstance.transform.position = hit.point;
            hitEffectInstance.transform.rotation = Quaternion.LookRotation(hit.normal);
            hitEffectInstance.SetActive(false);
            hitEffectInstance.SetActive(true);
            
            if (hitEffectPS != null)
                hitEffectPS.Play();
        }
    }
}
