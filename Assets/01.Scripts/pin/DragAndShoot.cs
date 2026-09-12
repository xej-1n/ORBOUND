using UnityEngine;
using UnityEngine.UI;

public class DragAndShoot : MonoBehaviour //구슬 기믹 스크립트 
{
    [Header("발사 설정")]
    public float maxPower = 15f;
    public float requiredChargeTime = 1.0f;

    [Header("수평 튕김 방지 설정")]
    public float antiStuckForce = 3f; 

    [Header("UI 연결칸")]
    public Image powerGauge;
    public Transform arrowPivot;

    private Rigidbody2D rb;
    private Vector2 startPoint;
    private bool isDragging = false;
    private float currentChargeTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        powerGauge.fillAmount = 0;
        arrowPivot.gameObject.SetActive(false);
    }

    void OnMouseDown()
    {
        isDragging = true;
        currentChargeTime = 0f;
        startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        arrowPivot.gameObject.SetActive(true);
        powerGauge.color = Color.white;
    }

    void OnMouseDrag()
    {
        if (!isDragging) return;

        currentChargeTime += Time.deltaTime;
        powerGauge.fillAmount = currentChargeTime / requiredChargeTime;

        if (currentChargeTime >= requiredChargeTime)
        {
            powerGauge.fillAmount = 1f;
            powerGauge.color = Color.green;
        }

        Vector2 currentPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dragDirection = startPoint - currentPoint;

        if (dragDirection != Vector2.zero)
        {
            arrowPivot.up = dragDirection;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        if (currentChargeTime >= requiredChargeTime)
        {
            Vector2 endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dragDirection = (startPoint - endPoint).normalized;

      
            rb.AddForce(dragDirection * maxPower, ForceMode2D.Impulse);
        }

        powerGauge.fillAmount = 0;
        arrowPivot.gameObject.SetActive(false);
        currentChargeTime = 0f;
    }

    void FixedUpdate()
    {

        if (rb.linearVelocity.magnitude > 0.5f)
        {
         
            if (Mathf.Abs(rb.linearVelocity.y) < 0.5f)
            {
     
                rb.AddForce(Vector2.down * antiStuckForce, ForceMode2D.Force);
            }
        }
    }
}