using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public enum GameTurn
{
    Player,  // [en] Player's turn / [tr] Oyuncu s�ras�
    AI       // [en] AI's turn / [tr] Yapay zek� s�ras�
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;   // [en] Singleton instance                / [tr] Tekil �rnek
    [Header("References")]
    public PlayerHand playerHand;         // [en] Player hand manager reference     / [tr] Oyuncu eli y�neticisi referans�
    public AIHand aiHand;                 // [en] AI hand manager reference         / [tr] AI eli y�neticisi referans�
    [Header("Turn Settings")]
    public GameTurn currentTurn;          // [en] Current turn                      / [tr] Mevcut s�ra
    public float aiThinkTime = 2f;        // [en] Delay time for AI to play         / [tr] AI'n�n hamle s�resi
    [Header("Player Areas")]
    public List<Card> playerDefenseCards = new List<Card>();    // [en] Player's defense cards  / [tr] Oyuncu defans kartlar�
    public List<Card> playerMidfieldCards = new List<Card>();   // [en] Player's midfield cards / [tr] Oyuncu orta saha kartlar�
    public List<Card> playerForwardCards = new List<Card>();    // [en] Player's forward cards  / [tr] Oyuncu forvet kartlar�
    public List<Card> playerCaptainCards = new List<Card>();    // [en] Player's captain cards  / [tr] Oyuncu kaptan kartlar�
    public List<Card> playerWeatherCards = new List<Card>();    // [en] Player's weather cards  / [tr] Oyuncu hava kartlar�
    [Header("AI Areas")]
    public List<Card> aiDefenseCards = new List<Card>();        // [en] AI's defense cards      / [tr] AI defans kartlar�
    public List<Card> aiMidfieldCards = new List<Card>();       // [en] AI's midfield cards     / [tr] AI orta saha kartlar�
    public List<Card> aiForwardCards = new List<Card>();        // [en] AI's forward cards      / [tr] AI forvet kartlar�
    public List<Card> aiCaptainCards = new List<Card>();        // [en] AI's captain cards      / [tr] AI kaptan kartlar�
    public List<Card> aiWeatherCards = new List<Card>();        // [en] AI's weather cards      / [tr] AI hava kartlar�
    public List<Card> activeWeatherCards = new List<Card>();    // [en] Currently active weather cards / [tr] Aktif hava kartlar�
    private void Awake()
    {
        if (Instance == null) Instance = this;                  // [en] Assign singleton instance / [tr] Tekil �rnek ata
        else Destroy(gameObject);                               // [en] Destroy duplicates        / [tr] �o�alt�lm�� objeyi sil
    }
    private void Start()
    {
        AIUIManager.Instance.SetupAIUI();         // [en] Initialize AI UI         / [tr] AI UI'yi ayarla
        DeckManager.Instance.GenerateAIDeck();    // [en] Create AI deck           / [tr] AI destesini olu�tur
        aiHand.InitAIHand();                      // [en] Initialize AI hand       / [tr] AI elini ba�lat
        currentTurn = GameTurn.Player;            // [en] Set first turn to player / [tr] �lk s�ray� oyuncuya ata
    }
    public void SetFirstTurn(GameTurn turn)
    {
        currentTurn = turn;    // [en] Set the turn     / [tr] S�ray� ayarla
        StartTurn();           // [en] Start the turn   / [tr] S�ra ba�lat
    }
    public void StartTurn()
    {
        if (currentTurn == GameTurn.Player) Debug.Log("Player turn ba�lad�.");  // [en] Player turn started / [tr] Oyuncu s�ras� ba�lad�
        else
        {
            Debug.Log("AI turn ba�lad�.");  // [en] AI turn started        / [tr] AI s�ras� ba�lad�
            StartCoroutine(AITurnDelay());  // [en] Delay before AI plays  / [tr] AI hamlesi i�in gecikme
        }
    }
    public void EndTurn()
    {
        currentTurn = currentTurn == GameTurn.Player ? GameTurn.AI : GameTurn.Player;  // [en] Switch turn / [tr] S�ray� de�i�tir
        StartTurn();  // [en] Start next turn / [tr] S�ray� ba�lat
    }
    private IEnumerator AITurnDelay()
    {
        yield return new WaitForSeconds(aiThinkTime);  // [en] Wait AI think time  / [tr] AI d���nme s�resi bekle
        aiHand.PlayTurn();                             // [en] Execute AI turn     / [tr] AI hamlesini uygula
    }
    public void OnPlayerCardPlayed(Card card)
    {
        if (card.cardType == CardType.Weather && !playerWeatherCards.Contains(card)) playerWeatherCards.Add(card);         // [en] Add weather card if not present / [tr] Hava kart� yoksa ekle
        else if (card.cardType == CardType.Captain && !playerCaptainCards.Contains(card)) playerCaptainCards.Add(card);    // [en] Add captain card if not present / [tr] Kaptan kart� yoksa ekle
        ApplyEffects();                                                                                                    // [en] Apply effects after playing     / [tr] Kart oynand�ktan sonra efektleri uygula
        CardPlayHandler.Instance.UpdatePowerTexts();                                                                       // [en] Update UI power texts           / [tr] G�� metinlerini g�ncelle
    }
    public void OnAICardPlayed(Card card)
    {
        if (card.cardType == CardType.Weather && !aiWeatherCards.Contains(card)) aiWeatherCards.Add(card);                 // [en] Add AI weather card / [tr] AI hava kart�n� ekle
        else if (card.cardType == CardType.Captain && !aiCaptainCards.Contains(card)) aiCaptainCards.Add(card);            // [en] Add AI captain card / [tr] AI kaptan kart�n� ekle
        ApplyEffects();                                                                                                    // [en] Apply effects       / [tr] Efektleri uygula
        AIPlayHandler.Instance.UpdatePowerTexts();                                                                         // [en] Update AI UI        / [tr] AI UI'yi g�ncelle
    }
    private void ApplyEffects()
    {
        CardPlayHandler.Instance.UpdateAllPlayerCardPowers();          // [en] Update all player cards' powers / [tr] T�m oyuncu kartlar�n�n g�c�n� g�ncelle
        AIPlayHandler.Instance.UpdateAllCardPowers();                  // [en] Update all AI cards' powers     / [tr] T�m AI kartlar�n�n g�c�n� g�ncelle
    }
}
