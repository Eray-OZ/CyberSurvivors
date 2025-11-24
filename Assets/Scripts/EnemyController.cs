using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Inspector'dan atanacaklar
    public Transform player; 
    public float moveSpeed = 3f;
    
    // Kodda erişim için gerekli özel değişkenler
    private Rigidbody2D rb;
    private SpriteRenderer sr; 

    void Start()
    {
        // Bileşenleri al
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>(); 
    }

    void Update()
    {
        if (player == null) return; // Player öldüyse takibi durdur

        // 1. Yönü hesapla ve Normalleştir
        Vector2 direction = (player.position - transform.position).normalized;

        // 2. Fizik motoruna hızı ata
        rb.linearVelocity = direction * moveSpeed;

        // 3. Yön Kontrolü (Flip Logic)
        if (sr != null) 
        {
            if (direction.x > 0)
            {
                sr.flipX = false;
            }
            else if (direction.x < 0)
            {
                sr.flipX = true;
            }
        }
    }
}