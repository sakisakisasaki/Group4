using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // どこからでも参照できるようにする
    public static ScoreManager instance;

    // 現在のスコア
    public int score = 1000;

    // 敵を倒したときに増える点数
    public int enemyScore = 100;

    // 1秒ごとに減る点数
    public int timeDecrease = 5;

    // 1秒計測用タイマー
    float timer = 0f;

    // カウント中かどうか
    bool isCounting = true;

    void Awake()
    {
        // すでに存在していたら新しい方を消す
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        // シーンが変わっても消えない
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        // カウント停止中なら何もしない
        if (!isCounting) return;

        // 経過時間を加算
        timer += Time.deltaTime;

        // 1秒経過したらスコアを減らす
        if (timer >= 1f)
        {
            score -= timeDecrease;

            // マイナス防止
            if (score < 0) score = 0;

            timer = 0f;
        }
    }

    // 敵を倒したときに呼ぶ
    public void AddEnemyScore()
    {
        score += enemyScore;
    }

    // クリア時に呼ぶ（時間減少を止める）
    public void StopCount()
    {
        isCounting = false;
    }
}