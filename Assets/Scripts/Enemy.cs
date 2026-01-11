using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public float moveDistance = 3f;

    // 踏めるかどうかのフラグ
    public bool canBeStomped = true;

    // 動くかどうかのフラグ
    public bool canMove = true;

    //  自動ジャンプするかどうかのフラグ
    public bool canAutoJump = false;

    //  ジャンプ力
    public float jumpPower = 6f;

    // ジャンプ間隔（秒）
    public float jumpInterval = 2f;

    //接地判定用
    public Transform groundCheck;           // 足元の位置
    public float groundCheckRadius = 0.1f;  // 円の半径
    public LayerMask groundLayer;           // 地面レイヤー

    private Vector3 startPos;
    private int direction = 1;

    // Rigidbody2D を取得してジャンプに使う
    private Rigidbody2D rb;

    //  ジャンプタイマー（ジャンプ間隔を測るため）
    private float jumpTimer = 0f;

    void Start()
    {
        startPos = transform.position;

        // Rigidbody2D を取得
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 動けない敵は処理しない
        if (canMove)
        {
            // 横移動
            transform.Translate(Vector2.right * speed * direction * Time.deltaTime);

            // 移動範囲を超えたら方向反転
            if (Vector3.Distance(startPos, transform.position) >= moveDistance)
            {
                direction *= -1;
            }
        }

        // 自動ジャンプ処理
        if (canAutoJump)
        {
            // 時間を進める
            jumpTimer += Time.deltaTime;

            // ジャンプ間隔が来て、地面にいる場合だけジャンプ
            if (jumpTimer >= jumpInterval && IsGrounded())
            {
                Jump();
                jumpTimer = 0f; // タイマーリセット
            }
        }
    }

    //  地面にいるか簡易判定
    bool IsGrounded()
    {
        // 足元に小さな円を置き、地面レイヤーと接触しているかチェック
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer) != null;
    }

    //  ジャンプ処理
    void Jump()
    {
        // Rigidbody2D に上向きの衝撃力を与える
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // プレイヤー以外は無視
        if (!collision.gameObject.CompareTag("Player")) return;

        // 踏める敵だけ踏み判定
        if (canBeStomped)
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                // 上から踏んだ判定
                if (contact.normal.y < 0)
                {
                    Debug.Log("敵を上から踏んだ！");
                    Die();
                    return;
                }
            }
        }

        // 踏めない or 上からでない場合
        Debug.Log("プレイヤーがダメージを受ける");
    }

    void Die()
    {
        // 敵が死んだら動きを止める
        canMove = false;

        // オブジェクトを削除
        Destroy(gameObject);
    }
}