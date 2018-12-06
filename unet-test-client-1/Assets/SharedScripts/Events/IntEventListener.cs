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
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public virtual void OnEventRaised(int? i)
    {
        Response.Invoke(i);
    }
}