using UnityEngine;
using UnityEngine.UI; 
using System.Collections; // Coroutine için eklendi

public class HealthController : MonoBehaviour
{
    public float maxHealth = 100f; 
    public Slider healthSlider; 
    
    private float currentHealth; 
    private SpriteRenderer sr; // YENİ: Görsel bileşeni tutar
    private Color originalColor; // YENİ: Orijinal rengi tutar

    void Start()
    {
        currentHealth = maxHealth;
        
        // Slider'ı başlangıçta ayarla
        if (healthSlider != null) 
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
        
        // YENİ: SpriteRenderer'ı al ve orijinal rengi kaydet
        sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            originalColor = sr.color;
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
        
        // YENİ: Hasar tepkimesi için Coroutine'i başlat
        if (sr != null)
        {
            StartCoroutine(DamageFlash());
        }
        
        if (currentHealth <= 0)
        {
            if (CompareTag("Player"))
            {
                Time.timeScale = 0f; // Oyunu durdur
                Debug.Log("GAME OVER!");
            }
            // Objeyi sahneden kaldır
            Destroy(gameObject); 
        }
    }

   // HealthController.cs içindeki DamageFlash fonksiyonu

private IEnumerator DamageFlash() 
{
    if (sr == null) yield break; 
    
    // YENİ DÜZELTME: Parlak kırmızı renk tanımlayıp opak yapıyoruz (Alpha = 1)
    Color flashColor = Color.red; 
    
    sr.color = flashColor; // 1. Parlat: Kırmızı ve Tam Opak yap
    yield return new WaitForSeconds(0.1f); // 2. Kısa bekleme
    sr.color = originalColor; // 3. Eski Renge Dön
}
}