using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Shooter //calling from shooter script
{
    public override void Fire()
    {
        base.Fire(); //will have access to properties of shooter
        if (canFire)
        {
            //we fire gun
        }

    }

    public void Update()
    {
        if (GameManager.Instance.InputController.Reload)
        {
            Reload();
        }
    }
}
