using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat
{
    public Sprite imageSprite;
    public Sprite drawnSprite;
    public string catName;


    public Cat()
    {
        catName = "Eggnog";
    }

    public Cat(Sprite image, Sprite drawn, string newName)
    {
        imageSprite = image;
        drawnSprite = drawn;
        catName = newName;

    }
}
