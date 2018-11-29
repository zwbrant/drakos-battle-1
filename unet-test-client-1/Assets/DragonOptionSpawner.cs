using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonOptionSpawner : MonoBehaviour {
    public ContainerSet DragonOptionContainers;
    public ObjectPool DragonOptionPool;

	// Use this for initialization
	void Start () {
		


	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnDragonOption(Dragon dragon)
    {
        var dragonPF = DragonOptionPool.GetObject();
        var dragonItem = dragonPF.GetComponent<DragonOptionItem>();

        dragonItem.Initialize(dragon);

        dragonPF.transform.parent = DragonOptionContainers.GetFirstEmptyContainerFromLeft().transform;
        dragonPF.transform.localPosition = Vector3.zero;
        dragonPF.gameObject.SetActive(true);

    }

    public void SpawnAllPossibleOptions()
    {
        foreach (Dragon dragon in DragonCache.Instance.Objects)
        {
            SpawnDragonOption(dragon);
        }
    }
}
