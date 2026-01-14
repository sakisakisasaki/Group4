using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // プレイヤーがすでに死んでいるかどうかを管理するフラグ
    bool isDead = false;

    //死亡時、見た目を消すための SpriteRenderer
    SpriteRenderer sr;

    void Start()
    {
        //開始時に SpriteRenderer を取得
        sr = GetComponent<SpriteRenderer>();
    }

    // 2Dの衝突が起きた瞬間に自動で呼ばれる関数
    void OnCollisionEnter2D(Collision2D collision)
    {
        // すでに死んでいたら、これ以上処理しない
        if (isDead) return;

        // 衝突した相手のタグが Enemy かどうかを確認
        if (collision.gameObject.CompareTag("Enemy"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            { 
                //上から踏んだか判定
                if (contact.normal.y > 0.5f)
                {
                    Enemy enemy = collision.gameObject.GetComponent<Enemy>();

                    // 踏めない敵なら死亡
                    if (enemy != null && !enemy.canBeStomped)
                    {
                        break; // 下へ（死亡処理）
                    }

                    // 踏める敵なら生存
                    return;
                }
            }

            // 死亡状態にする（多重実行防止）
            isDead = true;

            // 死亡時、プレイヤーの見た目・当たり判定を消す
            sr.enabled = false;
            GetComponent<Collider2D>().enabled = false;

            // ゲームオーバー処理
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        // 1秒間待つ
        yield return new WaitForSeconds(1f);

        // GameOver（Score）シーンへ切り替える
        SceneManager.LoadScene("Score");
    }
}