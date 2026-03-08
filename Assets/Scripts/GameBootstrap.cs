using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    void Awake()
    {
        CreateIfMissing<GameManager>();
        // CreateIfMissing<StatsTracker>();
    }

    void CreateIfMissing<T>() where T : MonoBehaviour
    {
        if (Object.FindAnyObjectByType<T>() == null)
        {
            GameObject obj = new GameObject(typeof(T).Name);
            obj.AddComponent<T>();
        }
    }
}