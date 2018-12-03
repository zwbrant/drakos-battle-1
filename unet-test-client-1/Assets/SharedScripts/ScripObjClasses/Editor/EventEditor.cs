// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using UnityEditor;
using UnityEngine;

namespace RoboRyanTron.Unite2017.Events
{
    [CustomEditor(typeof(Event))]
    public class EventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            Event e = target as Event;
            if (GUILayout.Button("Raise"))
                e.Raise();
        }
    }
}