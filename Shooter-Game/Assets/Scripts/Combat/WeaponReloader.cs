using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReloader : MonoBehaviour
{
    [SerializeField] int maxAmmo;
    [SerializeField] float reloadTime;
    [SerializeField] int clipSize;
    [SerializeField] Container inventory;
    [SerializeField] EWeaponType weaponType;

    public int shotsFiredInClip;
    bool isReloading;
    System.Guid containerItemId;

    public event System.Action OnAmmoChanged; //picking up ammo

    public int RoundsRemainingInClip
    {
        get
        {
            return clipSize - shotsFiredInClip; 
        }
    }

    public int RoundsRemainingInInventory
    {
        get
        {
            return inventory.GetAmountRemaining(containerItemId);
        }
    }

    public bool IsReloading
    {
        get
        {
            return isReloading;
        }
    }
    private void Awake()
    {
        inventory.OnContainerReady += () => //calls event from container to add gun to inventory 
        {
            containerItemId = inventory.Add(weaponType.ToString(), maxAmmo); //equal to guid
        };
    }

    public void Reload()
    {
        if (isReloading)
            return;
        isReloading = true;
       //if we are reloading and still have bullets in the clip it will reload from only the amount missing 
        print("is reloading");
        GameManager.Instance.Timer.Add(() => {
            ExecuteReload(inventory.TakeFromContainer(containerItemId, clipSize - RoundsRemainingInClip));
        },reloadTime);

        
    }

    private void ExecuteReload(int amount)
    {

        print("reload executed");
        isReloading = false;
        shotsFiredInClip -= amount;
        HandleOnAmmoChanged(); 
    }

    public void TakenFromClip(int amount) //takes the amount of ammo we are adding to the shots fired
    {
        shotsFiredInClip += amount;
        HandleOnAmmoChanged();
    }

    public void HandleOnAmmoChanged()
    {
        if (OnAmmoChanged != null)
            OnAmmoChanged();
    }
}
