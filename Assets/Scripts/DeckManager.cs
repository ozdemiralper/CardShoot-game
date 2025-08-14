using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance;

    [Header("Player & AI Decks")]
    // Oyuncunun oyun sahnesinde kullanaca�� se�ili kartlar
    public List<Card> selectedGameDeck = new List<Card>();

    // AI i�in olu�turulan gizli deste
    public List<Card> aiDeck = new List<Card>();

    [Header("Card Pool")]
    // T�m kart havuzu (Resources klas�r�nden ya da ba�ka bir yerden y�klenebilir)
    public List<Card> allCards = new List<Card>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Oyun sahnesi a��ld���nda AI destesini olu�tur
    public void GenerateAIDeck()
    {
        aiDeck.Clear();

        // CardDatabase'den sadece Player, Captain ve Weather tiplerini al
        List<Card> possibleCards = CardDatabase.cardList.FindAll(c =>
            c.cardType == CardType.Player ||
            c.cardType == CardType.Captain ||
            c.cardType == CardType.Weather
        );

        if (possibleCards.Count == 0)
        {
            Debug.LogError("AI i�in se�ilebilecek kart yok!");
            return;
        }

        // Rastgele 10 kart se�iyoruz
        List<Card> tempList = new List<Card>(possibleCards);
        for (int i = 0; i < 10; i++)
        {
            if (tempList.Count == 0) break; // Yeterli kart kalmazsa dur

            int index = Random.Range(0, tempList.Count);
            aiDeck.Add(tempList[index]);
            tempList.RemoveAt(index);
        }

        Debug.Log("AI destesi olu�turuldu. Kart say�s�: " + aiDeck.Count);
    }

}
