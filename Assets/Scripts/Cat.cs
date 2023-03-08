using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat
{
    public Sprite imageSprite;
    public Sprite drawnSprite;
    public string catName;
    public string rarity;
    public GameObject model;


    public Cat()
    {
        catName = "Eggnog";
    }

    public Cat(Sprite image, Sprite drawn, string newName, string rarity, GameObject model)
    {
        imageSprite = image;
        drawnSprite = drawn;
        catName = newName;
        this.rarity = rarity;
        this.model = model;
    }
}
