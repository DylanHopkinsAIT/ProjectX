using UnityEngine;

public class PlayerMovementAnimations : MonoBehaviour
{
    private static readonly int State = Animator.StringToHash("state");

    private Animator _animator;
    private PlayerMovement _movementScript;
    private GameObject _player;
    private Rigidbody2D _playerBody;

    private float _dirHorizontal;
    private bool _isGrounded;
    private bool _canDoubleJump;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerBody = GetComponent<Rigidbody2D>();
        _player = GameObject.Find("Player");
        _movementScript = _player.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        PlayMovementAnimations();
    }

    /// <summary>
    ///     This method determines what animation is to play and when (its a also a fucking mess right now)
    /// </summary>
    private void PlayMovementAnimations()
    {
        AnimationState state;
        var faceLeft = new Vector3(-1, 1, 1); //Modify X value of scale to flip player object left
        var faceRight = new Vector3(1, 1, 1); //Modify Y value of scale to flip player object right

        _dirHorizontal = Input.GetAxisRaw("Horizontal");
        _isGrounded = _movementScript.IsGrounded();
        _canDoubleJump = _movementScript.canDoubleJump;

        //Horizontal Movement Animations
        if (_dirHorizontal > 0f && _isGrounded) //Running right
        {
            state = AnimationState.Run;
            transform.localScale = faceRight;
        }
        else if (_dirHorizontal < 0f && _isGrounded) //Running left
        {
            state = AnimationState.Run;
            transform.localScale = faceLeft;
        }
        else
        {
            state = AnimationState.Idle0; //Not moving
        }

        //Vertical Movement Animations
        if (_playerBody.velocity.y > .01f && !_isGrounded) //Jumping Animations
        {
            switch (_canDoubleJump)
            {
                //Double jump
                case false when _playerBody.velocity.x > 0.1f: //Moving Right
                    state = AnimationState.Somersault;
                    transform.localScale = faceRight;
                    break;
                case false when _playerBody.velocity.x < -0.1f: //Moving Left
                    state = AnimationState.Somersault;
                    transform.localScale = faceLeft;
                    break;
                case false: //Not Moving
                    state = AnimationState.Somersault;
                    break;

                //Regular jump
                case true when _playerBody.velocity.x > 0.1f: //Moving Right
                    state = AnimationState.Jump;
                    transform.localScale = faceRight;
                    break;
                case true when _playerBody.velocity.x < -0.1f: //Moving Left
                    state = AnimationState.Jump;
                    transform.localScale = faceLeft;
                    break;
                case true: //Not moving
                    state = AnimationState.Somersault; //Not Moving
                    break;
            }
        }

        else if (_playerBody.velocity.y < -.01f && !_isGrounded) //Falling Animations
        {
            if (_playerBody.velocity.x > 0.1f) //Falling Right
            {
                state = AnimationState.Fall;
                transform.localScale = faceRight;
            }
            else if (_playerBody.velocity.x < -0.1f) //Falling Left
            {
                state = AnimationState.Fall;
                transform.localScale = faceLeft;
            }
            else //Falling Straight
            {
                state = AnimationState.Fall;
            }
        }

        _animator.SetInteger(State, (int) state); //Cast enum value as integer to pass to Animator
    }

    /// <summary>
    ///     Enum containing all of the different possible animations
    /// </summary>
    private enum AnimationState
    {
        AirAttack1,
        AirAttack2,
        Attack1,
        Attack2,
        Attack3,
        Cast,
        CollectItem,
        CornerClimb,
        CornerGrab,
        CornerJump,
        Crouch,
        Die,
        Fall,
        Hurt,
        Idle0,
        Idle1,
        Jump,
        LadderClimb,
        Run,
        Slide,
        Somersault,
        SwordDraw,
        SwordSheathe,
        WallSlide
    }
}