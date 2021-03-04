using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] float rateOfFire;
    [SerializeField] Projectile projectile; //calling class projectile
    [SerializeField] Transform hand; //need model to be correct for this to work
    [SerializeField] Transform aimTarget; 

    public WeaponReloader reloader; //accesses weapon reloader
    private ParticleSystem muzzleFireParticleSystem;


    float nextFireAllowed;
    public bool canFire;

    [HideInInspector]
    public Transform muzzle;

    public void Equip() //gameobject gets activated
    {
        transform.SetParent(hand); //may have to change using this depending on model
        //transform.localPosition = Vector3.zero;
        //transform.localRotation = Quaternion.identity; 
    }



    private void Awake()
    {
        muzzle = transform.Find("Muzzle");
        reloader = GetComponent<WeaponReloader>();
        muzzleFireParticleSystem = muzzle.GetComponent<ParticleSystem>();
          }

    public void Reload()
    {
        if (reloader == null)
            return;
        reloader.Reload();
    }

    void FireEffect()
    {
        if (muzzleFireParticleSystem == null)
            return;
        muzzleFireParticleSystem.Play();
    }

    public virtual void Fire() //virtual allows us to override it from a different class
    {
        //print("Fire1");
        canFire = false; //resets after fire
        if (Time.time < nextFireAllowed) 
            return;

        if (reloader != null)
        {
            if (reloader.IsReloading)
                return;
            if (reloader.RoundsRemainingInClip == 0)
                return;
            reloader.TakenFromClip(1);
            
        }//checks if there is still ammo 

        nextFireAllowed = Time.time + rateOfFire;
        muzzle.LookAt(aimTarget);
        FireEffect();

        //instantiate the projectile
        Instantiate(projectile, muzzle.position, muzzle.rotation);

        canFire = true;
    }
}
