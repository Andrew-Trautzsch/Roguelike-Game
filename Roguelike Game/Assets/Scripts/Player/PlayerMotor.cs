using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private PlayerStats playerStats;
    private PlayerLook playerLook;
    private Vector3 playerVelocity;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    private bool isGrounded;
    private bool crouching = false;
    private float crouchTimer = 1;
    private bool lerpCrouch = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerStats = GetComponent<PlayerStats>();
        playerLook = GetComponent<PlayerLook>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float tmp = crouchTimer / 1;
            tmp *= tmp;
            if (crouching)
                controller.height = Mathf.Lerp(controller.height, 1, tmp);
            else
                controller.height = Mathf.Lerp(controller.height, 2, tmp);
            if (tmp > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0;
            }
        }
    }

    // recieve the inputs for InputManager and apply them to the character controller
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * playerStats.playerSpeed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        controller.Move(playerVelocity * Time.deltaTime);
        // Debug.Log(playerVelocity.y);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }

    public void Shoot()
    {
        // Ensure the gun barrel and player stats are properly assigned
        if (playerLook.gunBarrel == null || playerStats == null)
        {
            Debug.LogError("Gun barrel or PlayerStats not set!");
            return;
        }

        // Spawn the bullet at the gun barrel position
        GameObject bullet = Instantiate(Resources.Load("Prefabs/PlayerBullet") as GameObject, playerLook.gunBarrel.position, Quaternion.identity);

        // Ensure the bullet has a Rigidbody and apply velocity
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            Vector3 shootDirection = playerLook.playerCamera.transform.forward;
            bulletRigidbody.velocity = shootDirection.normalized * 40f; // Adjust bullet speed
        }

        // Set the damage on the bullet
        PlayerBullet playerBullet = bullet.GetComponent<PlayerBullet>();
        if (playerBullet != null)
        {
            playerBullet.Initialize(playerStats.playerDamage); // Pass the player's damage to the bullet
        }
    }

}
