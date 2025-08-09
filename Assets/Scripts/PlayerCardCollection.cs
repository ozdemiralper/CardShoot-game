using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class PlayerCardCollection
{
    public List<int> ownedCardIDs = new List<int>(); // Oyuncunun sahip olduðu kart ID'leri

    private static string SavePath => Path.Combine(Application.persistentDataPath, "playerCards.json");

    // Kayýt yükleme
    public static PlayerCardCollection Load()
    {
        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);
            return JsonUtility.FromJson<PlayerCardCollection>(json);
        }
        else
        {
            PlayerCardCollection newCollection = new PlayerCardCollection();
            newCollection.GenerateStarterDeck();
            newCollection.Save();
            return newCollection;
        }
    }

    // Kayýt kaydetme
    public void Save()
    {
        string json = JsonUtility.ToJson(this, true);
        File.WriteAllText(SavePath, json);
    }

    // Ýlk açýlýþta rastgele deste oluþturma
    private void GenerateStarterDeck()
    {
        for (int i = 0; i < 20; i++)
        {
            int randomIndex = Random.Range(0, CardDatabase.cardList.Count);
            int cardID = CardDatabase.cardList[randomIndex].cardID;
            ownedCardIDs.Add(cardID);
        }
    }

    // Yeni kart ekleme
    public void AddCard(int cardID)
    {
        ownedCardIDs.Add(cardID);
        Save();
    }

    // Sahip olunan kart nesnelerini döndürme
    public List<Card> GetOwnedCards()
    {
        List<Card> cards = new List<Card>();
        foreach (int id in ownedCardIDs)
        {
            Card c = CardDatabase.cardList.Find(card => card.cardID == id);
            if (c != null) cards.Add(c);
        }
        return cards;
    }
}
