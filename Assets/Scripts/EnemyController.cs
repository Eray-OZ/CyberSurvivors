using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Inspector'dan atanacak değişkenler
    public Transform player; 
    public float moveSpeed = 3f;
    
    // Kodda erişim için gerekli özel değişkenler
    private Rigidbody2D rb;
    private SpriteRenderer sr; // Görsel bileşeni tutacak değişken

    void Start()
    {
        // Oyun başladığında gerekli bileşenleri alıyoruz
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>(); 
    }

    void Update()
    {
        // 1. Yönü hesapla ve Normalleştir (Hedef - Başlangıç)
        Vector2 direction = (player.position - transform.position).normalized;

        // 2. Fizik motoruna hızı at (Düşmanı hareket ettir)
        rb.linearVelocity = direction * moveSpeed;

        // 3. Yön Kontrolü (Flip Logic): Sadece sola hareket ediyorsa çevir
        if (sr != null) 
        {
            if (direction.x > 0)
            {
                sr.flipX = false;  // Player sağdaysa sağa bak (Normal)
            }
            else if (direction.x < 0)
            {
                sr.flipX = true;   // Player soldaysa sola bak (Çevir)
            }
        }
    }
}