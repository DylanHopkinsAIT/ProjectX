using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemCollector : MonoBehaviour
{
    private int score;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        //Collect coin
        if (col.gameObject.CompareTag("Coin"))
        {
            Destroy(col.gameObject);
            score += 10;
        }

        //Collect Diamond
        if (col.gameObject.CompareTag("Diamond"))
        {
            Destroy(col.gameObject);
            score += 100;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
