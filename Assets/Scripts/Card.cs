using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
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
    public bool isPlayed = false;

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
    public Card Clone()
    {
        return new Card(cardID, cardName, cardPower, cardDescription, position, weather, extra, imagePath);
    }

}
