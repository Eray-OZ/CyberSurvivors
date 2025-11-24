using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    // Inspector'dan atanan Player referansı
    public Transform player; 
    public float tileSize = 20f; // Fayans boyutu
    
    private Transform[] tiles; 

    void Start()
    {
        // Eğer TileSize 0 ise, dinamik olarak hesapla (önceki hatalardan korunmak için)
        if (tileSize <= 0 && transform.childCount > 0)
        {
            SpriteRenderer tileRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
            if (tileRenderer != null)
            {
                tileSize = tileRenderer.bounds.size.x;
            }
        }

        // Alt objeleri diziye doldurma
        tiles = new Transform[transform.childCount]; 
        for (int i = 0; i < transform.childCount; i++)
        {
            tiles[i] = transform.GetChild(i);
        }
    }

    void LateUpdate() 
    {
        // KRİTİK HATA ÇÖZÜMÜ: Eğer Player objesi yoksa (null), fonksiyonu hemen durdur.
        if (player == null) return; 

        // Bu kontrol sayesinde artık player.position okunduğunda hata vermez.
        foreach (Transform tile in tiles)
        {
            // Hata veren satır buydu: Şimdi güvenli.
            float deltaX = player.position.x - tile.position.x;
            float deltaY = player.position.y - tile.position.y;

            // X EKSENİNDE SARMA KONTROLÜ
            if (Mathf.Abs(deltaX) >= tileSize / 2f)
            {
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