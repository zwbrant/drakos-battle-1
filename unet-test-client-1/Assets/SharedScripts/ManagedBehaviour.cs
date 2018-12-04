using UnityEngine;
using System.Collections;

public class ManagedBehaviour<T> : MonoBehaviour where T : ManagedBehaviour<T>
{
    private static T m_Instance = null;
    public static T Instance
    {
        get
        {
            if (m_Instance == null)
            {
                Debug.Log("Creating Instance");
                m_Instance = (T)FindObjectOfType(typeof(T));
                if (m_Instance == null)
                {
                    m_Instance = (new GameObject(typeof(T).Name)).AddComponent<T>();
                }
                m_Instance.Init();

            }
            return m_Instance;
        }
    }

    public virtual void Init()
    {

    }

    protected virtual void Start()
    {
        if (m_Instance != null && m_Instance != this)
        {
            Destroy(this);
            Destroy(gameObject);
        }
        else if (m_Instance == null)
        {
            m_Instance = (T)this;
            Init();
        }
    }
}