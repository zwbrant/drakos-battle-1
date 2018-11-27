using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveChildrenOnGameState : MonoBehaviour
{
    public GameState ActiveState;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetActive(GameState state)
    {
        if (state == ActiveState)
            SetChildrenActive(true);
        else
            SetChildrenActive(false);
    }

    private void SetChildrenActive(bool active)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(active);
        }
    }
}
