using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    protected Joystick joystick;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private CharacterController controller;
    protected float playerSpeed = 4;
    protected float gravityValue = -9.81f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        joystick = FindObjectOfType<Joystick>();
    }

    private void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        controller.Move(move * Time.deltaTime * playerSpeed);

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
