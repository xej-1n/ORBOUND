using UnityEngine;

public class balldeadzone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            Destroy(collision.gameObject);
        }
    }
}
