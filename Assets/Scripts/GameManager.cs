using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Oyuncunun oyun sahnesinde kullanaca�� se�ili kartlar
    public List<Card> selectedGameDeck = new List<Card>();

    void Awake()
    {
        // E�er zaten bir GameManager varsa yok et, yoksa bu objeyi kal�c� yap
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
