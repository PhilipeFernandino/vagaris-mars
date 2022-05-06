using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {

    public Rigidbody rb;
    public Transform groundCheck;
    public LayerMask groundMask;
    public Transform resetPosition;
    public Camera playerCamera;

    public float cameraFOV = 85;
    public float groundDistance;
    public float jumpStrength;

    public float jetpackAcceleration;
    public float jumpTime;
    public float jetpackGas;
    public float gravityScale;
    public float verticalMaxSpeed;

    float x, z;
    Vector3 velocity;
    bool isGrounded;

    float defaultJetpackGas;
    float timeSinceJump = 0f;
    float checkpointPriority = 0;

    bool jumpInput;
    bool jumpInputReleased;
    bool runInput = false;
    bool isJumping = false;

    Vector2 axisInput;
    
    public Vector2 speed;
    public float sprintSpeedModifier;

    #region InputFunctions
    public void JumpInput(InputAction.CallbackContext context) {
        if (context.started) {
            jumpInput = true;
        }
        else if (context.canceled) {
            jumpInputReleased = true;
            jumpInput = false;
        }
    }

    public void RunInput(InputAction.CallbackContext context) {
        runInput = (context.performed || context.started) ? true : false;
    }

    public void MovementInput(InputAction.CallbackContext context) {
        axisInput = context.ReadValue<Vector2>();
    }

    public void ResetPosition(InputAction.CallbackContext context) {
        if (context.started) transform.position = resetPosition.position;
    }

    public void Checkpoint(Transform checkpoint, int priority) {
        if (checkpointPriority < priority) {
            resetPosition = checkpoint;
        }
    }

    #endregion

    void Awake() {
        playerCamera.fieldOfView = cameraFOV;
        defaultJetpackGas = jetpackGas;
    }

    void Update() {
        if (isJumping) timeSinceJump += Time.deltaTime;
    }

    void FixedUpdate() {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        //Movimenta o jogador
        axisInput = axisInput.normalized;
        Vector3 move = new Vector3(0f, 0f, 0f); 
        if (runInput)
            move = transform.forward * axisInput.y * speed.x * sprintSpeedModifier + transform.right * axisInput.x * speed.y;
        else 
            move = transform.forward * axisInput.y * speed.x + transform.right * axisInput.x * speed.y;

        rb.AddForce(move * Time.deltaTime, ForceMode.VelocityChange);
        //Cuidando do pulo, pulo forte e jetpack
        #region Vertical Movement
        if (isGrounded) {
            isJumping = false;
            jumpInputReleased = false;
            timeSinceJump = 0f;
            jetpackGas = defaultJetpackGas; 

            if (jumpInput) {
                rb.AddForce(transform.up * jumpStrength * Time.deltaTime, ForceMode.VelocityChange); 
                isJumping = true;
            }
  
        } else if (jumpInput && timeSinceJump <= jumpTime && isJumping) {
            rb.AddForce(transform.up * jumpStrength * Time.deltaTime, ForceMode.VelocityChange);

        } else if (((jumpInput && jumpInputReleased) || (jumpInput && !isJumping)) && jetpackGas > 0) {
            if (rb.velocity.y < verticalMaxSpeed) rb.AddForce(transform.up * jetpackAcceleration * Time.deltaTime, ForceMode.Acceleration);
            jetpackGas -= 1; 
            
        } else if (rb.velocity.y > -verticalMaxSpeed) rb.AddForce(Physics.gravity * gravityScale * Time.deltaTime, ForceMode.Acceleration);
        #endregion

    }
}
