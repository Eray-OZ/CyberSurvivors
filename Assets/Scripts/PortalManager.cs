using UnityEngine;
using System.Collections; // Coroutine için gerekli

public class PortalManager : MonoBehaviour
{
    // Inspector'dan atanacak prefab'lar
    public GameObject portalAPrefab;
    public GameObject portalBPrefab;

    // Aktif portalları tutacak referanslar
    public GameObject activePortalA;
    public GameObject activePortalB;

    void Update()
    {
     
        // Sol Click (Portal A)
        if (Input.GetMouseButtonDown(0))
        {
            PlacePortal(portalAPrefab, ref activePortalA);
        }

        // Sağ Click (Portal B)
        if (Input.GetMouseButtonDown(1))
        {
            PlacePortal(portalBPrefab, ref activePortalB);
        }
    }


    void PlacePortal(GameObject prefab, ref GameObject activePortal)
    {
        // 1. Mouse pozisyonunu al ve Dünya Koordinatına çevir
        Vector3 spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spawnPos.z = -1; // 2D için Z'yi sıfırla

        // 2. Eğer eski portal varsa, onu yok et
        if (activePortal != null)
        {
            Destroy(activePortal);
        }

        // 3. Yeni portalı üret ve referansı kaydet
        activePortal = Instantiate(prefab, spawnPos, Quaternion.identity);
    }



// PortalManager.cs (StartTeleport ve Coroutine fonksiyonlarının yerine)

// YENİ Başlatma Fonksiyonu: Coroutine'i başlatır
public void StartTeleport(Rigidbody2D playerRb, Transform targetPortal, Vector3 playerOriginalPos)
{
    // Hangi yöne iteceğimizi belirle
    Vector3 directionFromTarget = playerOriginalPos - targetPortal.position;

    // Coroutine'i başlat
    StartCoroutine(TeleportCoroutine(playerRb, targetPortal, directionFromTarget));
}

// YENİ Coroutine Fonksiyonu: Işınlanmayı yapar, Collider'ı yönetir ve YOK ETME İşlemini ekler
private IEnumerator TeleportCoroutine(Rigidbody2D playerRb, Transform targetPortal, Vector3 direction)
{
    if (playerRb == null) yield break;

    // 1. DÖNGÜYÜ KES: Simülasyonu ve Player hareketini kitle
    PlayerController.canMove = false; 
    playerRb.simulated = false;
    playerRb.linearVelocity = Vector2.zero;
    playerRb.angularVelocity = 0f;

    // 2. Player'ı hedefin hemen dışına ışınla
    playerRb.transform.position = targetPortal.position + direction.normalized * 0.5f;

    // 3. Kısa bir süre bekle (Player'ın fiziksel olarak ayrılması için)
    yield return new WaitForSeconds(0.15f); 

    // 4. FİZİĞİ AÇ
    playerRb.simulated = true;
    PlayerController.canMove = true; 
    
    // 5. YOK ETME MANTIĞI: Her iki portalı da yok et (Sorunu kesin çözer)
    if (activePortalA != null)
    {
        Destroy(activePortalA);
        activePortalA = null; // Referansı temizle
    }
    if (activePortalB != null)
    {
        Destroy(activePortalB);
        activePortalB = null; // Referansı temizle
    }
}



}