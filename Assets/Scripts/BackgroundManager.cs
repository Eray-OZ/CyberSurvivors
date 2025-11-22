using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public Transform player; // Hiyerarşiden atanan Player objesi
    private float tileSize; // Boyut artık kodla dinamik olarak hesaplanacak
    private Transform[] tiles; // Tüm fayansları tutacak dizi

    void Start()
    {
        // 1. Dinamik Boyut Hesaplama (Fayansın gerçek dünya boyutunu öğrenme)
        // İlk fayansın SpriteRenderer bileşenini alıyoruz
        SpriteRenderer tileRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        
        // Fayansın X eksenindeki gerçek Dünya Boyutunu hesapla
        tileSize = tileRenderer.bounds.size.x; 

        // 2. Alt objeleri diziye doldurma
        tiles = new Transform[transform.childCount]; 
        for (int i = 0; i < transform.childCount; i++)
        {
            tiles[i] = transform.GetChild(i);
        }
    }

    // Harita sarma mantığı için LateUpdate kullanılır (Player hareketinden sonra çalışır)
    void LateUpdate() 
    {
        foreach (Transform tile in tiles)
        {
            // Player'dan fayansa olan X ve Y mesafesini hesapla
            float deltaX = player.position.x - tile.position.x;
            float deltaY = player.position.y - tile.position.y;

            // X EKSENİNDE SARMA KONTROLÜ
            // Fayansın yarısı kadar uzaklaşmışsa (daha pürüzsüz olması için) ışınla.
            if (Mathf.Abs(deltaX) >= tileSize / 2f)
            {
                // Player'ın gittiği yöne (+1 veya -1) doğru tam bir fayans boyu kadar ışınla.
                tile.position += Vector3.right * tileSize * Mathf.Sign(deltaX);
            }

            // Y EKSENİNDE SARMA KONTROLÜ
            if (Mathf.Abs(deltaY) >= tileSize / 2f)
            {
                tile.position += Vector3.up * tileSize * Mathf.Sign(deltaY);
            }
        }
    }
}