using System;
using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public Event Event;


    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent Response;

    public void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public virtual void OnEventRaised()
    {
        Response.Invoke();
    }
}