using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 1f;
    public Transform playerBody; 
    
    float xRotation = 0f;
    float mouseX, mouseY;
    Vector2 mouseMovement;
    
    void Start () {
        Cursor.lockState = CursorLockMode.Locked; 
    }

    public void LookAround(InputAction.CallbackContext context) {
        mouseMovement = context.ReadValue<Vector2>();
    }

    void Update() {
        mouseMovement *= mouseSensitivity;

        //x
        xRotation -= mouseMovement.y; 
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        //y
        playerBody.Rotate(Vector3.up * mouseMovement.x); 
    }
}
