using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Variables")]
    public float walkSpeed = 6f;
    public float runSpeed = 12f;

    private Vector3 moveDirection = Vector3.zero;
    private float gravity = 10f;
    private CharacterController characterController;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        //use isRunningBoolean to see if the speed should use runspeed or walkspeed. And if the boolean canMove is false then it wont move at all
        float curSpeedX = (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical");
        float curSpeedY = (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal");
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        //On Air (add gravity to manage how fast the player will fall down if it falls down, just in case it starts flying?xd)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        //Player movement line
        characterController.Move(moveDirection * Time.deltaTime);
    }
}