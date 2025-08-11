using System.Collections.Generic;                    // using directive for collections           // koleksiyonlar için using bildirimi
using UnityEngine;                                   // base Unity engine namespace                 // Unity temel motor isim alaný
using UnityEngine.SceneManagement;                   // to manage scenes                            // sahne yönetimi için
using TMPro;                                         // for TextMeshPro UI elements                  // TextMeshPro UI öðeleri için

public class ShowCardsManager : MonoBehaviour       // main class declaration                       // ana sýnýf bildirimi
{
    public Transform contentPanel;                    // ScrollView content panel                     // ScrollView içerik paneli
    public GameObject playerCardPrefab;               // prefab for player cards                      // oyuncu kartý prefabý
    public GameObject captainCardPrefab;              // prefab for captain cards                     // kaptan kartý prefabý
    public GameObject weatherCardPrefab;              // prefab for weather cards                     // hava durumu kartý prefabý

    public TMP_Text captainCountText;                  // UI text to display captain count            // kaptan kart sayýsý göstergesi
    public TMP_Text weatherCountText;                  // UI text to display weather count            // hava durumu kart sayýsý göstergesi
    public TMP_Text forwardCountText;                  // UI text to display forward count            // forvet kart sayýsý göstergesi
    public TMP_Text midfieldCountText;                 // UI text to display midfield count           // orta saha kart sayýsý göstergesi
    public TMP_Text defansCountText;                   // UI text to display defense count            // defans kart sayýsý göstergesi

    void Start()                                     // called once when object is initialized       // obje baþlatýldýðýnda çaðrýlýr
    {
        string currentScene = SceneManager.GetActiveScene().name;  // get current active scene name     // aktif sahnenin adýný al

        if (currentScene == "MyCardsScene")          // if scene is MyCardsScene                    // eðer sahne MyCardsScene ise
        {
            ShowCards(SplashSceneManager.playerCards.GetOwnedCards());  // show owned cards            // sahip olunan kartlarý göster
        }
        else if (currentScene == "AllCardsScene")    // if scene is AllCardsScene                    // eðer sahne AllCardsScene ise
        {
            ShowCards(CardDatabase.cardList);        // show all cards from database                 // veritabanýndaki tüm kartlarý göster
        }

        ShowDeckSummary();                            // show the counts summary                       // kart sayýsý özetini göster
    }

    void ShowCards(List<Card> cards)                 // show cards on the content panel              // kartlarý içerik panelinde göster
    {
        foreach (Transform child in contentPanel)    // clear previous cards in content panel        // içerik panelindeki eski kartlarý temizle
            Destroy(child.gameObject);                // destroy each child gameobject                 // her bir çocuðu yok et

        cards.Sort((a, b) => b.cardID.CompareTo(a.cardID));  // sort cards descending by cardID         // kartlarý cardID'ye göre büyükten küçüðe sýrala

        foreach (Card card in cards)                   // instantiate each card prefab                 // her kart prefabýný oluþtur
        {
            GameObject prefabToUse = GetPrefabByCardType(card.cardType);  // get prefab based on card type  // kart tipine göre prefab al
            if (prefabToUse == null) continue;         // skip if prefab null                         // prefab yoksa atla

            GameObject newCard = Instantiate(prefabToUse, contentPanel, false); // instantiate card prefab    // kart prefabýný içerik paneline ekle
            var infoCard = newCard.GetComponent<InfoCardDisplay>();  // get InfoCardDisplay component     // InfoCardDisplay bileþenini al
            if (infoCard != null)                        // if component exists                        // bileþen varsa
            {
                infoCard.SetCardInfo(card, card.cardName);  // set card info to display                // kart bilgilerini ekranda göster
            }
        }
    }

    void ShowDeckSummary()                            // calculate and display card type counts       // kart türü sayýsýný hesapla ve göster
    {
        string currentScene = SceneManager.GetActiveScene().name;   // get current active scene name    // aktif sahnenin adýný al

        List<Card> cards = currentScene switch            // select card list by scene                    // sahneye göre kart listesini seç
        {
            "MyCardsScene" => SplashSceneManager.playerCards.GetOwnedCards(),  // owned cards          // sahip olunan kartlar
            "AllCardsScene" => CardDatabase.cardList,     // all cards                                     // tüm kartlar
            _ => new List<Card>()                          // empty list for others                          // diðerleri için boþ liste
        };

        int captainCount = 0;                             // count for captain cards                        // kaptan kart sayacý
        int weatherCount = 0;                             // count for weather cards                        // hava durumu kart sayacý

        int defansCount = 0;                              // count for defense player cards                 // defans kart sayacý
        int midfieldCount = 0;                            // count for midfield player cards                // orta saha kart sayacý
        int forwardCount = 0;                             // count for forward player cards                  // forvet kart sayacý

        foreach (var card in cards)                        // iterate through each card                      // her kartta dön
        {
            switch (card.cardType)                         // check card type                                // kart tipine bak
            {
                case CardType.Player:                       // if player card                                // oyuncu kartý ise
                    switch (card.position)                 // check player position                          // oyuncu pozisyonuna göre
                    {
                        case 0: defansCount++; break;       // defans                                      // defans ise sayacý artýr
                        case 1: midfieldCount++; break;     // orta saha                                    // orta saha ise sayacý artýr
                        case 2: forwardCount++; break;      // forvet                                       // forvet ise sayacý artýr
                    }
                    break;

                case CardType.Captain:                       // if captain card                              // kaptan kartý ise
                    captainCount++;                          // increment captain count                      // kaptan sayacýný artýr
                    break;

                case CardType.Weather:                       // if weather card                              // hava durumu kartý ise
                    weatherCount++;                          // increment weather count                      // hava durumu sayacýný artýr
                    break;
            }
        }

        if (captainCountText != null)                      // if UI element exists                          // UI elemaný varsa
            captainCountText.text = captainCount.ToString(); // display captain count                      // kaptan sayýsýný göster
        if (weatherCountText != null)                      // if UI element exists                          // UI elemaný varsa
            weatherCountText.text = weatherCount.ToString(); // display weather count                      // hava durumu sayýsýný göster
        if (forwardCountText != null)                       // if UI element exists                          // UI elemaný varsa
            forwardCountText.text = forwardCount.ToString(); // display forward count                      // forvet sayýsýný göster
        if (midfieldCountText != null)                      // if UI element exists                          // UI elemaný varsa
            midfieldCountText.text = midfieldCount.ToString(); // display midfield count                    // orta saha sayýsýný göster
        if (defansCountText != null)                         // if UI element exists                          // UI elemaný varsa
            defansCountText.text = defansCount.ToString(); // display defense count                       // defans sayýsýný göster
    }

    GameObject GetPrefabByCardType(CardType type)        // returns prefab based on card type             // kart tipine göre prefab döndürür
    {
        return type switch
        {
            CardType.Player => playerCardPrefab,           // player prefab                                // oyuncu prefabý
            CardType.Captain => captainCardPrefab,         // captain prefab                               // kaptan prefabý
            CardType.Weather => weatherCardPrefab,         // weather prefab                               // hava durumu prefabý
            _ => null,                                     // default null                                 // varsayýlan null
        };
    }

    public void OnBackButtonPressed()                      // called on back button click                    // geri butonuna basýldýðýnda çaðrýlýr
    {
        SceneManager.LoadScene("MenuScene");                // load main menu scene                           // menü sahnesini yükle
    }
}
