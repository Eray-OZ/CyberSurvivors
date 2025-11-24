using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // Inspector'dan atanacak değişkenler
    public GameObject wavePrefab; 
    public float waveSpeed = 15f; 
    public float fireRate = 0.5f; 
    
    private float nextFireTime; 

    void Update()
    {
        // 1. Silah Yönü: PIVOT'u fareye göre döndür
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // 2. Otomatik Ateşleme Mantığı
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot(); 
        }
    }

    // WeaponController.cs içindeki Shoot() fonksiyonu

    void Shoot()
{
    // YENİ: Player'ın merkezinden 1.5 birim ileriye doğru bir tampon bölge oluştur
    Vector3 spawnOffset = transform.right * 1.5f; 
    
    // Oluşturulacak konumu hesapla (Pivot + Offset)
    Vector3 spawnPos = transform.position + spawnOffset;

    // 1. Dalga Prefab'ını üret (yeni pozisyonda)
    GameObject wave = Instantiate(wavePrefab, spawnPos, transform.rotation);
    
    // 2. Dalga'ya hız ver (eski kodun devamı)
    Rigidbody2D rb = wave.GetComponent<Rigidbody2D>();
    if (rb != null)
    {
        rb.linearVelocity = transform.right * waveSpeed;
    }
}
}