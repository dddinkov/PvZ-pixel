using UnityEditor.UI;
using UnityEngine;

public abstract class SingletonResourceManager<T> : ResourceManager
    where T : MonoBehaviour
{
    public static T Instance {get; private set;}

    protected override void Awake()
    {
        base.Awake();

        InitializeSingleton();
    }

    protected virtual void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;
        }
    }

        private void InitializeSingleton()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this as T;
    }
}