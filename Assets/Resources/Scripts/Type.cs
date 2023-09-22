using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    Amethyst,
    Ruby,
    Emerald,
    Sapphire,
    Topaz
}

[System.Serializable]
public struct TypeSprite
{
    public Type type;
    public Sprite sprite;
}
