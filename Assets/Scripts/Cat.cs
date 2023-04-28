using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cat
{
    public Sprite imageSprite;
    public Sprite drawnSprite;
    public string catName;
    public string rarity;
    public string imgUrl;
    public int ownedNum;
    public string texture;

    public bool Owned { get { return ownedNum > 0; } }



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

    public Cat(Sprite image, string imgUrl, string texture, string newName, string rarity, int ownedNum)
    {
        imageSprite = image;
        this.imgUrl = imgUrl;
        catName = newName;
        this.rarity = rarity;
        this.ownedNum = ownedNum;
        this.texture = texture;
    }

    public void Print()
    {
        Debug.Log($"Name: {catName}, Rarirty: {rarity}");
    }

}
