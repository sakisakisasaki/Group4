using UnityEngine;

public class TitleBounce : MonoBehaviour
{
    [Header("Scale")]
    public float startScale = 0.8f;
    public float overshootScale = 1.1f;
    public float endScale = 1.0f;

    [Header("Timing (seconds)")]
    public float toOvershootTime = 0.22f;
    public float settleTime = 0.18f;

    RectTransform rt;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        // タイトル表示のたびに弾ませたいなら OnEnable が簡単
        StopAllCoroutines();
        StartCoroutine(Bounce());
    }

    System.Collections.IEnumerator Bounce()
    {
        // 0.8 → 1.1
        rt.localScale = Vector3.one * startScale;
        yield return LerpScale(startScale, overshootScale, toOvershootTime);

        // 1.1 → 1.0
        yield return LerpScale(overshootScale, endScale, settleTime);
        rt.localScale = Vector3.one * endScale;
    }

    System.Collections.IEnumerator LerpScale(float a, float b, float t)
    {
        float time = 0f;
        while (time < t)
        {
            time += Time.unscaledDeltaTime; // メニューでも止まらない
            float u = Mathf.Clamp01(time / t);

            // ちょい気持ちいい補間（イージング）
            u = 1f - Mathf.Pow(1f - u, 3f); // EaseOutCubic

            float s = Mathf.Lerp(a, b, u);
            rt.localScale = Vector3.one * s;
            yield return null;
        }
    }
}
