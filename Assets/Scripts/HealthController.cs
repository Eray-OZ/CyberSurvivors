using UnityEngine;
using UnityEngine.UI; // UI sistemini kullanmak için eklendi

public class HealthController : MonoBehaviour
{
    public float maxHealth = 100f; 
    public Slider healthSlider; // Health Bar Slider'ı (Inspector'dan atayacağız)
    
    private float currentHealth; 

    void Start()
    {
        currentHealth = maxHealth;
        
        // Slider'ı başlangıçta ayarla
        if (healthSlider != null) 
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        // Hasarı uygula
        currentHealth -= damageAmount;
        
        // Slider'ı güncelle
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
        
        if (currentHealth <= 0)
        {
            // Eğer canı biten Player ise, Game Over sinyali gönder
            if (CompareTag("Player"))
            {
                Time.timeScale = 0f; // Oyunu durdur
                Debug.Log("GAME OVER!");
            }
            // Objeyi sahneden kaldır
            Destroy(gameObject); 
        }
    }
}