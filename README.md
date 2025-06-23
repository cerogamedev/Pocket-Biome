# 🌱 Pocket Biome

*Bir tık-ve-izle ekosistem micromanagement oyunu*  
Unity 2021.3 · 2D · Pico-esque pixel-art  

![Unity](https://img.shields.io/badge/engine-Unity_2021.3-black.svg)
![License](https://img.shields.io/badge/license-MIT-green.svg)
![Build](https://img.shields.io/github/actions/workflow/status/YourUser/PocketBiome/unity.yml?label=CI%20build)

itch.io üzerinden deneyebilirsiniz <3 
https://cerobaby.itch.io/pocket-biome


<p align="center">
  <img src="docs/screenshots/cover.gif" width="640" alt="Pocket Biome timelapse" />
</p>

![image](https://github.com/user-attachments/assets/d2ae22b5-6ec1-49b4-942b-7eacd78514ed)
![image](https://github.com/user-attachments/assets/2dde59db-801f-4745-a036-07f25f2418e1)
![image](https://github.com/user-attachments/assets/4043a371-c018-4a58-a970-0bbefaaa8131)
![image](https://github.com/user-attachments/assets/fabc8a8c-eea6-4bd2-9521-9c9931c8f17d)



---

## 📖 İçindekiler
- [Özet](#özet)
- [Ekran Görüntüleri](#ekran-görüntüleri)
- [Oynanış](#oynanış)
  - [Kurallar](#kurallar)
  - [Kontroller](#kontroller)
  - [Tur Akışı](#tur-akışı)
  - [Skorlama](#skorlama)
  - [Mutasyonlar](#mutasyonlar)
  - [Mevsimler](#mevsimler)
- [Kurulum](#kurulum)
  - [Editörde Çalıştırma](#editörde-çalıştırma)
  - [Build Alma](#build-alma)
- [Teknik Mimari](#teknik-mimari)
  - [Önemli Design Pattern’ler](#önemli-design-patternler)
  - [Dizin Yapısı](#dizin-yapısı)
- [Yol Haritası](#yol-haritası)
- [Katkıda Bulunanlar](#katkıda-bulunanlar)
- [Lisans](#lisans)

---

## Özet

**Pocket Biome**, 16 × 16 hücrelik minimal bir adada geçen, döngüsel ekosistem simülasyonudur.  
Oyuncu her tur **yalnızca bir** toprağı sulayarak bitkilerin yayılmasına ve hayvanların
doğmasına aracılık eder. Amaç, **30 tur** sonunda mümkün olduğu kadar **zengin ve yoğun**
bir biyoçeşitlilik yaratmaktır. Basit tıklamalarla başlayıp derinleşen kuralları,
pixel-art estetiği ve “neredeyse idle” oynanışıyla hızlı ama stratejik bir deneyim sunar.

---

## Oynanış

### Kurallar
1. **Sulama** – Turunuzda herhangi bir **kuru** hücreye tıklayın; hücre “nemli” olur.  
2. **Bitki Yayılımı** – Tur sonunda:  
   - Nemli bir hücrede % _χ_ olasılıkla (mevsim × mutasyon çarpanlı) bitki filizlenir.  
   - Bitkili hücreler, komşu dört kareye % _φ_ olasılıkla yayılır.  
3. **Hayvan Doğumu** – Bitkili ama hayvansız hücrelerde tavşan doğma ihtimali vardır.  
4. **Hayvan Davranışı** – Tavşan her tur bulunduğu hücrede bir bitki yer; 3 tur aç kalırsa ölür.  
5. **Mevsimler** – İlkbahar → Yaz → Sonbahar → Kış döngüsü; kışta nemli hücreler donar, büyüme keskin düşer.  
6. **Mutasyonlar** – 5., 15. ve 25. turlarda üç rastgele DNA yükseltisinden biri seçilir.  
7. **Oyun Sonu** – 30. tur bitince skor paneli görünür.

### Kontroller
| Eylem | Klavye / Fare |
|-------|---------------|
| Hücreyi sulama | **Sol-mouse** tık |
| Sonraki tur | **Next Turn** UI butonu |
| Yeniden başlat | Oyun sonu panelindeki **Restart** |

### Tur Akışı
1. Oyuncu sulama yapar.  
2. `GridManager` → hücre durumları günceller (`AdvanceTurn`).  
3. `EcosystemManager` → bitki yayılımı, hayvan doğumu & davranışı.  
4. `SeasonManager` → tur sayısını artırır, gerekiyorsa mevsim değiştirir.  
5. UI turn ve mevsim etiketleri yenilenir; eğer _mutation turn_ ise
   DNA popup’ı gösterilir.

### Skorlama
```text
Skor = Popülasyon  ×  Çeşitlilik
       (canlı Plant+Animal sayısı)   (farklı tür adedi)
```

### Mutasyonlar

| İsim | Tip | Etki |
|------|-----|------|
| **Rich Soil** | SpreadBoost | Bitki yayılımı × 1.5 |
| **Fertile Womb** | BirthBoost | Hayvan doğumu × 1.4 |
| **Wet Retainer** | MoistLonger | Nemli hücreler +2 tur ıslak kalır |

### Mevsimler

| Mevsim | Tur | Büyüme × | Doğum × | Özel |
|--------|-----|---------|---------|------|
| İlkbahar | 8 | 1.2 | 1.2 | – |
| Yaz | 8 | 1.0 | 1.0 | – |
| Sonbahar | 8 | 0.8 | 0.9 | – |
| **Kış** | 6 | 0.25 | 0.4 | Nemli → Donmuş |

---

## Kurulum

### Editörde Çalıştırma
```bash
git clone https://github.com/YourUser/PocketBiome.git
cd PocketBiome
# Unity Hub → Add → klasörü seç → Unity 2021.3 LTS ile aç
```

> *Not:* Proje klasik 2D pipeline kullanır; URP veya HDRP gerektirmez.

### Build Alma
1. `File ▸ Build Settings ▸ Add Open Scenes`  
2. **Target**: Standalone Windows/Mac/Linux ⚙️  
3. `Build ▸` çıktıyı `Builds/` klasörüne kaydet.

---

## Teknik Mimari

### Önemli Design Pattern’ler
| Desen | Nerede? | Amaç |
|-------|---------|------|
| **Singleton** | `GameManager`, `SeasonManager`, `MutationManager` | Küresel hizmet |
| **State** | `Dry / Moist / Frozen` hücre durumları | Aynı nesne, değişen davranış |
| **Strategy** | `ISpreadStrategy` (bitki yayılımı) | Tür-bazlı algoritma enjeksiyonu |
| **Factory** | `CellFactory`, `SpeciesFactory` (gelecekte) | Nesne oluşturma soyutlaması |
| **Observer (C# event)** | Mevsim değişimleri → UI güncellemesi | Gevşek bağlanma |
| **Object Pool** | (Roadmap) Plant & Animal prefabları | GC baskısını düşürmek |

### Dizin Yapısı
```text
Assets/
 ├─ Scripts/
 │   ├─ Core/        ← Game loop & yöneticiler
 │   ├─ Grid/        ← Hücre & GridManager
 │   ├─ States/      ← ICellState alt sınıfları
 │   ├─ Species/     ← Plant & Animal veri/bileşenleri
 │   ├─ Season/      ← SeasonManager + SO
 │   ├─ Mutation/    ← MutationManager + SO
 │   └─ UI/
 ├─ Prefabs/
 ├─ Sprites/
 ├─ ScriptableObjects/
 └─ Docs/            ← GIF & PNG’ler, ek belgeler
```

---

## Yol Haritası
- [ ] 🎵 **Ses Cilası** – Ambient lo-fi loop, sulama & doğum SFX  
- [ ] 🕹️ **Gamepad Desteği** – A/B tuş eşlemesi  
- [ ] 💾 **Save/Load** – JSON snapshot ile tek tuş kayıt  
- [ ] 🌐 **WebGL Build** – itch.io’da oynanabilir demo  
- [ ] 🤖 **Yeni Hayvan AI’sı** – Yırtıcı (Carnivore) zinciri  
- [ ] 📈 **Steam Leaderboard** – Skor paylaşımı  

---

## Katkıda Bulunanlar
| Rol | İsim |
|-----|------|
| Tasarım & Kod | **Cherri “cero gor”** |
| Pixel-Art | **Cherri** |

> Katkıda bulunmak isterseniz `Issues` ya da `Pull Requests` dikkatle incelenir. Kod biçimlendirme için `.editorconfig` dosyasına uyun.

---

## Lisans
Bu proje **MIT Lisansı** ile sunulmaktadır – ayrıntı için `LICENSE` dosyasına bakınız.

---

<p align="center">
  <b>🌿 30 Turda kendi mikro-cennetini yarat, dengenin ne kadar kırılgan olduğunu gör!</b>
</p>
