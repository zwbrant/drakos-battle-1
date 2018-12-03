using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;

public class SceneLoadEvents : ManagedBehaviour<SceneLoadEvents> {

    public List<SceneResponse> SceneLoadResponses;

    [Serializable]
    public struct SceneResponse
    {
        public string Scene;
        public UnityEvent Response;
    }

    public override void Init()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var response = SceneLoadResponses.Find(item => item.Scene == scene.name).Response;
        if (response != null)
            response.Invoke();
    }
}


