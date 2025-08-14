using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public enum GameTurn
{
    Player,  // [en] Player's turn / [tr] Oyuncu sýrasý
    AI       // [en] AI's turn / [tr] Yapay zekâ sýrasý
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;   // [en] Singleton instance                / [tr] Tekil örnek
    [Header("References")]
    public PlayerHand playerHand;         // [en] Player hand manager reference     / [tr] Oyuncu eli yöneticisi referansý
    public AIHand aiHand;                 // [en] AI hand manager reference         / [tr] AI eli yöneticisi referansý
    [Header("Turn Settings")]
    public GameTurn currentTurn;          // [en] Current turn                      / [tr] Mevcut sýra
    public float aiThinkTime = 2f;        // [en] Delay time for AI to play         / [tr] AI'nýn hamle süresi
    [Header("Player Areas")]
    public List<Card> playerDefenseCards = new List<Card>();    // [en] Player's defense cards  / [tr] Oyuncu defans kartlarý
    public List<Card> playerMidfieldCards = new List<Card>();   // [en] Player's midfield cards / [tr] Oyuncu orta saha kartlarý
    public List<Card> playerForwardCards = new List<Card>();    // [en] Player's forward cards  / [tr] Oyuncu forvet kartlarý
    public List<Card> playerCaptainCards = new List<Card>();    // [en] Player's captain cards  / [tr] Oyuncu kaptan kartlarý
    public List<Card> playerWeatherCards = new List<Card>();    // [en] Player's weather cards  / [tr] Oyuncu hava kartlarý
    [Header("AI Areas")]
    public List<Card> aiDefenseCards = new List<Card>();        // [en] AI's defense cards      / [tr] AI defans kartlarý
    public List<Card> aiMidfieldCards = new List<Card>();       // [en] AI's midfield cards     / [tr] AI orta saha kartlarý
    public List<Card> aiForwardCards = new List<Card>();        // [en] AI's forward cards      / [tr] AI forvet kartlarý
    public List<Card> aiCaptainCards = new List<Card>();        // [en] AI's captain cards      / [tr] AI kaptan kartlarý
    public List<Card> aiWeatherCards = new List<Card>();        // [en] AI's weather cards      / [tr] AI hava kartlarý
    public List<Card> activeWeatherCards = new List<Card>();    // [en] Currently active weather cards / [tr] Aktif hava kartlarý
    private void Awake()
    {
        if (Instance == null) Instance = this;                  // [en] Assign singleton instance / [tr] Tekil örnek ata
        else Destroy(gameObject);                               // [en] Destroy duplicates        / [tr] Çoðaltýlmýþ objeyi sil
    }
    private void Start()
    {
        AIUIManager.Instance.SetupAIUI();         // [en] Initialize AI UI         / [tr] AI UI'yi ayarla
        DeckManager.Instance.GenerateAIDeck();    // [en] Create AI deck           / [tr] AI destesini oluþtur
        aiHand.InitAIHand();                      // [en] Initialize AI hand       / [tr] AI elini baþlat
        currentTurn = GameTurn.Player;            // [en] Set first turn to player / [tr] Ýlk sýrayý oyuncuya ata
    }
    public void SetFirstTurn(GameTurn turn)
    {
        currentTurn = turn;    // [en] Set the turn     / [tr] Sýrayý ayarla
        StartTurn();           // [en] Start the turn   / [tr] Sýra baþlat
    }
    public void StartTurn()
    {
        if (currentTurn == GameTurn.Player) Debug.Log("Player turn baþladý.");  // [en] Player turn started / [tr] Oyuncu sýrasý baþladý
        else
        {
            Debug.Log("AI turn baþladý.");  // [en] AI turn started        / [tr] AI sýrasý baþladý
            StartCoroutine(AITurnDelay());  // [en] Delay before AI plays  / [tr] AI hamlesi için gecikme
        }
    }
    public void EndTurn()
    {
        currentTurn = currentTurn == GameTurn.Player ? GameTurn.AI : GameTurn.Player;  // [en] Switch turn / [tr] Sýrayý deðiþtir
        StartTurn();  // [en] Start next turn / [tr] Sýrayý baþlat
    }
    private IEnumerator AITurnDelay()
    {
        yield return new WaitForSeconds(aiThinkTime);  // [en] Wait AI think time  / [tr] AI düþünme süresi bekle
        aiHand.PlayTurn();                             // [en] Execute AI turn     / [tr] AI hamlesini uygula
    }
    public void OnPlayerCardPlayed(Card card)
    {
        if (card.cardType == CardType.Weather && !playerWeatherCards.Contains(card)) playerWeatherCards.Add(card);         // [en] Add weather card if not present / [tr] Hava kartý yoksa ekle
        else if (card.cardType == CardType.Captain && !playerCaptainCards.Contains(card)) playerCaptainCards.Add(card);    // [en] Add captain card if not present / [tr] Kaptan kartý yoksa ekle
        ApplyEffects();                                                                                                    // [en] Apply effects after playing     / [tr] Kart oynandýktan sonra efektleri uygula
        CardPlayHandler.Instance.UpdatePowerTexts();                                                                       // [en] Update UI power texts           / [tr] Güç metinlerini güncelle
    }
    public void OnAICardPlayed(Card card)
    {
        if (card.cardType == CardType.Weather && !aiWeatherCards.Contains(card)) aiWeatherCards.Add(card);                 // [en] Add AI weather card / [tr] AI hava kartýný ekle
        else if (card.cardType == CardType.Captain && !aiCaptainCards.Contains(card)) aiCaptainCards.Add(card);            // [en] Add AI captain card / [tr] AI kaptan kartýný ekle
        ApplyEffects();                                                                                                    // [en] Apply effects       / [tr] Efektleri uygula
        AIPlayHandler.Instance.UpdatePowerTexts();                                                                         // [en] Update AI UI        / [tr] AI UI'yi güncelle
    }
    private void ApplyEffects()
    {
        CardPlayHandler.Instance.UpdateAllPlayerCardPowers();          // [en] Update all player cards' powers / [tr] Tüm oyuncu kartlarýnýn gücünü güncelle
        AIPlayHandler.Instance.UpdateAllCardPowers();                  // [en] Update all AI cards' powers     / [tr] Tüm AI kartlarýnýn gücünü güncelle
    }
}
