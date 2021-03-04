using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour

{
    [SerializeField]float minAngle;
    [SerializeField]float maxAngle;
  public void SetRotation(float amount)
    {
        float newAngle = CheckAngle(transform.eulerAngles.x - amount);
        float clampAngle = Mathf.Clamp(newAngle, minAngle, maxAngle);
        transform.eulerAngles = new Vector3(clampAngle, transform.eulerAngles.y, transform.eulerAngles.z);
    }
  public float GetAngle()
    {
        return CheckAngle(transform.eulerAngles.x);
    }

    public float CheckAngle(float value)
    {
        float angle = value - 180; //looking straight is zero 

        if (angle > 0)
            return angle - 180;

        return angle + 180; 
    }

    void Update()
    {
        print(GetAngle());
    }
}
