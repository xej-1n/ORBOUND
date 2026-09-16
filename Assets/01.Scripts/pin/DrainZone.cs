using UnityEngine;

public class DrainZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Orb"))
        {
            ScoreManager.instance.EndTurn();
        }
    }
}