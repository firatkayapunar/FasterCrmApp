<h1 align="center"> FasterCrmApp - CRM Yönetim Sistemi</h1>

**FasterCrmApp**, küçük işletmelerin müşteri ilişkilerini yönetmesini sağlayan bir CRM (Customer Relationship Management) çözümüdür. Kullanıcı yönetimi, müşteri takibi, görev atama ve bildirim sistemini destekleyen modüller içermektedir.

Bu proje, kiralanabilir bir yapıya dönüştürülerek daha modüler hale getirilebilir. Ancak, bu projede öncelikli hedefim temel bir CRM sistemi geliştirmek olduğu için bu yönü detaylandırmadım.

Aslında, React ve API ile birlikte geliştirmek daha modern ve ölçeklenebilir bir yapı sunardı, ancak ben klasik yaklaşımı tercih ettim.

---

## 🚀 **Proje Özeti**
Bu proje, **müşteri, kullanıcı, talep ve görev yönetimini** merkezi bir sistemde toplayarak iş süreçlerini verimli hale getirmeyi hedeflemektedir.

✅ **Öne Çıkan Özellikler:**
- Kullanıcı yönetimi (Rol bazlı erişim kontrolü)
- Müşteri bilgileri ve durum yönetimi
- Talep yönetimi ve durum takibi
- Görev oluşturma ve atama
- Bildirim sistemi

---

## 🔧 **Kurulum & Veritabanı Ayarları**

<ol>
  <li>
    <strong>Veritabanı migrasyonlarını çalıştırın:</strong>
    <pre><code>Update-Database</code></pre>
    <p>Bu komut, <strong>Package Manager Console</strong> üzerinden çalıştırılmalıdır.</p>
  </li>
  <li>
    <strong>Örnek verileri yüklemek için SQL scriptlerini çalıştırın:</strong>
    <ul>
      <li><code>ClientsInsertScript.sql</code></li>
      <li><code>IssuesInsertScript.sql</code></li>
      <li><code>LeadsInsertScript.sql</code></li>
      <li><code>UsersInsertScript.sql</code></li>
    </ul>
  </li>
</ol>

<br>
 ✅ <strong>Bu adımları takip ederek veritabanınızı hazır hale getirebilirsiniz.</strong>

---

## 📌 **Proje Ekran Görüntüleri**

