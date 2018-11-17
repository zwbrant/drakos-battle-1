using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class JsonUtilities {

    public static Card DeserializeCardFile(string cardGuid)
    {
        Card loadedCard = new Card();

        string filePath = Path.Combine(Application.streamingAssetsPath, cardGuid + ".json");
        if (File.Exists(filePath))
        {
            // Read the json from the file into a string
            string dataAsJson = File.ReadAllText(filePath);
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            loadedCard = JsonUtility.FromJson<Card>(dataAsJson);

            Debug.Log("Deserialized " + loadedCard.name);

        }
        else
        {
            Debug.LogError("Cannot load card with GUID " + cardGuid);

            
        }

        return loadedCard;
    }


}
