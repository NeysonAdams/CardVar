using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum CardType
{
    TWO,
    THREE,
    FOUR,
    FIVE,
    SIX,
    SEVEN,
    EIGHT,
    NINE,
    TEN,
    JAK,
    QUINE,
    KING,
    ACE
}
[Serializable]
//Model Super class, use for animation
public class Model
{
    public Vector2 position = Vector2.zero;
    public Vector2 scale = Vector2.one;
    public Quaternion rotation = Quaternion.identity;
}
[Serializable]
public class CardModel : Model
{
    public int id;
    public CardType type;
    public bool isBack = true;
}
public class PlayerModel
{
    public string name;
    public int cardCount;
}

