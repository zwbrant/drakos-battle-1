using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class Cache<T> : ManagedBehaviour<Cache<T>>
{
    public string JsonPathInStreamingAssets;
    public bool PathIsFolder = false;
    public UnityEvent ReadyEvent;

    public bool Ready { get; private set; }
    public List<T> Objects { get; private set; }

    public override void Init()
    {
        Objects = new List<T>();
        PopulateCache();
    }

    public void PopulateCache()
    {
        if (!PathIsFolder)
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, JsonPathInStreamingAssets + ".json");

            Objects = JsonUtilities.DeserializeFile<List<T>>(filePath);

            Ready = true;
            if (ReadyEvent != null) { ReadyEvent.Invoke(); }
        }
        else
        {
            throw new System.NotImplementedException();
        }
    }
}
