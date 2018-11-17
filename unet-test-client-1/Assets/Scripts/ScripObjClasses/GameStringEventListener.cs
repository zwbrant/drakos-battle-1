using RoboRyanTron.Unite2017.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStringEventListener : MonoBehaviour {
    [Tooltip("Event to register with.")]
    public GameStringEvent Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityStringEvent Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public virtual void OnEventRaised(string s1)
    {
        Response.Invoke(s1);
    }
}
