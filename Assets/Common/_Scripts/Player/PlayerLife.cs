using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerLife : MonoBehaviour
{
    private PlayerMovement movementScript;
    private Animator anim;
    private PlayerMovementAnimations animationScript;
    private static readonly int Death = Animator.StringToHash("death");

    // Start is called before the first frame update
    private void Start()
    {
        movementScript = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
        animationScript = GetComponent<PlayerMovementAnimations>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //If player collides with a trap call the die function
        if (col.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }

    /// <summary>
    /// Handles events that occur when player dies (in game)
    /// </summary>
    private void Die()
    {
        movementScript.enabled = false;   //Disable player input
        
        anim.SetTrigger(Death); //Play death animation

        animationScript.enabled = false; //Disable any other animations
    }

    /// <summary>
    /// Restarts level when called
    /// </summary>
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Reload current level
    }
}
