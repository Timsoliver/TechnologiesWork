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
    
    private InputAction shootAction;

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
