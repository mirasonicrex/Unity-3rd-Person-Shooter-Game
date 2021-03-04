using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [System.Serializable]
    public class CameraRig
    {
        public Vector3 CameraOffset;
        public float crouchHeight; //height of camera when crouching
        public float Damping;
    }

    //0.5, 0.05, -2.25 damping 10
    [SerializeField]
    CameraRig defaultCamera;
    [SerializeField]
    CameraRig aimCamera;
 
    Transform cameraLookTarget;
    private Player localPlayer;
   


    void Awake()
    {
        GameManager.Instance.OnLocalPlayerJoined += HandleOnLocalPlayerJoined; ;
    }

    void HandleOnLocalPlayerJoined (Player player)
    {
        localPlayer = player;
        cameraLookTarget = localPlayer.transform.Find("AimAngle");
        if (cameraLookTarget == null)
            cameraLookTarget = localPlayer.transform; //wont have an empty camera
    }


    // Update is called once per frame
    void Update()
    {
        if (localPlayer == null)
            return;

        CameraRig cameraRig = defaultCamera;
        if (localPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMING || localPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AMINGFIRING)
            cameraRig = aimCamera;

        float targetHeight = cameraRig.CameraOffset.y + (localPlayer.PlayerState.MoveState == PlayerState.EMoveState.CROUCHING ? cameraRig.crouchHeight: 0); //? means a true and false after if its true the cmaerarig.croughheight otherwise 0 

        Vector3 targetPosition = cameraLookTarget.position + localPlayer.transform.forward * cameraRig.CameraOffset.z +
            localPlayer.transform.up * targetHeight +
            localPlayer.transform.right * cameraRig.CameraOffset.x; //can adjust camera to right or left of players shoulder

        Quaternion targetRotation = cameraLookTarget.rotation;
            //creates lag in rotation 

        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraRig.Damping * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, cameraRig.Damping * Time.deltaTime);
    }
}
