using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragonOptionItem : MonoBehaviour {
    public DragonOptionSet EnabledItems;
    public DragonOptionSet DisabledItems;

    public UnityStringEvent DragonSelectedEvent;

    public Dragon Dragon { get; private set; }

    public Text Name;
    public Image Sprite;
    public Text Hp;
    public Text Attack;
    public Text Energy;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Initialize(Dragon dragon)
    {
        Dragon = dragon;

        Name.text = Dragon.name;
        Hp.text = Dragon.health.ToString();
        Attack.text = Dragon.attack.ToString();
        Energy.text = Dragon.energy.ToString();

        string spritePath = "Textures/" + Dragon.spriteFile;

        Sprite.sprite = Resources.Load<Sprite>(spritePath);
    }

    private void OnEnable()
    {
        EnabledItems.Add(this);
        DisabledItems.Remove(this);
    }

    private void OnDisable()
    {
        DisabledItems.Add(this);
        EnabledItems.Remove(this);
    }

    public void RaiseDragonSelectedEvent()
    {
        DragonSelectedEvent.Invoke(Dragon.id);
    }
}
