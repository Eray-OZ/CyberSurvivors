using UnityEngine;

public class WaveDamage : MonoBehaviour
{
    public float damageAmount = 20f; // Dalganın vereceği hasar
    public float lifespan = 3f; // Dalganın ne kadar süre sonra yok olacağı

    void Start()
    {
        // Belirtilen süre sonunda dalgayı yok et
        Destroy(gameObject, lifespan);
    }
    
    // Trigger tetiklendiğinde çalışır (Wave bir Trigger olduğu için)
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Temas edilen obje Düşman mı?
        if (other.CompareTag("Enemy"))
        {
            // Düşmanın HealthController bileşenine eriş
            HealthController enemyHealth = other.GetComponent<HealthController>();

            if (enemyHealth != null)
            {
                // Düşmana hasar ver
                enemyHealth.TakeDamage(damageAmount);
            }
            
            // Dalga bir kez vurduktan sonra yok olmalı
            Destroy(gameObject);
        }
    }
}