using RoboRyanTron.Unite2017.Sets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleManager : MonoBehaviour {

    public ObjectPool CirclePool;

    // circle parameters
    public Sprite EmptyCircleSprite;
    public Color EmptyCircleSpriteColor;
    public Color EmptyCircleColor;

    public int CircleCount = 6;
    public float Radius = 125;

    public CircleSet EnabledCircles;
    public Turn TurnSource;

    float _angleIncrement = 0f;


    // Use this for initialization
    void Start () {
        _angleIncrement = 360f / CircleCount;

        SpawnEmptyCircles();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnEmptyCircles()
    {
        for (int i = 0; i < CircleCount; i++)
        {
            var newCircle = CirclePool.GetObject();
            float degrees = _angleIncrement * i;

            var x = Mathf.Sin(Mathf.Deg2Rad * degrees) * Radius;
            var y = Mathf.Cos(Mathf.Deg2Rad * degrees) * Radius;

            newCircle.transform.localPosition = new Vector2(x, y);

            var item = newCircle.GetComponent<CircleItem>();
            item.SetIndex(i);
            item.SetEmpty(EmptyCircleSprite, EmptyCircleSpriteColor, EmptyCircleSpriteColor);
        }
    }

    public void RotateCircles(int turns)
    {
        transform.Rotate(new Vector3(0f, 0f, _angleIncrement * turns));
    }

    public void ApplyCircleUpdate(CircleUpdate update)
    {
        if (update.CircleIndex < 0 || update.CircleIndex > EnabledCircles.Items.Count)
            return;

        CircleItem item = EnabledCircles.Items[update.CircleIndex];
        item.UpdateCircle(update);
    }

    public void ConsumeTurn()
    {
        var update = TurnSource.CirclesUpdate;

        if (update.CircleRotation != null)
            RotateCircles((int)update.CircleRotation);
        if (update.CircleChanges != null)
        {
            for (int i = 0; i < update.CircleChanges.Length; i++)
            {
                ApplyCircleUpdate(update.CircleChanges[i]);
            }
        }
            
        
    }
}
