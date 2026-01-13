using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    public GameObject prefab;
    public int maxCount = 3;

    private List<GameObject> objects = new List<GameObject>();

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnAtMousePosition();
        }
    }

    void SpawnAtMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(Camera.main.transform.position.z);

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 0f;

        GameObject obj = Instantiate(
            prefab,
            worldPos,
            prefab.transform.rotation
        );

        objects.Add(obj);

        // 上限チェック
        if (objects.Count > maxCount)
        {
            Destroy(objects[0]);
            objects.RemoveAt(0);
        }
    }

    
    public void RemoveObject(GameObject obj)
    {
        objects.Remove(obj);
    }
}
