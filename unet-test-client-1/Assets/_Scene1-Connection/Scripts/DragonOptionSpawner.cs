using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DragonOptionSpawner : MonoBehaviour {
    public ContainerSet DragonOptionContainers;
    public ObjectPool DragonOptionPool;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnDragonOption(Dragon dragon, ContainerItem container)
    {
        var dragonPF = DragonOptionPool.GetObject();
        var dragonItem = dragonPF.GetComponent<DragonOptionItem>();

        dragonItem.Initialize(dragon);

        dragonPF.transform.parent = container.transform;
        dragonPF.transform.localPosition = Vector3.zero;
        dragonPF.gameObject.SetActive(true);
    }

    public void PopulateContainersFromLeftToRight()
    {
        var sortedContainers = DragonOptionContainers.SortFromLeftToRight();

        int dragonIndex = 0;

        for (int i = 0; i < sortedContainers.Count; i++)
        {
            // we've run out of dragons to spawn
            if (dragonIndex + 1 > DragonCache.Instance.Objects.Count)
            {
                break;
            }

            // this is an empty container and we have a dragon - spawn it
            if (sortedContainers[i].IsEmpty)
            {
                SpawnDragonOption(DragonCache.Instance.Objects[dragonIndex], sortedContainers[i]);
                dragonIndex++;
            } 
        }

        // if we have a container with a dragon in it, let's select it by default
        if (sortedContainers.Count > 0 && !sortedContainers[0].IsEmpty)
        {
            var firstOption = sortedContainers[0].transform.GetChild(0).gameObject;

            firstOption.GetComponent<Clickable>().ClickedEvent.Invoke(firstOption);
        }
    }
}
