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
            // [CardType (1 basamak)][CardPower (2 basamak)][Position (1 basamak)][Random (3 basamak)]
            cardList.Add(new Card(CardType.Coach, 3000003, "Hamsi", 0, "Motivasyonu y�ksek antren�r", 0, "Cards/coach3"));
            cardList.Add(new Card(CardType.Coach, 3000002, "Amcalotti", 0, "Strateji uzman� teknik direkt�r", 0, "Cards/coach2"));
            cardList.Add(new Card(CardType.Coach, 3000001, "Malinho", 0, "Deneyimli ve bilgili teknik adam", 0, "Cards/coach1"));

            cardList.Add(new Card(CardType.Weather, 2022003, "Wind", 2, "R�zgar forvet oyuncular�n� zorlar", 2, "Cards/wind"));
            cardList.Add(new Card(CardType.Weather, 2011002, "Snow", 1, "Kar orta saha oyuncular�na zorluk", 1, "Cards/snow"));
            cardList.Add(new Card(CardType.Weather, 2000001, "Rain", 0, "Ya�mur defans oyuncular�n� zorlar", 0, "Cards/rain"));

            cardList.Add(new Card(CardType.Captain, 1022009, "Kaptan", 2, "Mevkideki oyuncular�n g�c�n� X2 yapar", 2, "Cards/forCap2"));
            cardList.Add(new Card(CardType.Captain, 1022008, "Kaptan", 2, "Mevkideki oyuncular�n g�c�n� X2 yapar", 2, "Cards/forCap1"));
            cardList.Add(new Card(CardType.Captain, 1011007, "Kaptan", 1, "Mevkideki oyuncular�n g�c�n� X2 yapar", 1, "Cards/midCap2"));
            cardList.Add(new Card(CardType.Captain, 1011006, "Kaptan", 1, "Mevkideki oyuncular�n g�c�n� X2 yapar", 1, "Cards/midCap1"));
            cardList.Add(new Card(CardType.Captain, 1000005, "Kaptan", 0, "Mevkideki oyuncular�n g�c�n� X2 yapar", 0, "Cards/defCap2"));
            cardList.Add(new Card(CardType.Captain, 1000004, "Kaptan", 0, "Mevkideki oyuncular�n g�c�n� X2 yapar", 0, "Cards/defCap1"));

            cardList.Add(new Card(CardType.Player, 0102001, "Neyvar", 10, "�al�m ustas�, �st d�zey ve etkili forvet oyuncusu", 2, "Cards/neyvar"));
            cardList.Add(new Card(CardType.Player, 0102002, "Mesci.Y", 10, "T�m zamanlar�n en iyi futbolcusu olarak bilinir", 2, "Cards/mesciy"));
            cardList.Add(new Card(CardType.Player, 0102003, "Domaldo", 10, "Efsanevi forvet, gol makinesi ve tak�m lideri", 2, "Cards/domaldo"));
            cardList.Add(new Card(CardType.Player, 0092010, "Lewandoesk", 9, "G��l� fizi�i ve golc�l���yle tan�nan etkili forvet", 2, "Cards/lewandoesk"));
            cardList.Add(new Card(CardType.Player, 0092011, "Embapo", 9, "H�zl� ve etkili ataklarla rakip savunmay� zorlar", 2, "Cards/embapo"));
            cardList.Add(new Card(CardType.Player, 0091001, "Biruni", 9, "�ok y�nl�, oyun kurmada etkili orta saha oyuncusu", 1, "Cards/biruni"));
            cardList.Add(new Card(CardType.Player, 0090001, "S.Vamos", 9, "Agresif kaptan, tak�m�n� motive eden lider", 0, "Cards/svamos"));
            cardList.Add(new Card(CardType.Player, 0082001, "Djbala", 8, "�evik ve h�zl�, kritik pozisyon yarat�r", 2, "Cards/djbala"));
            cardList.Add(new Card(CardType.Player, 0081001, "Decenrice", 8, "Dayan�kl� yap�s� ve y�ksek pas isabet oran�", 1, "Cards/decenrice"));
            cardList.Add(new Card(CardType.Player, 0081002, "Kralex", 8, "Paslar� isabetli, oyunu iyi y�nlendiren kurucu", 1, "Cards/kralex"));
            cardList.Add(new Card(CardType.Player, 0080003, "Kimdir", 8, "Oyun kurucu defans, savunmay� y�nlendiren isim", 0, "Cards/kimdir"));
            cardList.Add(new Card(CardType.Player, 0080004, "Hekimo�lu", 8, "Atak g�c� y�ksek, sa� bek mevkiinde ba�ar�l�", 0, "Cards/hekimoglu"));
            cardList.Add(new Card(CardType.Player, 0080005, "Pandik", 8, "Sa�lam ve g�venilir defans oyuncusu", 0, "Cards/pandik"));
            cardList.Add(new Card(CardType.Player, 0072003, "Salak", 7, "�abuk ve etkili h�cum g�c�yle fark yarat�r", 2, "Cards/salak"));
            cardList.Add(new Card(CardType.Player, 0062001, "Sakaro", 6, "Kanat forvet, h�zl� ve �evik oyun tarz�", 2, "Cards/sakaro"));
            cardList.Add(new Card(CardType.Player, 0061002, "Riboz", 6, "Dayan�kl� ve g��l� yap�s�yla orta saha kontrol�", 1, "Cards/riboz"));
            cardList.Add(new Card(CardType.Player, 0072004, "Leon", 7, "H�zl� ve teknik kanat forvet, rakip savunmay� deler", 2, "Cards/leon"));
            cardList.Add(new Card(CardType.Player, 0062005, "Leman", 6, "Kanat forvet, h�cuma h�zl� katk� sa�lar", 2, "Cards/leman"));
            cardList.Add(new Card(CardType.Player, 0072006, "Kerizman", 7, "Tecr�beli golc�, kritik anlarda �ne ��kar", 2, "Cards/kerizman"));
            cardList.Add(new Card(CardType.Player, 0061007, "Gullu", 6, "Teknik ve h�zl� 10 numara, oyunu kurar", 1, "Cards/gullu"));
            cardList.Add(new Card(CardType.Player, 0061008, "Branda", 6, "Yarat�c� ve pas�r orta saha, tak�m�n beyni", 1, "Cards/branda"));
            cardList.Add(new Card(CardType.Player, 0061009, "Balongol", 6, "G��l� ve dinamik orta saha, savunmaya destek", 1, "Cards/balongol"));
            cardList.Add(new Card(CardType.Player, 0072007, "Reyis", 7, "M�cadeleci, tak�m�na ba�l� sahada lider", 2, "Cards/reyis"));
            cardList.Add(new Card(CardType.Player, 0071010, "Arraske", 7, "Teknik ve yarat�c� orta saha oyuncusu", 1, "Cards/arraske"));
            cardList.Add(new Card(CardType.Player, 0071011, "Calhanoro", 7, "Uzaktan �utlar� etkili, tehlikeli orta saha", 1, "Cards/calhanoro"));
            cardList.Add(new Card(CardType.Player, 0071012, "Pedroso", 7, "Gen� ve yetenekli, gelece�in y�ld�z� orta saha", 1, "Cards/pedroso"));
            cardList.Add(new Card(CardType.Player, 0070001, "Kadiolu", 7, "�ift y�nl�, dinamik bek, savunma ve h�cumda aktif", 0, "Cards/kadiolu"));
            cardList.Add(new Card(CardType.Player, 0070002, "Otemar", 7, "Tecr�beli ve sert stoper, hava toplar�nda g��l�", 0, "Cards/otemar"));
            cardList.Add(new Card(CardType.Player, 0070003, "Demeran", 7, "Sert ve g��l� stoper, rakip ataklar�n� keser", 0, "Cards/demeran"));
            cardList.Add(new Card(CardType.Player, 0070004, "Cacaorelle", 7, "H�zl� ve teknik sol bek, h�cuma destek verir", 0, "Cards/cacaorelle"));
            cardList.Add(new Card(CardType.Player, 0061013, "Rafasel", 6, "H�zl�, �evik ofansif orta saha, yarat�c� isim", 1, "Cards/rafasel"));
            cardList.Add(new Card(CardType.Player, 0060009, "Nevaldo", 6, "Orta sahadan defansa destek veren oyuncu", 0, "Cards/nevaldo"));
            cardList.Add(new Card(CardType.Player, 0060010, "Malverdi", 6, "�ok y�nl� stoper, savunmada kritik g�rev �stlenir", 0, "Cards/malverdi"));
            cardList.Add(new Card(CardType.Player, 0060011, "Kurtaran", 6, "Kaleci, kritik kurtar��lar�yla tak�m�n� korur", 0, "Cards/kurtaran"));
            cardList.Add(new Card(CardType.Player, 0060012, "Sanvicho", 6, "Fizik g�c� y�ksek sert stoper, savunman�n bel kemi�i", 0, "Cards/sanvicho"));
            cardList.Add(new Card(CardType.Player, 0060013, "Minato", 6, "Hava toplar�nda etkili ve g�venilir defans", 0, "Cards/minato"));
            cardList.Add(new Card(CardType.Player, 0050013, "Edrovin", 5, "Kaleci, h�zl� refleksleriyle rakip �utlar�n� engeller", 0, "Cards/edrovin"));
            cardList.Add(new Card(CardType.Player, 0052014, "Borisalpor", 5, "H�rsl� ve m�cadeleci, tak�m�na enerji katan forvet", 2, "Cards/borisalpor"));
            cardList.Add(new Card(CardType.Player, 0040014, "Fordi", 4, "Gen� ve yetenekli, gelece�in defans oyuncusu", 0, "Cards/fordi"));
            cardList.Add(new Card(CardType.Player, 0021015, "Lingaldo", 2, "Geli�mekte olan orta saha, potansiyeli y�ksek", 1, "Cards/lingaldo"));
        }
    }
}