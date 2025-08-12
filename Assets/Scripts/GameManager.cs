using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Oyuncunun oyun sahnesinde kullanacaðý seçili kartlar
    public List<Card> selectedGameDeck = new List<Card>();

    void Awake()
    {
        // Eðer zaten bir GameManager varsa yok et, yoksa bu objeyi kalýcý yap
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
