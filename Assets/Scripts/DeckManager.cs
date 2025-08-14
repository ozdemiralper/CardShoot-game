using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance;

    [Header("Player & AI Decks")]
    // Oyuncunun oyun sahnesinde kullanacaðý seçili kartlar
    public List<Card> selectedGameDeck = new List<Card>();

    // AI için oluþturulan gizli deste
    public List<Card> aiDeck = new List<Card>();

    [Header("Card Pool")]
    // Tüm kart havuzu (Resources klasöründen ya da baþka bir yerden yüklenebilir)
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

    // Oyun sahnesi açýldýðýnda AI destesini oluþtur
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
            Debug.LogError("AI için seçilebilecek kart yok!");
            return;
        }

        // Rastgele 10 kart seçiyoruz
        List<Card> tempList = new List<Card>(possibleCards);
        for (int i = 0; i < 10; i++)
        {
            if (tempList.Count == 0) break; // Yeterli kart kalmazsa dur

            int index = Random.Range(0, tempList.Count);
            aiDeck.Add(tempList[index]);
            tempList.RemoveAt(index);
        }

        Debug.Log("AI destesi oluþturuldu. Kart sayýsý: " + aiDeck.Count);
    }

}
