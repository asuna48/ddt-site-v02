# DDTank Multiple Server For Test Login

Bu depo, ASP.NET Core Razor Pages kullanılarak geliştirilmiş basit bir DDTank çoklu sunucu test arayüzüdür. Test amaçlı kullanım için hızlı kayıt/giriş, dashboard (sunucu seçimi) ve Play (seçilen sunucunun FlashURL'ine göre embed) sayfalarını içerir.

## Özet

- Proje: DDTank Multiple Server For Test Login
- Platform / Dil: C# / ASP.NET Core (Razor Pages) - hedef framework: .NET 9
- Kimlik: ASP.NET Core Identity (EF Core ile)
- Veri: Entity Framework Core / SQL Server (DefaultConnection)
- Frontend: Razor Pages + CSS (`wwwroot/css/modern-theme.css`)

## Ana Özellikler

- Kayıt (Register)
  - Kullanıcı adı, e-posta (opsiyonel), şifre ile kayıt
  - Geliştirme için parola politikası gevşetilmiş durumda (örnek test hesapları: `test01/test01`)
  - Sunucu tarafı hata mesajları Türkçe (TurkishIdentityErrorDescriber ile)

- Giriş (Login)
  - Kullanıcı adı/şifre, "Beni Hatırla" seçeneği
  - Başarılı girişte varsayılan olarak `/Home` sayfasına yönlendirme (veya sağlanan `returnUrl` güvenliyse oraya)

- Dashboard (`/Home`)
  - Kullanıcıya hoşgeldin mesajı
  - `ServerInfos` tablosundan çekilen sunucular listelenir; her sunucu için bir buton bulunur
  - Butona tıklayınca Play sayfasına `serverId` ile yönlendirme olur

- Play (`/Play`)
  - Seçilen sunucunun `FlashURL` ve `ConfigURL` bilgileri kullanılarak Flash embed oluşturulur
  - Embed parametreleri: `Loading.swf?user={UserName}&key={Key}&config={ConfigUrl}`

## Veri Modeli (örnek)

Model dosyası: `Data/ServerInfo.cs`

Önerilen sütunlar:

- Id int (PK)
- ServerID int
- ServerName nvarchar(max)
- ServerVersion int
- ServerIP nvarchar(max)
- ServerPort nvarchar(max)
- FlashURL nvarchar(max)
- RequestURL nvarchar(max)
- ResourceURL nvarchar(max)
- ConfigURL nvarchar(max)

Örnek SQL (Sunucu eklemek için):

```sql
INSERT INTO ServerInfos (ServerID, ServerName, ServerVersion, ServerIP, ServerPort, FlashURL, RequestURL, ResourceURL, ConfigURL)
VALUES (1, 'Sihirler Şehri', 3400, '127.0.0.1', '443', 'http://127.0.0.1/Flash_3400_v01/', 'http://127.0.0.1/request', 'http://127.0.0.1/resources/', 'http://127.0.0.1/config.xml');
```

## Kurulum (Geliştirme)

### Gereksinimler

- .NET 9 SDK
- SQL Server (localdb veya tam SQL Server)
- `appsettings.json` içinde `DefaultConnection` bağlantı dizesi

### Adımlar

1. Repo'yu klonlayın veya kopyalayın.
2. Bağımlılıkları geri yükleyin ve projeyi derleyin:

```powershell
dotnet restore
dotnet build
```

3. (Gerekirse) EF migration oluşturun ve DB'yi güncelleyin:

```powershell
dotnet tool install --global dotnet-ef
dotnet ef migrations add Init
dotnet ef database update
```

4. Uygulamayı çalıştırın:

```powershell
dotnet run
```

5. Tarayıcıdan erişin:

- Kayıt: `http://localhost:5147/Identity/Account/Register`
- Giriş: `http://localhost:5147/Identity/Account/Login`
- Dashboard: `http://localhost:5147/Home`
- Play (örnek): `http://localhost:5147/Play?serverId=1`

## Notlar ve Öneriler

- Parola politikası geliştirme aşamasında gevşetilmiştir. Production ortamı için güçlü parola kuralları yeniden etkinleştirilmelidir.
- Flash embed modern tarayıcılarda çalışmayabilir; gerçek oyun oynatımı gerekiyorsa HTML5 tabanlı bir çözüme taşımayı düşünün.
- Eğer model değişiklikleri (ör. `ServerInfo`) yaptıysanız EF migration oluşturup uygulamanız gerekir.

## Proje Dosyalarında Önemli Yerler

- `Areas/Identity/Pages/Account/` — Login, Register, Logout
- `Pages/Home.cshtml` — Dashboard (sunucu seçimi)
- `Pages/Play.cshtml` — Play/Flash embed
- `Data/ServerInfo.cs`, `Data/ApplicationDbContext.cs` — DB model ve context
- `wwwroot/css/modern-theme.css` — Ana stil
- `Program.cs` — Identity konfigürasyonu

---

Bu README'yi repo köküne ekledim. Eğer istersen README'yi özelleştirebilirim (örneğin ekran görüntüleri, daha ayrıntılı migration adımları, ya da bir `seed` script ekleme). Hangi eklemeleri istersin?