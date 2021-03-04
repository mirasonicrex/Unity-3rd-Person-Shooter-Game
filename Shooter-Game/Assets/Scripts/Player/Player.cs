using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(PlayerState))]
public class Player : MonoBehaviour
{
    [System.Serializable]
    public class MouseInput
    {
        public Vector2 Damping;
        public Vector2 Sensitivity;
        public bool LockMouse;
    }

    [SerializeField] float runSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float crouchSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] MouseInput MouseControl;
    [SerializeField] AudioController footSteps;
    [SerializeField] float minimumMoveThreshold;
    [SerializeField] float jumpHeight;

    public PlayerAim playerAim;

    Vector3 previousPosition; 

    private MovementController m_MovementController;
    public MovementController MovementController {
        get {
            if(m_MovementController == null)
                m_MovementController = GetComponent<MovementController>();
            return m_MovementController; 
        }
    }

    private PlayerShoot m_PlayerShoot;
    public PlayerShoot PlayerShoot
    {
        get
        {
            if (m_PlayerShoot == null)
            {
                m_PlayerShoot = GetComponent<PlayerShoot>();
            }
            return m_PlayerShoot;
        }
    }

 

    public PlayerState m_PlayerState;
    public PlayerState PlayerState
    {
        get
        {
            if (m_PlayerState == null)
                m_PlayerState = GetComponent<PlayerState>();
            return m_PlayerState;
        }
    }


    InputController playerInput;
    Vector2 mouseInput;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GameManager.Instance.InputController;
        GameManager.Instance.LocalPlayer = this; //local player is created by gamemanager in event

        if (MouseControl.LockMouse)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        LookAround();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeight);
        }
    }

    private void Move()
    {
        float moveSpeed = runSpeed;

        if (playerInput.IsWalking)
            moveSpeed = walkSpeed;

        if (playerInput.IsSprinting)
            moveSpeed = sprintSpeed;

        if (playerInput.IsCrouched)
            moveSpeed = crouchSpeed; 
        
   
        Vector2 direction = new Vector2(playerInput.Vertical * moveSpeed, playerInput.Horizontal * moveSpeed);

        MovementController.Move(direction);

        if (Vector3.Distance(transform.position, previousPosition) > minimumMoveThreshold)
            footSteps.Play(); //player has to move a certain distance before sound plays

        previousPosition = transform.position;


    }

    private void LookAround()
    {
        mouseInput.x = Mathf.Lerp(mouseInput.x, playerInput.MouseInput.x, 1f / MouseControl.Damping.x);
        mouseInput.y = Mathf.Lerp(mouseInput.y, playerInput.MouseInput.y, 1f / MouseControl.Damping.y);
        transform.Rotate(Vector3.up * mouseInput.x * MouseControl.Sensitivity.x);
        
        playerAim.SetRotation(mouseInput.y * MouseControl.Sensitivity.y);
    }


}
