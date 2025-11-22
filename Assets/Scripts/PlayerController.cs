using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 15f; 
    private Rigidbody2D rb; 
    private SpriteRenderer sr; // Görseli tutacak değişken

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>(); 
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal"); 
        float moveY = Input.GetAxis("Vertical");   

        Vector2 moveDirection = new Vector2(moveX, moveY);
        rb.linearVelocity = moveDirection * moveSpeed; 
        
        // Yön Kontrolü (Flip Logic)
        if (moveX > 0)
        {
            sr.flipX = false; // Sağa hareket ediyorsa, flip yapma
        }
        else if (moveX < 0)
        {
            sr.flipX = true;  // Sola hareket ediyorsa, görseli yatay çevir
        }
    }
}