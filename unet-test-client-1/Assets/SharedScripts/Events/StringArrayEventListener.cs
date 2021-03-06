﻿using System;
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
        if (Event != null)
            Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        if (Event != null)
            Event.UnregisterListener(this);
    }

    public virtual void OnEventRaised(string[] array)
    {
        if (Response != null)
            Response.Invoke(array);
    }
}