using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IntEvent : ScriptableObject
{
    /// <summary>
    /// The list of listeners that this event will notify if it is raised.
    /// </summary>
    private readonly List<IntEventListener> eventListeners =
        new List<IntEventListener>();

    public void Raise(int? i)
    {
        for (int x = eventListeners.Count - 1; x >= 0; x--)
            eventListeners[x].OnEventRaised(i);
    }

    public void RegisterListener(IntEventListener listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(IntEventListener listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}