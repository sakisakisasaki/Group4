using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnDestroy()
    {
        // ©•ª‚ªÁ‚¦‚é‚Æ‚«‚É’Ê’m
        SpawnManager.Instance?.RemoveObject(gameObject);
    }
}
