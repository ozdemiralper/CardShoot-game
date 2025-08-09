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
            cardList.Add(new Card(CardType.Player, 2, "Mesci.Y", 10, "T�m zamanlar�n en iyi futbolcusu", 2, 0, 0, "Cards/mesciy"));
            cardList.Add(new Card(CardType.Player, 3, "Pandik", 8, "Sa�lam ve g�venilir defans oyuncusu", 0, 0, 0, "Cards/pandik"));
            cardList.Add(new Card(CardType.Player, 4, "Biruni", 9, "�ok y�nl� ve etkili orta saha", 1, 0, 0, "Cards/biruni"));
            cardList.Add(new Card(CardType.Player, 5, "Djbala", 8, "H�zl� ve �evik forvet oyuncusu", 2, 0, 0, "Cards/djbala"));
            cardList.Add(new Card(CardType.Player, 6, "Pedroso", 7, "Gen� ve yetenekli orta saha", 1, 0, 0, "Cards/pedroso"));
            cardList.Add(new Card(CardType.Player, 7, "Kralex", 8, "Paslar� isabetli ve oyun kurucu", 1, 0, 0, "Cards/kralex"));
            cardList.Add(new Card(CardType.Player, 8, "Neyvar", 10, "�al�m ustas�, �st d�zey forvet", 2, 0, 0, "Cards/neyvar"));
            cardList.Add(new Card(CardType.Player, 9, "Reyis", 7, "M�cadeleci ve tak�m�na ba�l�", 2, 0, 0, "Cards/reyis"));
            cardList.Add(new Card(CardType.Player, 10, "S.Vamos", 9, "Agresif kaptan, lider ruhu", 0, 0, 0, "Cards/svamos"));
            cardList.Add(new Card(CardType.Player, 11, "Embapo", 9, "H�zl� ve etkili forvet oyuncusu", 2, 0, 0, "Cards/embapo"));
            cardList.Add(new Card(CardType.Player, 12, "Lewandoesk", 9, "G��l� ve golc� forvet", 2, 0, 0, "Cards/lewandoesk"));
            cardList.Add(new Card(CardType.Player, 13, "Decenrice", 8, "Dayan�kl� ve pas isabeti y�ksek", 1, 0, 0, "Cards/decenrice"));
            cardList.Add(new Card(CardType.Player, 14, "Minato", 6, "Hava toplar�nda etkili defans", 0, 0, 0, "Cards/minato"));
            cardList.Add(new Card(CardType.Player, 15, "Lingaldo", 2, "Geli�mekte olan orta saha oyuncusu", 1, 0, 0, "Cards/lingaldo"));
            cardList.Add(new Card(CardType.Player, 16, "Calhanoro", 7, "Uzaktan �utlar� etkili orta saha", 1, 0, 0, "Cards/calhanoro"));
            cardList.Add(new Card(CardType.Player, 17, "Kadiolu", 7, "�ift y�nl� ve dinamik bek", 0, 0, 0, "Cards/kadiolu"));
            cardList.Add(new Card(CardType.Player, 18, "Arraske", 7, "Teknik ve yarat�c� orta saha", 1, 0, 0, "Cards/arraske"));
            cardList.Add(new Card(CardType.Player, 19, "Rafasel", 6, "H�zl� ve �evik ofansif orta saha", 1, 0, 0, "Cards/rafasel"));
            cardList.Add(new Card(CardType.Player, 20, "Sanvicho", 6, "Fizik g�c� y�ksek sert stoper", 0, 0, 0, "Cards/sanvicho"));

            cardList.Add(new Card(CardType.Weather, 200, "Rain", 0, "Ya�mur defans oyuncular�n� zorlar", 0, 0, 0, "Cards/rain"));
            cardList.Add(new Card(CardType.Weather, 201, "Snow", 1, "Kar orta saha oyuncular�na zorluk", 1, 0, 0, "Cards/snow"));
            cardList.Add(new Card(CardType.Weather, 202, "Wind", 2, "R�zgar forvet oyuncular�n� zorlar", 2, 0, 0, "Cards/wind"));

            cardList.Add(new Card(CardType.Captain, 206, "Kaptan", 0, "Mevkideki oyuncular�n g�c�n� X2 yapar", 0, 0, 0, "Cards/defCap1"));
            cardList.Add(new Card(CardType.Captain, 207, "Kaptan", 0, "Mevkideki oyuncular�n g�c�n� X2 yapar", 0, 0, 0, "Cards/defCap1"));
            cardList.Add(new Card(CardType.Captain, 208, "Kaptan", 1, "Mevkideki oyuncular�n g�c�n� X2 yapar", 1, 0, 0, "Cards/midCap1"));
            cardList.Add(new Card(CardType.Captain, 209, "Kaptan", 1, "Mevkideki oyuncular�n g�c�n� X2 yapar", 1, 0, 0, "Cards/midCap2"));
            cardList.Add(new Card(CardType.Captain, 210, "Kaptan", 2, "Mevkideki oyuncular�n g�c�n� X2 yapar", 2, 0, 0, "Cards/forCap1"));
            cardList.Add(new Card(CardType.Captain, 211, "Kaptan", 2, "Mevkideki oyuncular�n g�c�n� X2 yapar", 2, 0, 0, "Cards/forCap2"));

            cardList.Add(new Card(CardType.Coach, 300, "Malinho", 0, "Deneyimli ve bilgili teknik adam", 0, 0, 0, "Cards/coach1"));
            cardList.Add(new Card(CardType.Coach, 301, "Amcalotti", 0, "Strateji uzman� teknik direkt�r", 0, 0, 0, "Cards/coach2"));
            cardList.Add(new Card(CardType.Coach, 302, "Hamsi", 0, "Motivasyonu y�ksek antren�r", 0, 0, 0, "Cards/coach3"));

        }
    }
}