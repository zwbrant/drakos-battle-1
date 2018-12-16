using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleManager : MonoBehaviour {

    public ObjectPool CirclePool;

    // circle parameters
    public int CircleCount = 6;
    public float Radius = 125;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnEmptyCircles()
    {
        for (int i = 0; i < CircleCount; i++)
        {
            var newCircle = CirclePool.GetObject();
            var x = Mathf.Sin(i * (360 / CircleCount)) * Radius;
            var y = Mathf.Cos(i * (360 / CircleCount)) * Radius;

            newCircle.transform.localPosition = new Vector2(x, y);

        }
    }
}
