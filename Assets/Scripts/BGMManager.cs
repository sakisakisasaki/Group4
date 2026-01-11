using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    private static BGMManager instance;

    public AudioSource audioSource;

    // シーンごとのBGMをセット
    public AudioClip GameSceneBGM;
    public AudioClip ScoreBGM;
    public AudioClip StartBGM;
    public AudioClip ClearBGM; 

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        // シーンが変わるたびに呼ばれるイベントに登録
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // 忘れず解除
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // シーン切り替え時に呼ばれる
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioClip clipToPlay = null;

        // シーン名でBGMを切り替え
        switch (scene.name)
        {
            case "GameScene":
                clipToPlay = GameSceneBGM;
                break;
            case "Score":
                clipToPlay = ScoreBGM;
                break;
            case "Start":
                clipToPlay = StartBGM;
                break;
            case "Clear": 
                clipToPlay = ClearBGM;
                break;
        }

        // 既に同じ曲なら再生しない
        if (clipToPlay != null && audioSource.clip != clipToPlay)
        {
            audioSource.clip = clipToPlay;
            audioSource.Play();
        }
    }
}