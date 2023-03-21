using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat
{
    public Sprite imageSprite;
    public Sprite drawnSprite;
    public string catName;
    public string rarity;


    public Cat()
    {
        catName = "Eggnog";
    }

    public Cat(Sprite image, Sprite drawn, string newName, string rarity)
    {
        imageSprite = image;
        drawnSprite = drawn;
        catName = newName;
        this.rarity = rarity;
    }

    public Cat(Sprite image, string newName, string rarity)
    {
        imageSprite = image;
        catName = newName;
        this.rarity = rarity;
    }

    public void Print()
    {
        Debug.Log($"Name: {catName}, Rarirty: {rarity}");
    }

}
