using UnityEngine;

public class DragAndShoot : MonoBehaviour //구글 당길때 코드 
{
    public float power = 5f; 
    private Rigidbody2D rb;
    private Vector2 startPoint;
    private Vector2 endPoint;

    void Start()
    {
   
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMouseDown()
    {
     
        startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }


    void OnMouseUp()
    {
 
        endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        Vector2 direction = (startPoint - endPoint);

       
        rb.AddForce(direction * power, ForceMode2D.Impulse);
    }
}