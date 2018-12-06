using System;
using UnityEngine;
using UnityEngine.Events;

public class StringArrayEventListener : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public StringArrayEvent Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityStringArrayEvent Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public virtual void OnEventRaised(string[] array)
    {
        Response.Invoke(array);
    }
}