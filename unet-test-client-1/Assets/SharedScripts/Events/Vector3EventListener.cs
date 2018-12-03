using RoboRyanTron.Unite2017.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector3EventListener : MonoBehaviour {
    [Tooltip("Event to register with.")]
    public Vector3Event Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityVector3Event Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public virtual void OnEventRaised(Vector3 vector)
    {
        Response.Invoke(vector);
    }
}
