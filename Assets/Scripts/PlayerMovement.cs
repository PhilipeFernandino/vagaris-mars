using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {

    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;
    public Transform resetPosition;

    public float walkSpeed = 12f;
    public float runSpeed = 20f;
    public float maxWalkSpeed = 14f;
    public float maxRunSpeed = 20f;
    
    public float groundDistance = 0.4f;
    public float jumpHeight = 20f;
    public float gravityScale = 2f;

    public float walkingNoise = 10f;

    public float jumpTime = 1f;

    public NoiseSimulator noiseSimulator;

    float gravity = 9.81f;
    float x, z;
    Vector3 velocity;
    bool isGrounded;

    float timeSinceJump = 0f;
    bool isJumping = false;
    
    bool jumpInput = false; 
    bool runInput = false;

    Vector2 axisInput;

    void Start() {
        gravity *= gravityScale;
    }

    
    public void JumpInput(InputAction.CallbackContext context) {
        if (context.performed) jumpInput = true;
        if (context.canceled) jumpInput = false;
    }

    public void RunInput(InputAction.CallbackContext context) {
        if (context.performed) runInput = true;
    }

    public void MovementInput(InputAction.CallbackContext context) {
        axisInput = context.ReadValue<Vector2>();
    }

    void Update() {

        gravity = 9.81f * gravityScale;
        timeSinceJump += Time.deltaTime;

        #region Teleport
        if (transform.position.y < -50) {
            velocity.y = -2f;
            controller.Move(Vector3.zero);
            transform.position = resetPosition.position;
            return;
        }
        #endregion

        #region Vertical Movement
        //Checa se o player está no chão
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        //Impede o player de atravessar o teto enquanto pula
        if ((controller.collisionFlags & CollisionFlags.Above) != 0) velocity.y = -2f;

        //Se o player tá no meio do pulo
        if (isJumping && timeSinceJump < jumpTime && jumpInput) velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);

        //Manipula a velocidade vertical
        if (isGrounded) {
        
            isJumping = false;
            timeSinceJump = 0f;

            if (jumpInput) {
                isJumping = true;
                velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity); 
            }
            else if (velocity.y < 0) velocity.y = -2f;
        } else velocity.y -= gravity * Time.deltaTime;
        #endregion

        #region Horizontal Movement

        Vector3 move = Vector3.Normalize(transform.right * axisInput.x + transform.forward * axisInput.y);
        float speed = runInput ? runSpeed : walkSpeed;
        if (!runInput) {
            
        }
        #endregion
        move.x *= speed;
        controller.Move((move + velocity) * Time.deltaTime);
        if (move != Vector3.zero) noiseSimulator.MakeNoise(gameObject, walkingNoise);
    }
}
