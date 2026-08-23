using UnityEngine;

public class BallBoun : MonoBehaviour
{
    [Header("튕김 기본 설정")]
    public float _minBounceForce = 8f;
    public float _maxBounceForce = 14f;

    [Header("연속 충돌 방지")]
    public float _bounceCooldown = 0.05f; //충돌 쿨탐
    private float _lastBounceTime;

    private Rigidbody2D _rb;
    private void Awake()
    {
        _rb= GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Time.time < _lastBounceTime + _bounceCooldown) return;

        if(collision.gameObject.CompareTag("Pin"))
        {
            _lastBounceTime = Time.time;

            Pin hitPin = collision.gameObject.GetComponent<Pin>();
            if(hitPin != null && ScoreManager.instance != null)
            {
                ScoreManager.instance.AddScore(hitPin.ScoreValue);
            }
            Vector2 baseDirection = (transform.position - collision.transform.position).normalized;
        }
    }

}
