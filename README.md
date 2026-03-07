# HOSPITAL LITE PROJECT
Projemizi inşa edeceğimiz klasik katmanlı mimarinin (N-Tier Architecture) katmanlar arası veri trafiği şu şekilde olacak:
`Presentation (API):` Sadece istekleri (Request) karşılar ve DTO döner.
`Business (Service):` İş kurallarının (Validation, Yetki, Hesaplama) döndüğü motor.
`DataAccess (Repository):` Veritabanı ile konuşan (EF Core) tek yer.
`Core (Entities):` Tüm katmanların bildiği, projenin kalbi (Merkezi kütüphane).