### Kullanıcı Yönetimi (Admin)
![5](https://github.com/user-attachments/assets/4d639d60-d5c3-453b-8de5-3ea6535073a4)

### Talepler
![1](https://github.com/user-attachments/assets/dfd29909-ed99-4216-a010-6e695ee36424)
![6](https://github.com/user-attachments/assets/a7211bf7-928c-401f-96b8-e8f30d6373a5)

### Görev Yönetimi
![2](https://github.com/user-attachments/assets/50457275-8f7a-4e5d-829d-2ff798f58e81)

### Bildirimler
![3](https://github.com/user-attachments/assets/d4508bee-6d45-4f97-8900-fdc93c52b70f)

### Müşteri Yönetimi
![1](https://github.com/user-attachments/assets/27c390b9-055c-43b5-b4d6-6535bd6b1962)

---

<h2>📂 Proje Yapısı</h2>

<pre>
📦 FasterCrmApp
│
├── 📄 FasterCrmApp.Common
│   ├── Dependencies
│   ├── Constants.cs
│
├── 📄 FasterCrmApp.DataAccess
│   ├── Dependencies
│   ├── 📂 Abstract
│   │   ├── 📂 Base
│   │   │   ├── ICommandRepository.cs
│   │   │   ├── IQueryRepository.cs
│   │   │   ├── IRepository.cs
│   │   ├── IClientRepository.cs
│   │   ├── IIssueRepository.cs
│   │   ├── ILeadRepository.cs
│   │   ├── ILogRepository.cs
│   │   ├── INotificationRepository.cs
│   │   ├── IUserRepository.cs
│   ├── 📂 Concrete
│   │   ├── 📂 EntityFramework
│   │   │   ├── 📂 Base
│   │   │   │   ├── EfBaseRepository.cs
│   │   │   ├── 📂 Context
│   │   │   │   ├── DatabaseContext.cs
│   │   │   │   ├── DesignTimeDbContextFactory.cs
│   │   │   ├── EfClientRepository.cs
│   │   │   ├── EfIssueRepository.cs
│   │   │   ├── EfLeadRepository.cs
│   │   │   ├── EfLogRepository.cs
│   │   │   ├── EfNotificationRepository.cs
│   │   │   ├── EfUserRepository.cs
│   ├── 📂 Migrations
│   │   ├── 20250225202215_InitialCreate.cs
│   │   ├── DatabaseContextModelSnapshot.cs
│   ├── 📂 Scripts
│   │   ├── ClientsInsertScript.sql
│   │   ├── IssuesInsertScript.sql
│   │   ├── LeadsInsertScript.sql
│   │   ├── UsersInsertScript.sql
│   ├── 📂ServiceCollectionExtension.cs
│   │   ├── DependencyInjection.cs
│
├── 📄 FasterCrmApp.Entities
│   ├── Dependencies
│   ├── 📂 Abstract
│   │   ├── EntityBase.cs
│   ├── 📂 Concrete
│   │   ├── Client.cs
│   │   ├── Issue.cs
│   │   ├── Lead.cs
│   │   ├── Log.cs
│   │   ├── Notification.cs
│   │   ├── User.cs
│   ├── 📂 Enum
│   │   ├── LeadType.cs
│   │   ├── LogType.cs
│   │   ├── NotificationType.cs
│   │   ├── Role.cs
│
├── 📄 FasterCrmApp.Models
│   ├── Dependencies
│   ├── 📂 Results
│   │   ├── Result.cs
│   ├── ClientModels.cs
│   ├── IssueModels.cs
│   ├── LeadModels.cs
│   ├── LogModels.cs
│   ├── NotificationModels.cs
│   ├── UserModels.cs
│
├── 📄 FasterCrmApp.Services
│    ├── Dependencies
│    ├── 📂 Abstract
│    │   ├── IClientService.cs
│    │   ├── IIssueService.cs
│    │   ├── ILeadService.cs
│    │   ├── ILogService.cs
│    │   ├── INotificationService.cs
│    │   ├── IUserService.cs
│    ├── 📂 Concrete
│    │   ├── ClientService.cs
│    │   ├── IssueService.cs
│    │   ├── LeadService.cs
│    │   ├── LogService.cs
│    │   ├── NotificationService.cs
│    │   ├── UserService.cs
│    ├── 📂 Helper
│    │   ├── LeadTypeHelper.cs
│    │   ├── RoleHelper.cs
│    ├── 📂 Profiles
│    │   ├── ClientProfile.cs
│    │   ├── IssueProfile.cs
│    │   ├── LeadProfile.cs
│    │   ├── LogProfile.cs
│    │   ├── NotificationProfile.cs
│    │   ├── UserProfile.cs
│    ├── 📂 ServiceCollectionExtension
│    │   ├── DependencyInjection.cs
│    ├── 📂 Validation
│    │   ├── 📂 FluentValidation
│    │   │   ├── ClientValidator.cs
│    │   │   ├── IssueValidator.cs
│    │   │   ├── LeadValidator.cs
│    │   │   ├── LogValidator.cs
│    │   │   ├── NotificationValidator.cs
│    │   │   ├── UserValidator.cs
│    │   ├── CustomValidationException.cs
│    │   ├── ValidationTool.cs
│  
├── 📄 FasterCrmApp.UI
│   ├── Connected Services
│   ├── Dependencies
│   ├── Properties
│   ├── wwwroot
│   ├── 📂 Controllers
│   │   ├── AccountController.cs
│   │   ├── ClientController.cs
│   │   ├── ComponentController.cs
│   │   ├── ControllerBase.cs
│   │   ├── IssueController.cs
│   │   ├── LeadController.cs
│   │   ├── NotificationController.cs
│   │   ├── UserController.cs
│   ├── 📂 Filter
│   │   ├── AuthAttribute.cs
│   ├── 📂 Models
│   │   ├── BaseModalViewModel.cs
│   │   ├── ClientModalInputViewModel.cs
│   │   ├── ClientModalViewModel.cs
│   │   ├── IssueModalInputViewModel.cs
│   │   ├── IssueModalViewModel.cs
│   │   ├── LeadModalInputViewModel.cs
│   │   ├── LeadModalViewModel.cs
│   │   ├── ModalEnum.cs
│   │   ├── ModalInputViewModel.cs
│   │   ├── NavbarViewModel.cs
│   │   ├── UserModalViewModel.cs
│   ├── 📂 ViewComponents
│   │   ├── NavbarViewComponent.cs
│   ├── 📂 Views
│   │   ├── 📂 Account
│   │   │   ├── Login.cshtml
│   │   ├── 📂 Client
│   │   │   ├── _DynamicModal.cshtml
│   │   │   ├── _ModalInputs.cshtml
│   │   │   ├── Index.cshtml
│   │   ├── 📂 Issue
│   │   │   ├── _DynamicModal.cshtml
│   │   │   ├── _ModalInputs.cshtml
│   │   │   ├── Index.cshtml
│   │   ├── 📂 Lead
│   │   │   ├── _DynamicModal.cshtml
│   │   │   ├── _ModalInputs.cshtml
│   │   │   ├── Index.cshtml
│   │   ├── 📂 Notification
│   │   │   ├── Index.cshtml
│   │   ├── 📂 Shared
│   │   │   ├── 📂 Components
│   │   │   │   ├── 📂 Navbar
│   │   │   │   │   ├── Default.cshtml
│   │   │   ├── _Layout.cshtml
│   │   │   ├── _LayoutEmpty.cshtml
│   │   │   ├── _ValidationScriptsPartial.cshtml
│   │   ├── _ViewImports.cshtml
│   │   ├── _ViewStart.cshtml
│   │   ├── 📂 User
│   │   │   ├── _DynamicModal.cshtml
│   │   │   ├── Index.cshtml
│   ├── appsettings.json
│   ├── bundleconfig.json
│   ├── libman.json
│   ├── Program.cs 
</pre>

---

<h2>🛠️ Kullanılan Teknolojiler ve Mimariler</h2>

<ul> 
  <li><strong>.NET 8</strong> (ASP.NET Core MVC)</li> 
  <li><strong>Entity Framework Core</strong></li> 
  <li><strong>N-Tier Architecture</strong></li> 
  <li><strong>jQuery</strong> ( Frontend etkileşimleri ve dinamik içerik yönetim)</li> 
  <li><strong>MSSQL</strong> (Veritabanı)</li> 
</ul>

---

<p>
  <h3>Not:</h3>
  Yakın zamanda React ve ASP.NET Core API kullanarak temel bir e-ticaret projesine başlamayı planlıyorum.
</p>
