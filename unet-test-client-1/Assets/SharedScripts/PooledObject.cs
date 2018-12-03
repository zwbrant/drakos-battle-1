using UnityEngine;

public class PooledObject : MonoBehaviour
{

    public ObjectPool Pool;


    private void OnEnable()
    {

    }

    public void ReturnToPool()
    {
        Pool.AddObject(this);
    }

}