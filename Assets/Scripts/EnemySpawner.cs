using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject playerTarget;

    // Üretilecek düşman Prefab'ı
    public GameObject enemyPrefab; 
    


    // Düşman üretim sıklığı (2 saniyede bir başlasın)
    public float spawnInterval = 2f; 
    
    // Oyuncudan ne kadar uzakta spawn edileceği (ekran dışı yarıçap)
    public float spawnRadius = 10f; 
    

    void SpawnEnemy()
{

    // Bu fonksiyon her 2 saniyede bir çalışacak.

    // 1. Player'ın pozisyonunu al
    Vector3 playerPos = playerTarget.transform.position;
    
    // 2. Rastgele bir yön vektörü oluştur ve uzunluğunu spawnRadius kadar uzat
    // insideUnitCircle.normalized, bize rastgele bir 1 birimlik vektör verir.
    Vector3 randomOffset = Random.insideUnitCircle.normalized * spawnRadius;

    // 3. Spawn noktasını hesapla (Player'ın pozisyonu + Rastgele Uzaklık)
    Vector3 spawnPos = playerPos + randomOffset; 

    // 4. Hesaplanan pozisyonda düşmanı üret ve bir değişkene kaydet
    GameObject newEnemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

    // 5. Yeni düşmanın EnemyController'ını bul
    EnemyController enemyController = newEnemy.GetComponent<EnemyController>();
    
    // 6. Ve Player referansını ona ver (Hata veren kısmı burada çözüyoruz!)
    enemyController.player = playerTarget.transform;

}

    void Start()
{
    // "SpawnEnemy" isimli fonksiyonu 1 saniye sonra başlat ve 
    // sonra her 'spawnInterval' saniyede bir tekrar et
    InvokeRepeating("SpawnEnemy", 1f, spawnInterval);
}


}