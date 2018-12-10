using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class IntEventListener : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public IntEvent Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityIntEvent Response;

    private void OnEnable()
    {
        if (Event != null)
            Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        if (Event != null)
            Event.UnregisterListener(this);
    }

    public virtual void OnEventRaised(int? i)
    {
        if (Response != null)
            Response.Invoke(i);
    }
}