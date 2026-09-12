using UnityEngine;

public class PegController : MonoBehaviour  //핀 피격 처리 스크립트 
{
    private SpriteRenderer spriteRenderer;
    private bool isHit = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
     
        if (collision.gameObject.CompareTag("Orb") && !isHit)
        {
            HitPeg();
        }
    }

    void HitPeg()
    {
        isHit = true;


        spriteRenderer.color = Color.gray;

 
    }
}