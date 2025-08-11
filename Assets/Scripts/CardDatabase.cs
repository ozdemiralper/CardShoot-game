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
            cardList.Add(new Card(CardType.Coach, 3000003, "Hamsi", 0, "Motivasyonu yüksek antrenör", 0, "Cards/coach3"));
            cardList.Add(new Card(CardType.Coach, 3000002, "Amcalotti", 0, "Strateji uzmaný teknik direktör", 0, "Cards/coach2"));
            cardList.Add(new Card(CardType.Coach, 3000001, "Malinho", 0, "Deneyimli ve bilgili teknik adam", 0, "Cards/coach1"));

            cardList.Add(new Card(CardType.Weather, 2022003, "Wind", 2, "Rüzgar forvet oyuncularýný zorlar", 2, "Cards/wind"));
            cardList.Add(new Card(CardType.Weather, 2011002, "Snow", 1, "Kar orta saha oyuncularýna zorluk", 1, "Cards/snow"));
            cardList.Add(new Card(CardType.Weather, 2000001, "Rain", 0, "Yaðmur defans oyuncularýný zorlar", 0, "Cards/rain"));

            cardList.Add(new Card(CardType.Captain, 1022009, "Kaptan", 2, "Mevkideki oyuncularýn gücünü X2 yapar", 2, "Cards/forCap2"));
            cardList.Add(new Card(CardType.Captain, 1022008, "Kaptan", 2, "Mevkideki oyuncularýn gücünü X2 yapar", 2, "Cards/forCap1"));
            cardList.Add(new Card(CardType.Captain, 1011007, "Kaptan", 1, "Mevkideki oyuncularýn gücünü X2 yapar", 1, "Cards/midCap2"));
            cardList.Add(new Card(CardType.Captain, 1011006, "Kaptan", 1, "Mevkideki oyuncularýn gücünü X2 yapar", 1, "Cards/midCap1"));
            cardList.Add(new Card(CardType.Captain, 1000005, "Kaptan", 0, "Mevkideki oyuncularýn gücünü X2 yapar", 0, "Cards/defCap1"));
            cardList.Add(new Card(CardType.Captain, 1000004, "Kaptan", 0, "Mevkideki oyuncularýn gücünü X2 yapar", 0, "Cards/defCap1"));

            cardList.Add(new Card(CardType.Player, 0102008, "Neyvar", 10, "Çalým ustasý, üst düzey forvet", 2, "Cards/neyvar"));
            cardList.Add(new Card(CardType.Player, 0102002, "Mesci.Y", 10, "Tüm zamanlarýn en iyi futbolcusu", 2, "Cards/mesciy"));
            cardList.Add(new Card(CardType.Player, 0102001, "Domaldo", 10, "Efsanevi forvet, gol makinesi", 2, "Cards/domaldo"));
            cardList.Add(new Card(CardType.Player, 0092012, "Lewandoesk", 9, "Güçlü ve golcü forvet", 2, "Cards/lewandoesk"));
            cardList.Add(new Card(CardType.Player, 0092011, "Embapo", 9, "Hýzlý ve etkili forvet oyuncusu", 2, "Cards/embapo"));
            cardList.Add(new Card(CardType.Player, 0091004, "Biruni", 9, "Çok yönlü ve etkili orta saha", 1, "Cards/biruni"));
            cardList.Add(new Card(CardType.Player, 0090010, "S.Vamos", 9, "Agresif kaptan, lider ruhu", 0, "Cards/svamos"));
            cardList.Add(new Card(CardType.Player, 0082005, "Djbala", 8, "Hýzlý ve çevik forvet oyuncusu", 2, "Cards/djbala"));
            cardList.Add(new Card(CardType.Player, 0081013, "Decenrice", 8, "Dayanýklý ve pas isabeti yüksek", 1, "Cards/decenrice"));
            cardList.Add(new Card(CardType.Player, 0081007, "Kralex", 8, "Paslarý isabetli ve oyun kurucu", 1, "Cards/kralex"));
            cardList.Add(new Card(CardType.Player, 0080036, "Kimdir", 8, "Oyun kurucu defans", 0, "Cards/kimdir"));
            cardList.Add(new Card(CardType.Player, 0080035, "Hekimoðlu", 8, "Atak gücü yüksek sað bek", 0, "Cards/hekimoglu"));
            cardList.Add(new Card(CardType.Player, 0080003, "Pandik", 8, "Saðlam ve güvenilir defans oyuncusu", 0, "Cards/pandik"));
            cardList.Add(new Card(CardType.Player, 0072030, "Salak", 7, "Çabuk ve etkili hücum oyuncusu", 2, "Cards/salak"));
            cardList.Add(new Card(CardType.Player, 0071029, "Sakaro", 6, "Kanat forvet", 2, "Cards/sakaro"));
            cardList.Add(new Card(CardType.Player, 0071028, "Riboz", 6, "Dayanýklý ve güçlü orta saha", 1, "Cards/riboz"));
            cardList.Add(new Card(CardType.Player, 0071027, "Leon", 7, "Hýzlý ve teknik kanat forveti", 2, "Cards/leon"));
            cardList.Add(new Card(CardType.Player, 0071026, "Leman", 6, "Kanat forvet", 2, "Cards/leman"));
            cardList.Add(new Card(CardType.Player, 0071025, "Kerizman", 7, "Tecrübeli golcü", 2, "Cards/kerizman"));
            cardList.Add(new Card(CardType.Player, 0071024, "Gullu", 6, "Teknik ve hýzlý 10 numara", 1, "Cards/gullu"));
            cardList.Add(new Card(CardType.Player, 0071023, "Branda", 7, "Yaratýcý ve pasör orta saha", 1, "Cards/branda"));
            cardList.Add(new Card(CardType.Player, 0071021, "Balongol", 6, "Güçlü, dinamik orta saha", 1, "Cards/balongol"));
            cardList.Add(new Card(CardType.Player, 0072009, "Reyis", 7, "Mücadeleci ve takýmýna baðlý", 2, "Cards/reyis"));
            cardList.Add(new Card(CardType.Player, 0071018, "Arraske", 7, "Teknik ve yaratýcý orta saha", 1, "Cards/arraske"));
            cardList.Add(new Card(CardType.Player, 0071016, "Calhanoro", 7, "Uzaktan þutlarý etkili orta saha", 1, "Cards/calhanoro"));
            cardList.Add(new Card(CardType.Player, 0071007, "Kralex", 8, "Paslarý isabetli ve oyun kurucu", 1, "Cards/kralex"));
            cardList.Add(new Card(CardType.Player, 0071006, "Pedroso", 7, "Genç ve yetenekli orta saha", 1, "Cards/pedroso"));
            cardList.Add(new Card(CardType.Player, 0070017, "Kadiolu", 7, "Çift yönlü ve dinamik bek", 0, "Cards/kadiolu"));
            cardList.Add(new Card(CardType.Player, 0070040, "Otemar", 7, "Tecrübeli ve sert stoper", 0, "Cards/otemar"));
            cardList.Add(new Card(CardType.Player, 0070032, "Demeran", 7, "Sert ve güçlü stoper", 0, "Cards/demeran"));
            cardList.Add(new Card(CardType.Player, 0070031, "Cacaorelle", 7, "Hýzlý ve teknik sol bek", 0, "Cards/cacaorelle"));
            cardList.Add(new Card(CardType.Player, 0061029, "Sakaro", 6, "Kanat forvet", 2, "Cards/sakaro"));
            cardList.Add(new Card(CardType.Player, 0061028, "Riboz", 6, "Dayanýklý ve güçlü orta saha", 1, "Cards/riboz"));
            cardList.Add(new Card(CardType.Player, 0061024, "Gullu", 6, "Teknik ve hýzlý 10 numara", 1, "Cards/gullu"));
            cardList.Add(new Card(CardType.Player, 0061019, "Rafasel", 6, "Hýzlý ve çevik ofansif orta saha", 1, "Cards/rafasel"));
            cardList.Add(new Card(CardType.Player, 0061021, "Balongol", 6, "Güçlü, dinamik orta saha", 1, "Cards/balongol"));
            cardList.Add(new Card(CardType.Player, 0060039, "Nevaldo", 6, "Orta sahadan defansa destek", 0, "Cards/nevaldo"));
            cardList.Add(new Card(CardType.Player, 0060038, "Malverdi", 6, "Çok yönlü stoper", 0, "Cards/malverdi"));
            cardList.Add(new Card(CardType.Player, 0060037, "Kurtaran", 6, "Kaleci, kritik kurtarýþlar yapar", 0, "Cards/kurtaran"));
            cardList.Add(new Card(CardType.Player, 0060026, "Leman", 6, "Kanat forvet", 2, "Cards/leman"));
            cardList.Add(new Card(CardType.Player, 0060020, "Sanvicho", 6, "Fizik gücü yüksek sert stoper", 0, "Cards/sanvicho"));
            cardList.Add(new Card(CardType.Player, 0060014, "Minato", 6, "Hava toplarýnda etkili defans", 0, "Cards/minato"));
            cardList.Add(new Card(CardType.Player, 0050033, "Edrovin", 5, "Kaleci, refleksleri üst düzey", 0, "Cards/edrovin"));
            cardList.Add(new Card(CardType.Player, 0050022, "Borisalpor", 5, "Hýrslý ve mücadeleci forvet", 2, "Cards/borisalpor"));
            cardList.Add(new Card(CardType.Player, 0040034, "Fordi", 4, "Genç ve yetenekli defans", 0, "Cards/fordi"));
            cardList.Add(new Card(CardType.Player, 0021015, "Lingaldo", 2, "Geliþmekte olan orta saha oyuncusu", 1, "Cards/lingaldo"));

        }
    }
}