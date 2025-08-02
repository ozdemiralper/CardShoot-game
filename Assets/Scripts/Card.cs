using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Card : MonoBehaviour
{
    public int cardID; // Unique identifier for the card
    public string cardName;
    public int cardPower;
    public string cardDescription;
    public int position;
    public int weather;
    public int extra;
    public string imagePath;

    // Method to display card information

    public Card(int id, string name, int power, string description, int pos, int weatherType, int extraType, string image)
    {
        cardID = id;
        cardName = name;
        cardPower = power;
        cardDescription = description;
        position = pos;
        weather = weatherType;
        extra = extraType;
        imagePath = image;
    }
}
