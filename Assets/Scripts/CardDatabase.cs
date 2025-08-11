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
            cardList.Add(new Card(CardType.Captain, 1000005, "Kaptan", 0, "Mevkideki oyuncularýn gücünü X2 yapar", 0, "Cards/defCap2"));
            cardList.Add(new Card(CardType.Captain, 1000004, "Kaptan", 0, "Mevkideki oyuncularýn gücünü X2 yapar", 0, "Cards/defCap1"));

            cardList.Add(new Card(CardType.Player, 0102001, "Neyvar", 10, "Çalým ustasý, üst düzey ve etkili forvet oyuncusu", 2, "Cards/neyvar"));
            cardList.Add(new Card(CardType.Player, 0102002, "Mesci.Y", 10, "Tüm zamanlarýn en iyi futbolcusu olarak bilinir", 2, "Cards/mesciy"));
            cardList.Add(new Card(CardType.Player, 0102003, "Domaldo", 10, "Efsanevi forvet, gol makinesi ve takým lideri", 2, "Cards/domaldo"));
            cardList.Add(new Card(CardType.Player, 0092010, "Lewandoesk", 9, "Güçlü fiziði ve golcülüðüyle tanýnan etkili forvet", 2, "Cards/lewandoesk"));
            cardList.Add(new Card(CardType.Player, 0092011, "Embapo", 9, "Hýzlý ve etkili ataklarla rakip savunmayý zorlar", 2, "Cards/embapo"));
            cardList.Add(new Card(CardType.Player, 0091001, "Biruni", 9, "Çok yönlü, oyun kurmada etkili orta saha oyuncusu", 1, "Cards/biruni"));
            cardList.Add(new Card(CardType.Player, 0090001, "S.Vamos", 9, "Agresif kaptan, takýmýný motive eden lider", 0, "Cards/svamos"));
            cardList.Add(new Card(CardType.Player, 0082001, "Djbala", 8, "Çevik ve hýzlý, kritik pozisyon yaratýr", 2, "Cards/djbala"));
            cardList.Add(new Card(CardType.Player, 0081001, "Decenrice", 8, "Dayanýklý yapýsý ve yüksek pas isabet oraný", 1, "Cards/decenrice"));
            cardList.Add(new Card(CardType.Player, 0081002, "Kralex", 8, "Paslarý isabetli, oyunu iyi yönlendiren kurucu", 1, "Cards/kralex"));
            cardList.Add(new Card(CardType.Player, 0080003, "Kimdir", 8, "Oyun kurucu defans, savunmayý yönlendiren isim", 0, "Cards/kimdir"));
            cardList.Add(new Card(CardType.Player, 0080004, "Hekimoðlu", 8, "Atak gücü yüksek, sað bek mevkiinde baþarýlý", 0, "Cards/hekimoglu"));
            cardList.Add(new Card(CardType.Player, 0080005, "Pandik", 8, "Saðlam ve güvenilir defans oyuncusu", 0, "Cards/pandik"));
            cardList.Add(new Card(CardType.Player, 0072003, "Salak", 7, "Çabuk ve etkili hücum gücüyle fark yaratýr", 2, "Cards/salak"));
            cardList.Add(new Card(CardType.Player, 0062001, "Sakaro", 6, "Kanat forvet, hýzlý ve çevik oyun tarzý", 2, "Cards/sakaro"));
            cardList.Add(new Card(CardType.Player, 0061002, "Riboz", 6, "Dayanýklý ve güçlü yapýsýyla orta saha kontrolü", 1, "Cards/riboz"));
            cardList.Add(new Card(CardType.Player, 0072004, "Leon", 7, "Hýzlý ve teknik kanat forvet, rakip savunmayý deler", 2, "Cards/leon"));
            cardList.Add(new Card(CardType.Player, 0062005, "Leman", 6, "Kanat forvet, hücuma hýzlý katký saðlar", 2, "Cards/leman"));
            cardList.Add(new Card(CardType.Player, 0072006, "Kerizman", 7, "Tecrübeli golcü, kritik anlarda öne çýkar", 2, "Cards/kerizman"));
            cardList.Add(new Card(CardType.Player, 0061007, "Gullu", 6, "Teknik ve hýzlý 10 numara, oyunu kurar", 1, "Cards/gullu"));
            cardList.Add(new Card(CardType.Player, 0061008, "Branda", 6, "Yaratýcý ve pasör orta saha, takýmýn beyni", 1, "Cards/branda"));
            cardList.Add(new Card(CardType.Player, 0061009, "Balongol", 6, "Güçlü ve dinamik orta saha, savunmaya destek", 1, "Cards/balongol"));
            cardList.Add(new Card(CardType.Player, 0072007, "Reyis", 7, "Mücadeleci, takýmýna baðlý sahada lider", 2, "Cards/reyis"));
            cardList.Add(new Card(CardType.Player, 0071010, "Arraske", 7, "Teknik ve yaratýcý orta saha oyuncusu", 1, "Cards/arraske"));
            cardList.Add(new Card(CardType.Player, 0071011, "Calhanoro", 7, "Uzaktan þutlarý etkili, tehlikeli orta saha", 1, "Cards/calhanoro"));
            cardList.Add(new Card(CardType.Player, 0071012, "Pedroso", 7, "Genç ve yetenekli, geleceðin yýldýzý orta saha", 1, "Cards/pedroso"));
            cardList.Add(new Card(CardType.Player, 0070001, "Kadiolu", 7, "Çift yönlü, dinamik bek, savunma ve hücumda aktif", 0, "Cards/kadiolu"));
            cardList.Add(new Card(CardType.Player, 0070002, "Otemar", 7, "Tecrübeli ve sert stoper, hava toplarýnda güçlü", 0, "Cards/otemar"));
            cardList.Add(new Card(CardType.Player, 0070003, "Demeran", 7, "Sert ve güçlü stoper, rakip ataklarýný keser", 0, "Cards/demeran"));
            cardList.Add(new Card(CardType.Player, 0070004, "Cacaorelle", 7, "Hýzlý ve teknik sol bek, hücuma destek verir", 0, "Cards/cacaorelle"));
            cardList.Add(new Card(CardType.Player, 0061013, "Rafasel", 6, "Hýzlý, çevik ofansif orta saha, yaratýcý isim", 1, "Cards/rafasel"));
            cardList.Add(new Card(CardType.Player, 0060009, "Nevaldo", 6, "Orta sahadan defansa destek veren oyuncu", 0, "Cards/nevaldo"));
            cardList.Add(new Card(CardType.Player, 0060010, "Malverdi", 6, "Çok yönlü stoper, savunmada kritik görev üstlenir", 0, "Cards/malverdi"));
            cardList.Add(new Card(CardType.Player, 0060011, "Kurtaran", 6, "Kaleci, kritik kurtarýþlarýyla takýmýný korur", 0, "Cards/kurtaran"));
            cardList.Add(new Card(CardType.Player, 0060012, "Sanvicho", 6, "Fizik gücü yüksek sert stoper, savunmanýn bel kemiði", 0, "Cards/sanvicho"));
            cardList.Add(new Card(CardType.Player, 0060013, "Minato", 6, "Hava toplarýnda etkili ve güvenilir defans", 0, "Cards/minato"));
            cardList.Add(new Card(CardType.Player, 0050013, "Edrovin", 5, "Kaleci, hýzlý refleksleriyle rakip þutlarýný engeller", 0, "Cards/edrovin"));
            cardList.Add(new Card(CardType.Player, 0052014, "Borisalpor", 5, "Hýrslý ve mücadeleci, takýmýna enerji katan forvet", 2, "Cards/borisalpor"));
            cardList.Add(new Card(CardType.Player, 0040014, "Fordi", 4, "Genç ve yetenekli, geleceðin defans oyuncusu", 0, "Cards/fordi"));
            cardList.Add(new Card(CardType.Player, 0021015, "Lingaldo", 2, "Geliþmekte olan orta saha, potansiyeli yüksek", 1, "Cards/lingaldo"));
        }
    }
}