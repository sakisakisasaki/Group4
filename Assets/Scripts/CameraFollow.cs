using UnityEngine;

public class CameraFollowX : MonoBehaviour
{
    private Transform player;

    public float minX = -20f;   // ステージ左端
    public float maxX = 120f;   // ステージ右端
    public float offsetX = 3f;  //プレイヤーを左に置く量

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Playerタグのオブジェクトが見つかりません");
        }
    }

    void LateUpdate()
    {
        if (player == null) return;//プレイヤーがいないと動かない

        // プレイヤー位置＋オフセット
        float targetX = player.position.x + offsetX;

        // ステージ端で制限
        float clampedX = Mathf.Clamp(targetX, minX, maxX);

        transform.position = new Vector3(
            clampedX,              
            transform.position.y,
            transform.position.z
        );
    }
}