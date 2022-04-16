using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    /// <summary>
    /// Destroy trap object if falls out of map
    /// </summary>
    /// <param name="other">Other object</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trap"))
        {
            Destroy(other.gameObject);
        }
    }
}
