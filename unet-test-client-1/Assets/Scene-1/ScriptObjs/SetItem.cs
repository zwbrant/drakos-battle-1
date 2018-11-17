using RoboRyanTron.Unite2017.Sets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SetItem<T> : MonoBehaviour {

    public virtual RuntimeSet<T> SetProp { get; set; }

    private void OnEnable()
    {
        T component = this.GetComponent<T>();

        if (component != null)
            SetProp.Add(component);
        else
            Debug.LogError(string.Format("Gameobject {0} does not have a {1} component; can't add to {2} runtimeset", gameObject.name, typeof(T).Name, SetProp.GetType().Name));

        

    }

    private void OnDisable()
    {
        T component = this.GetComponent<T>();

        if (component != null)
            SetProp.Remove(component);
        else
            Debug.LogError(string.Format("Gameobject {0} does not have a {1} component; can't remove from {2} runtimeset", gameObject.name, typeof(T).Name, SetProp.GetType().Name));
    }
}
