using System.Collections.Generic;                    // using directive for collections           // koleksiyonlar i�in using bildirimi
using UnityEngine;                                   // base Unity engine namespace                 // Unity temel motor isim alan�
using UnityEngine.SceneManagement;                   // to manage scenes                            // sahne y�netimi i�in
using TMPro;                                         // for TextMeshPro UI elements                  // TextMeshPro UI ��eleri i�in

public class ShowCardsManager : MonoBehaviour       // main class declaration                       // ana s�n�f bildirimi
{
    public Transform contentPanel;                    // ScrollView content panel                     // ScrollView i�erik paneli
    public GameObject playerCardPrefab;               // prefab for player cards                      // oyuncu kart� prefab�
    public GameObject captainCardPrefab;              // prefab for captain cards                     // kaptan kart� prefab�
    public GameObject weatherCardPrefab;              // prefab for weather cards                     // hava durumu kart� prefab�

    public TMP_Text captainCountText;                  // UI text to display captain count            // kaptan kart say�s� g�stergesi
    public TMP_Text weatherCountText;                  // UI text to display weather count            // hava durumu kart say�s� g�stergesi
    public TMP_Text forwardCountText;                  // UI text to display forward count            // forvet kart say�s� g�stergesi
    public TMP_Text midfieldCountText;                 // UI text to display midfield count           // orta saha kart say�s� g�stergesi
    public TMP_Text defansCountText;                   // UI text to display defense count            // defans kart say�s� g�stergesi

    void Start()                                     // called once when object is initialized       // obje ba�lat�ld���nda �a�r�l�r
    {
        string currentScene = SceneManager.GetActiveScene().name;  // get current active scene name     // aktif sahnenin ad�n� al

        if (currentScene == "MyCardsScene")          // if scene is MyCardsScene                    // e�er sahne MyCardsScene ise
        {
            ShowCards(SplashSceneManager.playerCards.GetOwnedCards());  // show owned cards            // sahip olunan kartlar� g�ster
        }
        else if (currentScene == "AllCardsScene")    // if scene is AllCardsScene                    // e�er sahne AllCardsScene ise
        {
            ShowCards(CardDatabase.cardList);        // show all cards from database                 // veritaban�ndaki t�m kartlar� g�ster
        }

        ShowDeckSummary();                            // show the counts summary                       // kart say�s� �zetini g�ster
    }

    void ShowCards(List<Card> cards)                 // show cards on the content panel              // kartlar� i�erik panelinde g�ster
    {
        foreach (Transform child in contentPanel)    // clear previous cards in content panel        // i�erik panelindeki eski kartlar� temizle
            Destroy(child.gameObject);                // destroy each child gameobject                 // her bir �ocu�u yok et

        cards.Sort((a, b) => b.cardID.CompareTo(a.cardID));  // sort cards descending by cardID         // kartlar� cardID'ye g�re b�y�kten k����e s�rala

        foreach (Card card in cards)                   // instantiate each card prefab                 // her kart prefab�n� olu�tur
        {
            GameObject prefabToUse = GetPrefabByCardType(card.cardType);  // get prefab based on card type  // kart tipine g�re prefab al
            if (prefabToUse == null) continue;         // skip if prefab null                         // prefab yoksa atla

            GameObject newCard = Instantiate(prefabToUse, contentPanel, false); // instantiate card prefab    // kart prefab�n� i�erik paneline ekle
            var infoCard = newCard.GetComponent<InfoCardDisplay>();  // get InfoCardDisplay component     // InfoCardDisplay bile�enini al
            if (infoCard != null)                        // if component exists                        // bile�en varsa
            {
                infoCard.SetCardInfo(card, card.cardName);  // set card info to display                // kart bilgilerini ekranda g�ster
            }
        }
    }

    void ShowDeckSummary()                            // calculate and display card type counts       // kart t�r� say�s�n� hesapla ve g�ster
    {
        string currentScene = SceneManager.GetActiveScene().name;   // get current active scene name    // aktif sahnenin ad�n� al

        List<Card> cards = currentScene switch            // select card list by scene                    // sahneye g�re kart listesini se�
        {
            "MyCardsScene" => SplashSceneManager.playerCards.GetOwnedCards(),  // owned cards          // sahip olunan kartlar
            "AllCardsScene" => CardDatabase.cardList,     // all cards                                     // t�m kartlar
            _ => new List<Card>()                          // empty list for others                          // di�erleri i�in bo� liste
        };

        int captainCount = 0;                             // count for captain cards                        // kaptan kart sayac�
        int weatherCount = 0;                             // count for weather cards                        // hava durumu kart sayac�

        int defansCount = 0;                              // count for defense player cards                 // defans kart sayac�
        int midfieldCount = 0;                            // count for midfield player cards                // orta saha kart sayac�
        int forwardCount = 0;                             // count for forward player cards                  // forvet kart sayac�

        foreach (var card in cards)                        // iterate through each card                      // her kartta d�n
        {
            switch (card.cardType)                         // check card type                                // kart tipine bak
            {
                case CardType.Player:                       // if player card                                // oyuncu kart� ise
                    switch (card.position)                 // check player position                          // oyuncu pozisyonuna g�re
                    {
                        case 0: defansCount++; break;       // defans                                      // defans ise sayac� art�r
                        case 1: midfieldCount++; break;     // orta saha                                    // orta saha ise sayac� art�r
                        case 2: forwardCount++; break;      // forvet                                       // forvet ise sayac� art�r
                    }
                    break;

                case CardType.Captain:                       // if captain card                              // kaptan kart� ise
                    captainCount++;                          // increment captain count                      // kaptan sayac�n� art�r
                    break;

                case CardType.Weather:                       // if weather card                              // hava durumu kart� ise
                    weatherCount++;                          // increment weather count                      // hava durumu sayac�n� art�r
                    break;
            }
        }

        if (captainCountText != null)                      // if UI element exists                          // UI eleman� varsa
            captainCountText.text = captainCount.ToString(); // display captain count                      // kaptan say�s�n� g�ster
        if (weatherCountText != null)                      // if UI element exists                          // UI eleman� varsa
            weatherCountText.text = weatherCount.ToString(); // display weather count                      // hava durumu say�s�n� g�ster
        if (forwardCountText != null)                       // if UI element exists                          // UI eleman� varsa
            forwardCountText.text = forwardCount.ToString(); // display forward count                      // forvet say�s�n� g�ster
        if (midfieldCountText != null)                      // if UI element exists                          // UI eleman� varsa
            midfieldCountText.text = midfieldCount.ToString(); // display midfield count                    // orta saha say�s�n� g�ster
        if (defansCountText != null)                         // if UI element exists                          // UI eleman� varsa
            defansCountText.text = defansCount.ToString(); // display defense count                       // defans say�s�n� g�ster
    }

    GameObject GetPrefabByCardType(CardType type)        // returns prefab based on card type             // kart tipine g�re prefab d�nd�r�r
    {
        return type switch
        {
            CardType.Player => playerCardPrefab,           // player prefab                                // oyuncu prefab�
            CardType.Captain => captainCardPrefab,         // captain prefab                               // kaptan prefab�
            CardType.Weather => weatherCardPrefab,         // weather prefab                               // hava durumu prefab�
            _ => null,                                     // default null                                 // varsay�lan null
        };
    }

    public void OnBackButtonPressed()                      // called on back button click                    // geri butonuna bas�ld���nda �a�r�l�r
    {
        SceneManager.LoadScene("MenuScene");                // load main menu scene                           // men� sahnesini y�kle
    }
}
