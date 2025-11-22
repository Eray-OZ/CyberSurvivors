using UnityEngine;

public class AuraController : MonoBehaviour
{
    // Inspector'dan ayarlanacak cooldown süresi
    public float damageCooldown = 0.5f; 
    
    // Kod içinde takip edilecek sonraki hasar zamanı
    private float nextDamageTime; 

    void OnTriggerStay2D(Collider2D other)
    {
        // 1. Düşman Tag'i kontrol et VE 
        // 2. Zamanlayıcının dolup dolmadığını kontrol et
        if (other.CompareTag("Enemy") && Time.time >= nextDamageTime)
        {
            // Zamanlayıcıyı güncelle: Bir sonraki hasar anını ayarla
            nextDamageTime = Time.time + damageCooldown; 

            // Düşmanın can scriptini bul
            HealthController enemyHealth = other.GetComponent<HealthController>();
            float damage = 50f; 

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                Debug.Log("Hasar verildi! Yeni Cooldown: " + damageCooldown);
            }
        }
    }
}