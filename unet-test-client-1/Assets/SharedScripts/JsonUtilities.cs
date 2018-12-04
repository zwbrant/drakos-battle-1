using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class JsonUtilities {

    public static BasicCard DeserializeCardFile(string cardGuid)
    {
        BasicCard loadedCard = new BasicCard();

        string filePath = Path.Combine(Application.streamingAssetsPath + "/Cards/", cardGuid + ".json");
        if (File.Exists(filePath))
        {
            // Read the json from the file into a string
            string dataAsJson = File.ReadAllText(filePath);
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            loadedCard = JsonUtility.FromJson<BasicCard>(dataAsJson);

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
