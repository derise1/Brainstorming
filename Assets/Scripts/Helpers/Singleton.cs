using UnityEngine;

[DisallowMultipleComponent]
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected bool isDontDestroyOnLoad = false;

    public static T Instance
    {
        get
        {
            if (Reference == null)
            {
                if (!(Reference = FindObjectOfType<T>()))
                {
                    throw new MissingReferenceException($"The singleton reference to a {typeof(T).Name} does not found!");
                }
            }

            return Reference;
        }
    }

    public static bool HasReference
    {
        get
        {
            if (Reference == null)
            {
                return (Reference = FindObjectOfType<T>()) != null;
            }

            return true;
        }
    }

    protected static T Reference;

    protected virtual void Awake()
    {
        if (isDontDestroyOnLoad)
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}