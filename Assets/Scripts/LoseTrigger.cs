using UnityEngine;

public class LoseTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("powerUp"))
        {
            Destroy(collision.gameObject);
        }
    }
}
