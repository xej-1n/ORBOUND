using UnityEngine;


public enum PegType { TopBumper, BottomBumper, LeftObstacle, RightObstacle }

public class PegController : MonoBehaviour
{
    [Header("핀 설정")]
    public PegType pegType; 

    private SpriteRenderer spriteRenderer;
    private int hitCount = 0;
    private int maxHits = 6; 

  
    private float lastHitTime = -1f;
    private float hitCooldown = 0.3f; 

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Orb"))
        {
        
            if (Time.time - lastHitTime >= hitCooldown)
            {
                HitPeg();
            }
        }
    }

    void HitPeg()
    {
        if (hitCount >= maxHits) return; 

        hitCount++;
        lastHitTime = Time.time; 

    

        if (hitCount >= maxHits)
        {
           
            Color fadedColor = spriteRenderer.color;
            fadedColor.a = 0.4f;
            spriteRenderer.color = fadedColor;
        }
    }
}