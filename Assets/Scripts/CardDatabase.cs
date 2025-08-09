using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static List<Card> cardList = new List<Card>();

    void Awake()
    {
        Initialize();
    }

    public static void Initialize()
    {
        if (cardList.Count == 0)
        {
            cardList.Add(new Card(CardType.Player, 1, "Domaldo", 10, "Efsanevi forvet, gol makinesi", 2, 0, 0, "Cards/domaldo"));
            cardList.Add(new Card(CardType.Player, 2, "Mesci.Y", 10, "Tüm zamanlarýn en iyi futbolcusu", 2, 0, 0, "Cards/mesciy"));
            cardList.Add(new Card(CardType.Player, 3, "Pandik", 8, "Saðlam ve güvenilir defans oyuncusu", 0, 0, 0, "Cards/pandik"));
            cardList.Add(new Card(CardType.Player, 4, "Biruni", 9, "Çok yönlü ve etkili orta saha", 1, 0, 0, "Cards/biruni"));
            cardList.Add(new Card(CardType.Player, 5, "Djbala", 8, "Hýzlý ve çevik forvet oyuncusu", 2, 0, 0, "Cards/djbala"));
            cardList.Add(new Card(CardType.Player, 6, "Pedroso", 7, "Genç ve yetenekli orta saha", 1, 0, 0, "Cards/pedroso"));
            cardList.Add(new Card(CardType.Player, 7, "Kralex", 8, "Paslarý isabetli ve oyun kurucu", 1, 0, 0, "Cards/kralex"));
            cardList.Add(new Card(CardType.Player, 8, "Neyvar", 10, "Çalým ustasý, üst düzey forvet", 2, 0, 0, "Cards/neyvar"));
            cardList.Add(new Card(CardType.Player, 9, "Reyis", 7, "Mücadeleci ve takýmýna baðlý", 2, 0, 0, "Cards/reyis"));
            cardList.Add(new Card(CardType.Player, 10, "S.Vamos", 9, "Agresif kaptan, lider ruhu", 0, 0, 0, "Cards/svamos"));
            cardList.Add(new Card(CardType.Player, 11, "Embapo", 9, "Hýzlý ve etkili forvet oyuncusu", 2, 0, 0, "Cards/embapo"));
            cardList.Add(new Card(CardType.Player, 12, "Lewandoesk", 9, "Güçlü ve golcü forvet", 2, 0, 0, "Cards/lewandoesk"));
            cardList.Add(new Card(CardType.Player, 13, "Decenrice", 8, "Dayanýklý ve pas isabeti yüksek", 1, 0, 0, "Cards/decenrice"));
            cardList.Add(new Card(CardType.Player, 14, "Minato", 6, "Hava toplarýnda etkili defans", 0, 0, 0, "Cards/minato"));
            cardList.Add(new Card(CardType.Player, 15, "Lingaldo", 2, "Geliþmekte olan orta saha oyuncusu", 1, 0, 0, "Cards/lingaldo"));
            cardList.Add(new Card(CardType.Player, 16, "Calhanoro", 7, "Uzaktan þutlarý etkili orta saha", 1, 0, 0, "Cards/calhanoro"));
            cardList.Add(new Card(CardType.Player, 17, "Kadiolu", 7, "Çift yönlü ve dinamik bek", 0, 0, 0, "Cards/kadiolu"));
            cardList.Add(new Card(CardType.Player, 18, "Arraske", 7, "Teknik ve yaratýcý orta saha", 1, 0, 0, "Cards/arraske"));
            cardList.Add(new Card(CardType.Player, 19, "Rafasel", 6, "Hýzlý ve çevik ofansif orta saha", 1, 0, 0, "Cards/rafasel"));
            cardList.Add(new Card(CardType.Player, 20, "Sanvicho", 6, "Fizik gücü yüksek sert stoper", 0, 0, 0, "Cards/sanvicho"));

            cardList.Add(new Card(CardType.Weather, 200, "Rain", 0, "Yaðmur defans oyuncularýný zorlar", 0, 0, 0, "Cards/rain"));
            cardList.Add(new Card(CardType.Weather, 201, "Snow", 1, "Kar orta saha oyuncularýna zorluk", 1, 0, 0, "Cards/snow"));
            cardList.Add(new Card(CardType.Weather, 202, "Wind", 2, "Rüzgar forvet oyuncularýný zorlar", 2, 0, 0, "Cards/wind"));

            cardList.Add(new Card(CardType.Captain, 206, "Kaptan", 0, "Mevkideki oyuncularýn gücünü X2 yapar", 0, 0, 0, "Cards/defCap1"));
            cardList.Add(new Card(CardType.Captain, 207, "Kaptan", 0, "Mevkideki oyuncularýn gücünü X2 yapar", 0, 0, 0, "Cards/defCap1"));
            cardList.Add(new Card(CardType.Captain, 208, "Kaptan", 1, "Mevkideki oyuncularýn gücünü X2 yapar", 1, 0, 0, "Cards/midCap1"));
            cardList.Add(new Card(CardType.Captain, 209, "Kaptan", 1, "Mevkideki oyuncularýn gücünü X2 yapar", 1, 0, 0, "Cards/midCap2"));
            cardList.Add(new Card(CardType.Captain, 210, "Kaptan", 2, "Mevkideki oyuncularýn gücünü X2 yapar", 2, 0, 0, "Cards/forCap1"));
            cardList.Add(new Card(CardType.Captain, 211, "Kaptan", 2, "Mevkideki oyuncularýn gücünü X2 yapar", 2, 0, 0, "Cards/forCap2"));

            cardList.Add(new Card(CardType.Coach, 300, "Malinho", 0, "Deneyimli ve bilgili teknik adam", 0, 0, 0, "Cards/coach1"));
            cardList.Add(new Card(CardType.Coach, 301, "Amcalotti", 0, "Strateji uzmaný teknik direktör", 0, 0, 0, "Cards/coach2"));
            cardList.Add(new Card(CardType.Coach, 302, "Hamsi", 0, "Motivasyonu yüksek antrenör", 0, 0, 0, "Cards/coach3"));

        }
    }
}