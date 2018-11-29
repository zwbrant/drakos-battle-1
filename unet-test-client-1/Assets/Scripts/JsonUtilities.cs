using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class JsonUtilities {

    public static Card DeserializeCardFile(string cardGuid)
    {
        Card loadedCard = new Card();

        string filePath = Path.Combine(Application.streamingAssetsPath + "/Cards/", cardGuid + ".json");
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
            Debug.LogError("Cannot load card at path: " + filePath);


        }

        return loadedCard;
    }

    public static DragonTest DeserializeDragonFile(string cardGuid)
    {
        DragonTest loadedCard = new DragonTest();

        string filePath = Path.Combine(Application.streamingAssetsPath + "/Dragons/", cardGuid + ".json");
        if (File.Exists(filePath))
        {
            // Read the json from the file into a string
            string dataAsJson = File.ReadAllText(filePath);
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            var cat = JsonUtility.FromJson<DragonTest[]>(dataAsJson);

            Debug.Log("Deserialized " + loadedCard.name);

        }
        else
        {
            Debug.LogError("Cannot load card at path: " + filePath);


        }

        return loadedCard;
    }

    public static T DeserializeFile<T>(string filePath) {
        if (File.Exists(filePath))
        {
            using (StreamReader file = File.OpenText(@filePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                T obj = (T)serializer.Deserialize(file, typeof(T));
                return obj;
            }
        } else
        {
            throw new FileNotFoundException("No JSON file found at " + filePath);
        }

    }
}
