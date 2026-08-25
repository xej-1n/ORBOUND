using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class OrbController : MonoBehaviour  //구슬 컨트롤러 스크립트 
{
    public float launchForce = 15f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

       
        Vector2 launchDirection = new Vector2(Random.Range(-0.5f, 0.5f), -1f).normalized;

     
        rb.AddForce(launchDirection * launchForce, ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
  
        float maxSpeed = 20f;
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }
}