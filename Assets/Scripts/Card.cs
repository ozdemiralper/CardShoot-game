using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
[System.Serializable]

public enum CardType
{
    Player,     // Oyuncu kart�
    Cup,       // Kupa kart�
    Weather,    // Hava durumu kart�   cardPower= 0-3 (0: Normal, 1: Ya�mur, 2: Kar, 3: F�rt�na) 
    Coach,      // Teknik direkt�r
    Trophy,     // Kupa
    Extra       // Ekstra �zel kart
}

public class Card
{
    public CardType cardType;
    public int cardID; // Unique identifier for the card
    public string cardName;
    public int cardPower;
    public string cardDescription;
    public int position;
    public int weather;
    public int extra;
    public string imagePath;
    public bool isPlayed = false;

    // Method to display card information

    public Card(CardType type, int id, string name, int power, string description, int pos, int weatherType, int extraType, string image)
    {
        cardType = type;
        cardID = id;
        cardName = name;
        cardPower = power;
        cardDescription = description;
        position = pos;
        weather = weatherType;
        extra = extraType;
        imagePath = image;
    }
    public Card Clone()
    {
        return new Card(cardType, cardID, cardName, cardPower, cardDescription, position, weather, extra, imagePath);
    }

    public enum CupPosition
    {
        ForwardCup,
        MidfieldCup,
        DefenseCup
    }
}
