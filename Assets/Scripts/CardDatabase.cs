using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static List<Card> cardList = new List<Card>();

    void Awake()
    {
        // 10 adet futbolcu kartý örnek olarak eklendi
        cardList.Add(new Card(1, "Domaldo", 10, "Efsane forvet", 2, 0, 0, "Cards/domaldo"));
        cardList.Add(new Card(2, "Mesci.Y", 10, "The GOAT", 2, 0, 0, "Cards/mesciy"));
        cardList.Add(new Card(3, "Pandik", 8, "Duvar gibi defans", 0, 0, 0, "Cards/pandik"));
        cardList.Add(new Card(4, "Biruni", 9, "....", 1, 0, 0, "Cards/biruni"));
        cardList.Add(new Card(5, "Djbala", 8, "Hýz canavarý", 2, 0, 0, "Cards/djbala"));
        cardList.Add(new Card(6, "Pedroso", 7, "Genç yetenek", 1, 0, 0, "Cards/pedroso"));
        cardList.Add(new Card(7, "Kralex", 8, "Pas ustasý", 1, 0, 0, "Cards/kralex"));
        cardList.Add(new Card(8, "Neyvar", 10, "Çalým ustasý", 2, 0, 0, "Cards/neyvar"));
        cardList.Add(new Card(9, "Reyis", 7, "Kendini satar takýmý asla", 2, 0, 0, "Cards/reyis"));
        cardList.Add(new Card(10, "S.Vamos", 9, "Kaptan kýrmýzý kart", 0, 0, 0, "Cards/svamos"));
        cardList.Add(new Card(11, "Embapo", 9, "....", 2, 0, 0, "Cards/embapo"));
        cardList.Add(new Card(12, "Lewandoesk", 9, "Güçlü ve etkili forvet, mükemmel golcü.", 2, 0, 0, "Cards/lewandoesk"));
        cardList.Add(new Card(13, "Decenrice", 8, "Dayanýklý ve pas isabeti yüksek orta saha oyuncusu.", 1, 0, 0, "Cards/decenrice"));
        cardList.Add(new Card(14, "Minato", 6, "Fiziksel gücü yüksek, hava toplarýnda etkili defans oyuncusu.", 0, 0, 0, "Cards/minato"));
        cardList.Add(new Card(15, "Lingaldo", 2, "Zaman zaman sahneye çýkan orta saha oyuncusu.", 1, 0, 0, "Cards/lingaldo"));
        cardList.Add(new Card(16, "Calhanoro", 7, "Uzaktan þutlarý ve duran toplarýyla etkili oyun kurucu.", 1, 0, 0, "Cards/calhanoro"));
        cardList.Add(new Card(17, "Kadiolu", 7, "Çift yönlü oynayabilen dinamik bek oyuncusu.", 0, 0, 0, "Cards/kadiolu"));
        cardList.Add(new Card(18, "Arraske", 7, "Tekniðiyle öne çýkan yaratýcý orta saha oyuncusu.", 1, 0, 0, "Cards/arraske"));
        cardList.Add(new Card(19, "Rafasel", 6, "Hýzý ve çevikliðiyle fark yaratan ofansif orta saha.", 1, 0, 0, "Cards/rafasel"));
        cardList.Add(new Card(20, "Sanvicho", 6, "Fizik gücü yüksek, müdahaleleri sert bir stoper.", 0, 0, 0, "Cards/sanvicho"));

        cardList.Add(new Card(120, "Hava", 0, "..", 4, 0, 0, "Cards/hava"));
        cardList.Add(new Card(121, "Kupa", 0, "..", 3, 0, 0, "Cards/cup"));
    }

    public static Card GetCardByID(int id)
    {
        return cardList.Find(card => card.cardID == id);
    }
}
