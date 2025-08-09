# Football-Themed Card Game / Futbol Temalı Kart Oyunu 

## About / Proje Hakkında

This project is a football-themed card game developed using [Unity](https://unity.com/). Players manage a deck composed of various card types such as Players, Captains, Coaches, and Weather effects to compete on the field. Gameplay focuses on strategic card placement and dynamic power management influenced by card types and environmental factors.  

Bu proje, Unity kullanılarak geliştirilmiş futbol temalı bir kart oyunudur. Oyuncular, oyuncu kartları, kaptan kartları, teknik adam (coach) kartları ve hava durumu etkileri gibi farklı kart türlerinden oluşan destelerini yöneterek saha üzerinde mücadele ederler. Oyun, stratejik kart yerleştirme ve kart türleri ile çevresel faktörlerin etkilediği güç yönetimi üzerine kuruludur.

---

## Installation / Kurulum

### Prerequisites / Gereksinimler

- [Unity 2023.x](https://unity.com/releases) (or the version used in the project)  
- Visual Studio or any C# IDE  
- [TextMesh Pro](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.0/manual/index.html) (via Unity Package Manager)  
- Required assets (card images, UI elements, etc.)  

### How to Run [EN]
1. Open the project with Unity Hub.  
2. Make sure you have the required Unity version installed.  
3. Add scenes from `Assets/Scenes` to Build Settings.  
4. Use `SplashScene` and `MenuScene` as main scenes.  
5. Build and run the project.
### Çalıştırma [TR]
1. Unity Hub ile projeyi açın.  
2. Gerekli Unity sürümünün yüklü olduğundan emin olun.  
3. `Assets/Scenes` klasöründeki sahneleri Build Settings’e ekleyin.  
4. Ana sahne olarak `SplashScene` ve `MenuScene` kullanılır.  
5. Projeyi derleyip çalıştırın.

---

## Usage [EN]
- Select single or multiplayer mode from the main menu.  
- Receive player cards and play them strategically on the field.  
- Captain and weather cards dynamically influence card powers.  
- Set nickname (3 characters) and avatar in the options menu.  
- Access in-game menu for returning to main menu, changing settings, or quitting.
## Kullanım [TR]
- Ana menüden tek oyunculu veya çok oyunculu mod seçin.  
- Oyuncu kartları alır ve stratejik olarak sahaya oynarsınız.  
- Kaptan ve hava durumu kartları, kart güçlerini dinamik olarak etkiler.  
- Ayarlar menüsünden 3 karakterlik takma adınızı ve avatarınızı seçebilirsiniz.  
- Oyun içi menüden ana menüye dönebilir, ayarları değiştirebilir veya çıkış yapabilirsiniz.

---

## Features [EN]
- Various card types: Player, Captain, Coach, Weather  
- Dynamic power strategy based on card values  
- Nickname and avatar selection saved via PlayerPrefs  
- Infrastructure ready for multiplayer mode  
- Simple and functional UI  
- In-game and main menu settings panels  
- Singleton pattern for data management (PlayerDataManager)
## Özellikler [TR]
- Farklı kart türleri: Oyuncu, Kaptan, Teknik Adam (Coach), Hava Durumu  
- Kart değerlerine göre dinamik güç stratejisi  
- Takma ad ve avatar seçimi PlayerPrefs ile kayıt edilir  
- Çok oyunculu mod için altyapı hazır  
- Basit ve işlevsel kullanıcı arayüzü  
- Oyun içi ve ana menü ayar panelleri  
- Veri yönetimi için singleton tasarım deseni (PlayerDataManager)

---

## Technologies [EN]
- [Unity 2023.x](https://unity.com/)  
- C# programming language  
- [TextMesh Pro](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.0/manual/index.html)  
- UnityEngine.SceneManagement  
- PlayerPrefs data storage  
- LINQ (for filtering card lists)
## Teknolojiler [TR]
- [Unity 2023.x](https://unity.com/)  
- C# Programlama Dili  
- [TextMesh Pro](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.0/manual/index.html)  
- UnityEngine.SceneManagement  
- PlayerPrefs veri saklama  
- LINQ (kart listesi filtrelemek için)

---

## Contributing [EN] 
- Install the required Unity version and dependencies.  
- Add new cards, mechanics, or UI improvements.  
- Follow singleton and data management patterns.  
- Keep your code clean and well-commented.  
- Use clear commit messages when submitting pull requests.
## Katkı Sağlama [TR]
- Gerekli Unity sürümünü ve bağımlılıkları kurun.  
- Yeni kartlar, mekanikler veya UI geliştirmeleri ekleyin.  
- Singleton ve veri yönetimi desenlerine uyun.  
- Kodunuzu temiz ve yorumlu tutun.  
- Pull request gönderirken açıklayıcı commit mesajları kullanın.

---

## Documentation [EN]  
- Check the `Docs` folder for detailed code structure and scene layout.  
- Card data defined in the `Card` class and `CardDatabase` script.  
- Player data managed via the singleton `PlayerDataManager`.  
- Menu and in-game UI organized in `MenuManager`, `InGameMenuManager`, and `GameUIManager`.
## Dokümantasyon [TR]
- Kod yapısı ve sahne düzeni için `Docs` klasörünü inceleyin.  
- Kart verisi `Card` sınıfı ve `CardDatabase` scriptinde tanımlıdır.  
- Oyuncu verileri singleton `PlayerDataManager` ile yönetilir.  
- Menü ve oyun içi UI `MenuManager`, `InGameMenuManager` ve `GameUIManager` scriptlerinde organize edilmiştir.

---

## Acknowledgments [EN] 
- Project idea and design: Mustafa Alper Özdemir  
- Unity community and official documentation  
- TextMesh Pro developers  
- Card game example projects and references  
## Teşekkürler [TR]
- Proje fikri ve tasarımı: Mustafa Alper Özdemir  
- Unity topluluğu ve resmi dokümantasyon  
- TextMesh Pro geliştiricileri  
- Kart oyunu örnek projeleri ve referanslar  


