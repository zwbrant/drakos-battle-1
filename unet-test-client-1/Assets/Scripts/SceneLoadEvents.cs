using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;

public class SceneLoadEvents : ManagedBehaviour<SceneLoadEvents> {

    public List<SceneResponse> SceneLoadResponses;
    public GameStateEvent GameStateEvent;

    [Serializable]
    public struct SceneResponse
    {
        public string Scene;

        public Event EventOnLoad;
        public bool RaiseStateEventOnLoad;
        public ClientGameState StateOnLoad;
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
        var response = SceneLoadResponses.Find(item => item.Scene == scene.name);

        if (GameStateEvent != null && response.RaiseStateEventOnLoad)
            GameStateEvent.Raise(response.StateOnLoad);
        if (response.EventOnLoad != null)
            response.EventOnLoad.Raise();
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}


