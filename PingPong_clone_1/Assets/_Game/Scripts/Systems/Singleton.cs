using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Component {
    public static T Instance => _instance ??= FindObjectOfType<T>() ;
    private static T _instance;

 
    
    public virtual void OnDestroy() {
        _instance = null;
    }

}