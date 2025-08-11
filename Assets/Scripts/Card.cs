using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[System.Serializable]

public enum CardType
{
    Player,     // Oyuncu kartý
    Captain,    // Kaptan kartý           cardPower= 0-3 
    Weather,    // Hava durumu kartý   cardPower= 0-3 (0: Normal, 1: Yaðmur, 2: Kar, 3: Fýrtýna) 
    Coach,      // Teknik direktör 
}

public class Card
{
    public CardType cardType;
    public int cardID; // Unique identifier for the card
    public string cardName;
    public int cardPower;
    public int originalPower;  // Orijinal güç, her zaman kartýn temel gücü
    public string cardDescription;
    public int position;
    public string imagePath;
    public bool isPlayed = false;

    public Card(CardType type, int id, string name, int power, string description, int pos, string image)
    {
        cardType = type;
        cardID = id;
        cardName = name;
        cardPower = power;
        originalPower = power;  // Orijinal güç burada atanýyor
        cardDescription = description;
        position = pos;
        imagePath = image;
    }
    public Card Clone()
    {
        return new Card(cardType, cardID, cardName, cardPower, cardDescription, position, imagePath);
    }
}

