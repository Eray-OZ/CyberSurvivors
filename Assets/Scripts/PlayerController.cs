using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // INSPECTOR DEĞİŞKENLERİ
    public float moveSpeed = 15f; 
    public float damagePerTick = 5f; // YENİ: Düşmandan alınacak hasar miktarını üste taşıdık
    
    // KOD İÇİN ÖZEL DEĞİŞKENLER
    private Rigidbody2D rb; 
    private SpriteRenderer sr; 
    private Animator anim; // YENİ: Animator bileşeni

    public static bool canMove = true;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>(); 
        anim = GetComponent<Animator>(); // YENİ: Animator bileşenini al
    }

    void Update()
    {

        if (canMove == false) return; // YENİ: Eğer ışınlanıyorsak hareket etme


        float moveX = Input.GetAxis("Horizontal"); 
        float moveY = Input.GetAxis("Vertical");   

        Vector2 moveDirection = new Vector2(moveX, moveY);
        rb.linearVelocity = moveDirection * moveSpeed; // Hareket kodun burada, lineer hız kullanılıyor
        
        // 1. ANİMASYON BAĞLANTISI: Hız değerini Animator'e gönderir
        // magnitude, hareketin büyüklüğünü verir (0 = durma, >0 = yürüme)
        if (anim != null) // Hata koruması
        {
            anim.SetFloat("Speed", moveDirection.magnitude); 
        }
        
        // 2. Yön Kontrolü (Flip Logic)
        if (moveX > 0)
        {
            sr.flipX = false;
        }
        else if (moveX < 0)
        {
            sr.flipX = true;
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        // Çarptığımız objenin Enemy Tag'ine sahip olup olmadığını kontrol ediyoruz
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Player'ın kendi HealthController bileşenine eriş
            HealthController playerHealth = GetComponent<HealthController>();

            // Hasar miktarını belirle (Üstteki değişkenden al)
            // Canı azalt ve kontrol et
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damagePerTick); // Hasar miktarı artık üstten alınıyor

            }
        }
    }


    // PlayerController.cs içindeki OnTriggerEnter2D fonksiyonu


    private void OnTriggerEnter2D(Collider2D other)
{
    PortalManager manager = FindAnyObjectByType<PortalManager>();
    // Rigidbody'yi alıyoruz, Collider'ı değil!
    Rigidbody2D playerRb = GetComponent<Rigidbody2D>(); 

    if (manager == null || playerRb == null) return;

    if (other.CompareTag("PortalA"))
    {
        if (manager.activePortalB != null)
        {
            // Yeni fonksiyonu çağırırken Rigidbody'yi gönder
            manager.StartTeleport(playerRb, manager.activePortalB.transform, transform.position); 
        }
    }
    else if (other.CompareTag("PortalB"))
    {
        if (manager.activePortalA != null)
        {
            // Yeni fonksiyonu çağırırken Rigidbody'yi gönder
            manager.StartTeleport(playerRb, manager.activePortalA.transform, transform.position);
        }
    }
}
  


}