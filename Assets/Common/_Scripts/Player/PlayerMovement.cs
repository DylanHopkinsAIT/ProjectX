using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask ground;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    public bool canDoubleJump;
    private CapsuleCollider2D _collider;

    private float _dirHorizontal;
    private Rigidbody2D _playerBody;

    // Start is called before the first frame update
    private void Start()
    {
        _playerBody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        VerticalMovement(); //Call vertical movement as it is using single input and applying a force only once
    }

    private void FixedUpdate()
    {
        HorizontalMovement(); //Call horizontal movement script in FixedUpdate as its reading a continuous input and uses continuous forces
    }

    /// <summary>
    ///     Controls the vertical movement of player, primarily jumping and double jumping
    /// </summary>
    private void VerticalMovement()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //If jump key is pressed & isGrounded, allow player to jump and make allow double jumping
            if (IsGrounded())
            {
                var velocity = _playerBody.velocity;
                velocity = new Vector2(velocity.x, 0);
                velocity += new Vector2(0, jumpForce);
                _playerBody.velocity = velocity;
                canDoubleJump = true;
            }
            /*If jump key is pressed & player is NOT grounded (aka in air), allow player to jump a second
                * time and then make double jump unavailable until grounded again*/
            else if (canDoubleJump)
            {
                var velocity = _playerBody.velocity;
                velocity = new Vector2(velocity.x, 0);
                velocity = new Vector2(0, jumpForce);
                _playerBody.velocity = velocity;
                canDoubleJump = false;
            }
        }
    }

    /// <summary>
    ///     Controls the left and right movement of the player using the built in horizontal axis (between -1 and 1)
    /// </summary>
    private void HorizontalMovement()
    {
        //Set float dirHorizontal equal to the horizontal axis ranging between -1 and 1, allowing analog support.
        _dirHorizontal = Input.GetAxisRaw("Horizontal");

        //Player Move between Left(-1) & Right(1) by using horizontal as a direction, and moveSpeed as a multiplier.
        _playerBody.velocity = new Vector2(_dirHorizontal * moveSpeed, _playerBody.velocity.y);
    }

    /// <summary>
    ///     Checks if player is colliding with ground
    /// </summary>
    /// <returns>True if grounded</returns>
    public bool IsGrounded()
    {
        var bounds = _collider.bounds;
        return Physics2D.BoxCast(bounds.center, bounds.size, 0f, Vector2.down, .1f, ground);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            transform.parent = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            transform.parent = null;
        }
    }
}