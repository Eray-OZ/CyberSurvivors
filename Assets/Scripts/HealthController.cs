using UnityEngine;

public class HealthController : MonoBehaviour
{
    // Bu değer Inspector'da ayarlanacak
    public float maxHealth = 100f; 

    // Anlık can değeri
    private float currentHealth; 

    void Start()
    {
        // Oyun başladığında, mevcut can maksimum cana eşitlenir.
        currentHealth = maxHealth; 
    }



    public void TakeDamage(float damageAmount)
{
    Debug.Log(gameObject.name + " hasar alıyor: " + damageAmount);
    // Hasarı uygula
    currentHealth -= damageAmount;
    
    if (currentHealth <= 0)
    {
        // Objeyi sahneden kaldir
        Destroy(gameObject); 
    }
}

}