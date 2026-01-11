using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyGroundCheckSetup : MonoBehaviour
{
    public GameObject groundCheckPrefab; // GroundCheck の空オブジェクトプレハブ
    public float groundOffset = 0.05f;   // 足元より少し下に置くオフセット

    void Awake()
    {
        // すでに GroundCheck がある場合は作らない
        Transform existing = transform.Find("GroundCheck");
        if (existing != null) return;

        // SpriteRenderer から高さを取得
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float spriteHeight = sr.bounds.size.y;

        // GroundCheck を生成
        GameObject gc;
        if (groundCheckPrefab != null)
        {
            gc = Instantiate(groundCheckPrefab, transform);
        }
        else
        {
            gc = new GameObject("GroundCheck");
            gc.transform.parent = transform;
        }

        // Enemy の足元より少し下に配置（ローカル座標）
        gc.transform.localPosition = new Vector3(0f, -spriteHeight / 2f - groundOffset, 0f);

        // GroundCheck の Transform を Enemy スクリプトに自動セット
        Enemy enemyScript = GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.groundCheck = gc.transform;
        }

        Debug.Log("GroundCheck を自動配置しました: " + gc.transform.position);
    }
}
