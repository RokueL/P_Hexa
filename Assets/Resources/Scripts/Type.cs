using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    Diamond,
    Ruby,
    Emerald
}

[System.Serializable]
public struct TypeSprite
{
    public Type type;
    public Sprite sprite;
}
