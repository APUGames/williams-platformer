using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D playerCharacter;
    Animator playerAnimator;
    CapsuleCollider2D playerBodyCollider;
    BoxCollider2D playerFeetCollider;

    [Tooltip("Change this value to change the run speed")]
    [SerializeField] float runSpeed = 5.0f;

    [Tooltip("Change this value to change the jump speed")]
    [SerializeField] float jumpSpeed = 5.0f;

    [Tooltip("Change this value to change the climb speed")]
    [SerializeField] float climbSpeed = 5.0f;

    private float gravityScaleAtStart;

    private bool isAlive = true;

    [SerializeField] Vector2 deathSeq = new Vector2(25f, 25f);

    [SerializeField] AudioClip coinPickSFX;

    // Start is called before the first frame update
    void Start()
    {
        playerCharacter = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerBodyCollider = GetComponent<CapsuleCollider2D>();
        playerFeetCollider = GetComponent<BoxCollider2D>();

        // Storej Gravity Scale when game starts
        gravityScaleAtStart = playerCharacter.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive)
        {
            return;
        }

        Run();
        FlipSprite();
        Jump();
        Climb();
        Die();
    }

   
    private void Run()
    {
        // Get a value between -1 and +1
        float hMovement = Input.GetAxis("Horizontal");
        Vector2 runVelocity = new Vector2(hMovement * runSpeed, playerCharacter.velocity.y);
        playerCharacter.velocity = runVelocity;

        print(runVelocity);

        bool hSpeed = Mathf.Abs(playerCharacter.velocity.x) > Mathf.Epsilon;

        playerAnimator.SetBool("Run", hSpeed);

    }

    private void FlipSprite()
    {
        // If the player is moving horizontally in one direction
        bool hMovement = Mathf.Abs(playerCharacter.velocity.x) > Mathf.Epsilon;

        if (hMovement)
        {
            // Reverse the current scaling of the X-axis to flip the sprite
            transform.localScale = new Vector2(Mathf.Sign(playerCharacter.velocity.x), 1f);
        }
        else
        {
            transform.localScale = new Vector2(Mathf.Sign(playerCharacter.velocity.x), 1f);
        }
    }

    private void Jump()
    {
        // Will stop the function unless true
        if(!playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if (Input.GetButtonDown("Jump"))
        {
            // Get new Y velocity based on a controllable variable
            Vector2 jumpVelocity = new Vector2(0.0f, jumpSpeed);
            playerCharacter.velocity += jumpVelocity;
        }
        
    
                
    }

    private void Climb()
    {
        //Will stop the function unless true
        if (!playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            playerAnimator.SetBool("Climb", false);
            playerCharacter.gravityScale = gravityScaleAtStart;
            return;
        }

        // "Vertical" from Input Manager
        float vMovement = Input.GetAxis("Vertical");

        // X needs to remain the same and we need to change Y
        Vector2 climbVelocity = new Vector2(playerCharacter.velocity.x, vMovement * climbSpeed);

        playerCharacter.velocity = climbVelocity;

        playerCharacter.gravityScale = 0.0f;

        bool vSpeed = Mathf.Abs(playerCharacter.velocity.y) > Mathf.Epsilon;

        playerAnimator.SetBool("Climb", vSpeed);
    }

    private void Die()
    {
        if (playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            playerAnimator.SetTrigger("die");
            playerCharacter.velocity = deathSeq;
        }
    }

}

