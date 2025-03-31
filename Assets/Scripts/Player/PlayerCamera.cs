using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    
    [Header("Camera")]
    public float lookSpeed = 180f;
    public float lookDOWNLimit = 45f;
    public float lookUPLimit = -14f;
    private float rotationX = 0;
    public Camera playerCamera;

    void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //Camera Movement
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed * Time.deltaTime;
        rotationX = Mathf.Clamp(rotationX, lookUPLimit, lookDOWNLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed * Time.deltaTime, 0);
    }
}
