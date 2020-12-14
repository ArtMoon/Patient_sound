using UnityEngine;

public class Singleton<T> : MonoBehaviour where T: MonoBehaviour 
{
    public static T Instance { get; private set; }

   
    protected void InitInstance(T obj)
    {
        if(Instance == default)
        {
            Instance = obj;
            DontDestroyOnLoad(this);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

}
