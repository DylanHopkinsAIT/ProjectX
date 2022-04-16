using UnityEngine;

public class StickToPlatform : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        //If player is standing on the platform
        if (col.gameObject.name == "Player")
        {
            //Make player a child of the platform
            col.gameObject.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //If player leaves platform
        if (other.gameObject.name == "Player")
        {
            //Remove parent from player object
            other.gameObject.transform.SetParent(null);
        }
    }
}
