

// BU DOSYA, ÇEŞİTLİ YETKİLERE SAHİP OLAN KULLANICILARIN YAPABİLECEĞİ İŞLEMLERLE İLGİLİ KISITLAMALARIN YAPILDIĞI YER.

using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace MultiShop.IdentityServer
{
    public static class Config
    {

        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            // her bir mikroservis için o mikroservislere erişmek için key belirleyeceğimiz yer.
            new ApiResource("ResourceCatalog")
            {
                Scopes = {"CatalogFullPermission","CatalogReadPermission"}
            },
            new ApiResource("ResourceDiscount")
            {
                Scopes = {"DiscountFullPermission"}
            },
            new ApiResource("ResourceOrder")
            {
                Scopes = {"OrderFullPermission"}
            },
            new ApiResource("ResourceCargo")
            {
                Scopes = {"CargoFullPermission"}
            },

            new ApiResource("ResourceBasket")
            {
                Scopes = {"BasketFullPermission"}
            },

            new ApiResource("ResourceComment")
            {
                Scopes = {"CommentFullPermission", "CommentReadPermission" }
            },

            new ApiResource("ResourcePayment")
            {
                Scopes = {"PaymentFullPermission"}
            },

            new ApiResource("ResourceImages")
            {
                Scopes = {"ImagesFullPermission"}
            },

            new ApiResource("ResourceMessage")
            {
                Scopes = {"MessageFullPermission"}
            },

             new ApiResource("ResourceOcelot")
            {
                Scopes = {"OcelotFullPermission"}
            },



            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)

        };

        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
            // identity kaynağına sahip olan kişi hangi değerlere erişim sağlicak.

            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };



        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            new ApiScope("CatalogFullPermission","Katalog İşlemleri için Full Yetki"),
            new ApiScope("CatalogReadPermission","Katalog İşlemleri için Okumaya Yetkisi"),

            new ApiScope("DiscountFullPermission","İndirim İşlemleri için Full Yetki"),

            new ApiScope("OrderFullPermission","Sipariş İşlemleri için Full Yetki"),

            new ApiScope("CargoFullPermission","Kargo İşlemleri için Full Yetki"),

            new ApiScope("BasketFullPermission","Sepet İşlemleri için Full Yetki"),

            new ApiScope("CommentFullPermission","Yorum İşlemleri için Full Yetki"),
            new ApiScope("CommentReadPermission","Yorum İşlemleri için Okumaya Yetki"),

            new ApiScope("PaymentFullPermission","Ödeme İşlemleri için Full Yetki"),

            new ApiScope("ImagesFullPermission","Görsel İşlemleri için Full Yetki"),

            new ApiScope("MessageFullPermission","Mesaj İşlemleri için Full Yetki"),

            new ApiScope("OcelotFullPermission","Ocelot İşlemleri için Full Yetki"),

            new ApiScope(IdentityServerConstants.LocalApi.ScopeName)

        };


        public static IEnumerable<Client> Clients => new Client[]
        {
            //Visitor
            new Client
            {
                ClientId = "MultiShopVisitorID",
                ClientName = "MultiShop Visitor User",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = {new Secret("multishopsecret".Sha256())},
                AllowedScopes = { "CatalogFullPermission", "CatalogReadPermission" , "OcelotFullPermission" , "CommentReadPermission", "CommentFullPermission", "ImagesFullPermission" }
            },

            //Manager
            new Client
            {
                ClientId = "MultiShopManagerID",
                ClientName = "MultiShop Manager User",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = {new Secret("multishopsecret".Sha256())},
                AllowedScopes = { "CatalogFullPermission" , "CatalogReadPermission" , "BasketFullPermission" , "OcelotFullPermission" , "CommentFullPermission" ,
                    "PaymentFullPermission" , "ImagesFullPermission","DiscountFullPermission","OrderFullPermission","MessageFullPermission","CargoFullPermission",
                    "CommentReadPermission",
                IdentityServerConstants.LocalApi.ScopeName,
                IdentityServerConstants.StandardScopes.Email,
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile
                }
            },

            //Admin
            new Client
            {
                ClientId = "MultiShopAdminID",
                ClientName = "MultiShop Admin User",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = {new Secret("multishopsecret".Sha256())},
                AllowedScopes = { "CatalogFullPermission" , "CatalogReadPermission" , "DiscountFullPermission" , "OrderFullPermission", "CargoFullPermission" , "BasketFullPermission", "OcelotFullPermission",
                    "CommentFullPermission" , "PaymentFullPermission" , "ImagesFullPermission","CargoFullPermission","CommentReadPermission",
                IdentityServerConstants.LocalApi.ScopeName,
                IdentityServerConstants.StandardScopes.Email,
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile
                },
                AccessTokenLifetime = 3600
            }
        };

    }
}