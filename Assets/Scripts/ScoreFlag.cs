using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // プレイヤーが入ったか判定
        if (other.CompareTag("Player"))
        {
            // スコアを確定（時間減少を止める）
            ScoreManager.instance.StopCount();

        }
    }
}