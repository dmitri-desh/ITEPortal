using ITEPortal.Data.Models;

namespace ITEPortal.Data.Persistence
{
    public class DbInitializer
    {
        public async static void Initialize(ApplicationContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Set<User>().Any() && !context.Set<Exhibition>().Any())
            {
                var user = new User
                {
                    Name = "Exhibitor",
                    Email = "itportal@mailforspam.com",
                    Role = Contracts.Enums.Role.Exhibitor,
                };

                var exhibitor = new Exhibitor
                {
                    User = user,
                };

                var exhibition = new Exhibition
                {
                    Name = "Aquatherm Moscow 2023",
                    LogoUrl = "URL",
                    Description = "Международная выставка для отопления, водоснабжения, инженерно-сантехнических систем, бассейнов, саун и спа.",
                    StartDate = DateTimeOffset.UtcNow,
                    EndDate = DateTimeOffset.UtcNow,
                    Place = "Москва, «Крокус ЭКСПО»",
                    Exhibitors = new List<Exhibitor>() { exhibitor },
                };

                var stand = new Stand
                {
                    Area = 55.00m,
                    BuildingType = Contracts.Enums.BuildingType.Optima,
                    StandConfiguration = Contracts.Enums.StandConfiguration.ISL,
                    SecondFloorArea = 33.00m,
                    StandNumber = 1,
                };

                exhibition.Stands.Add(stand);
                exhibitor.Stands.Add(stand);

                await context.Set<Exhibitor>().AddAsync(exhibitor);
                await context.Set<Exhibition>().AddAsync(exhibition);
            }

            if (!context.Set<Category>().Any())
            {
                //core categories
                var selectBuildingCategory = new Category { Name = "select-building" };
                var cleaninCategory = new Category { Name = "cleaning" };
                var autoCardCategory = new Category { Name = "auto-card" };
                var waterCategory = new Category { Name = "water" };
                var telecommunicationCategory = new Category { Name = "telecommunication" };
                var graphicWorksCategory = new Category { Name = "graphic-works" };
                var airCategory = new Category { Name = "air" };
                var tvRentCategory = new Category { Name = "tv-rent" };
                var temporaryStaffCategory = new Category { Name = "temporary-staff" };
                var powerCategory = new Category { Name = "power" };

                //parent categories
                var standDesignCategory = new Category { Name = "stand-design" };
                var furnitureCategory = new Category { Name = "furniture" };
                var socketLightsCategory = new Category { Name = "sockets-lights" };

                //nested categories
                var carpetCoveringCategory = new Category { Name = "carpet-covering", ParentCategory = standDesignCategory };
                var wallPanelsCategory = new Category { Name = "wall-panels", ParentCategory = standDesignCategory };
                var doorsFencesCategory = new Category { Name = "doors-fences", ParentCategory = standDesignCategory };
                var otherConstructiveCategory = new Category { Name = "other-constructive", ParentCategory = standDesignCategory };
                var chairCategory = new Category { Name = "chair", ParentCategory = furnitureCategory };
                var tableCategory = new Category { Name = "table", ParentCategory = furnitureCategory };
                var rackCategory = new Category { Name = "rack", ParentCategory = furnitureCategory };
                var racksCabinetsShelvesCategory = new Category { Name = "racks-cabinets-shelves", ParentCategory = furnitureCategory };
                var showCaseCategory = new Category { Name = "showcase", ParentCategory = furnitureCategory };
                var podiumCategory = new Category { Name = "podium", ParentCategory = furnitureCategory };
                var otherFurnitureCategory = new Category { Name = "other-furniture", ParentCategory = furnitureCategory };
                var kitcheEquipmentCategory = new Category { Name = "kitchen-equipment", ParentCategory = furnitureCategory };
                var socketsCategory = new Category { Name = "sockets", ParentCategory = socketLightsCategory };
                var lightingCategory = new Category { Name = "lighting", ParentCategory = socketLightsCategory };

                var categoryList = new List<Category>()
                {
                 selectBuildingCategory,cleaninCategory,autoCardCategory,waterCategory,telecommunicationCategory,graphicWorksCategory,
                 airCategory,tvRentCategory,temporaryStaffCategory,powerCategory,standDesignCategory,furnitureCategory,
                 socketLightsCategory,carpetCoveringCategory,wallPanelsCategory,doorsFencesCategory,otherConstructiveCategory,chairCategory,
                 tableCategory,rackCategory,racksCabinetsShelvesCategory,showCaseCategory,podiumCategory,otherFurnitureCategory,
                 kitcheEquipmentCategory,socketsCategory, lightingCategory
                };

                await context.Set<Category>().AddRangeAsync(categoryList);

                if (!context.Set<Product>().Any())
                {
                    var products = new Product[]
                    {
                        new Product{ Name = "standard",Category = selectBuildingCategory},
                        new Product{ Name = "premium_verona",Category = selectBuildingCategory},
                        new Product{ Name = "premium_ferrara",Category = selectBuildingCategory},
                        new Product{ Name = "premium_ravenna",Category = selectBuildingCategory},
                        new Product{ Name = "premium_cremona",Category = selectBuildingCategory},
                        new Product{ Name = "premium_ancona",Category = selectBuildingCategory},
                        new Product{ Name = "premium_fabriano",Category = selectBuildingCategory},
                        new Product{ Name = "premium_livorno",Category = selectBuildingCategory},

                        new Product{ Name = "vacuum_cleaner",Category=cleaninCategory},
                        new Product{ Name = "wet_cleaning",Category=cleaninCategory},

                        new Product{Name="auto_1",Category=autoCardCategory},
                        new Product{Name="auto_2",Category=autoCardCategory},
                        new Product{Name="auto_3",Category=autoCardCategory},

                        new Product{ Name = "water_1",Category=waterCategory},
                        new Product{ Name = "water_2",Category=waterCategory},
                        new Product{ Name = "water_3",Category=waterCategory},
                        new Product{ Name = "water_4",Category=waterCategory},

                        new Product{ Name="tv_1",Category=telecommunicationCategory},
                        new Product{ Name="tv_2",Category=telecommunicationCategory},
                        new Product{ Name="tv_3",Description="Интернет-подключение проводное 100 Мбит/с",Category=telecommunicationCategory},
                        new Product{ Name="tv_4",Category=telecommunicationCategory},
                        new Product{ Name="tv_5",Description="Дополнительная точка доступа Wi-Fi (только при заказе интернет-подключения)",Category=telecommunicationCategory},
                        new Product{ Name="tv_6",Category=telecommunicationCategory},

                        new Product{ Name="graphic_1",Description="Дополнительная надпись на фризовой панели",Category=graphicWorksCategory},
                        new Product{ Name="graphic_2",Category=graphicWorksCategory},
                        new Product{ Name="graphic_3",Description="Логотип на фризовой панели, многоцветный",Category=graphicWorksCategory},
                        new Product{ Name="graphic_4",Category=graphicWorksCategory},
                        new Product{ Name="graphic_5",Category=graphicWorksCategory},
                        new Product{ Name="graphic_6",Description="Ламинирование цветной пленкой ORACAL",Category=graphicWorksCategory},
                        new Product{ Name="graphic_7",Description="Полноцветная печать на пленке, включая оклейку",Category=graphicWorksCategory},
                        new Product{ Name="graphic_8",Description="Полноцветная печать на баннере, включая монтаж",Category=graphicWorksCategory},
                        new Product{ Name="air_1",Category=graphicWorksCategory},
                        new Product{ Name="air_2",Category=graphicWorksCategory},

                        new Product{ Name="tv_rent_1",Category=tvRentCategory},
                        new Product{ Name="tv_rent_2",Category=tvRentCategory},
                        new Product{ Name="tv_rent_3",Category=tvRentCategory},
                        new Product{ Name="tv_rent_4",Category=tvRentCategory},
                        new Product{ Name="tv_rent_5",Category=tvRentCategory},

                        new Product{ Name = "staff_1",Category = temporaryStaffCategory},
                        new Product{ Name = "staff_2",Category = temporaryStaffCategory},
                        new Product{ Name = "staff_3",Category = temporaryStaffCategory},
                        new Product{ Name = "staff_4",Category = temporaryStaffCategory},
                        new Product{ Name = "staff_5",Category = temporaryStaffCategory},

                        new Product{ Name="power_1",Category=powerCategory},
                        new Product{ Name="power_2",Category=powerCategory},
                        new Product{ Name="power_3",Category=powerCategory},
                        new Product{ Name="power_4",Category=powerCategory},
                        new Product{ Name="power_5",Category=powerCategory},

                        new Product{ Name="covering_1",Category=carpetCoveringCategory},

                        new Product{ Name="wall_panels_1",Category=wallPanelsCategory},
                        new Product{ Name="wall_panels_2",Category=wallPanelsCategory},
                        new Product{ Name="wall_panels_3",Category=wallPanelsCategory},
                        new Product{ Name="wall_panels_4",Category=wallPanelsCategory},
                        new Product{ Name="wall_panels_5",Category=wallPanelsCategory},
                        new Product{ Name="wall_panels_6",Category=wallPanelsCategory},
                        new Product{ Name="wall_panels_7",Category=wallPanelsCategory},
                        new Product{ Name="wall_panels_8",Category=wallPanelsCategory},

                        new Product{ Name="doors_fences_1",Category=doorsFencesCategory},
                        new Product{ Name="doors_fences_2",Category=doorsFencesCategory},
                        new Product{ Name="doors_fences_3",Category=doorsFencesCategory},
                        new Product{ Name="doors_fences_4",Category=doorsFencesCategory},
                        new Product{ Name="doors_fences_5",Category=doorsFencesCategory},

                        new Product{ Name="other_constructive_1",Category=otherConstructiveCategory},
                        new Product{ Name="other_constructive_2",Category=otherConstructiveCategory},
                        new Product{ Name="other_constructive_3",Category=otherConstructiveCategory},
                        new Product{ Name="other_constructive_4",Category=otherConstructiveCategory},

                        new Product{ Name="chair_1",Category=chairCategory},
                        new Product{ Name="chair_2",Category=chairCategory},
                        new Product{ Name="chair_3",Category=chairCategory},
                        new Product{ Name="chair_4",Category=chairCategory},

                        new Product{ Name="table_1",Category=tableCategory},
                        new Product{ Name="table_2",Category=tableCategory},
                        new Product{ Name="table_3",Category=tableCategory},
                        new Product{ Name="table_4",Category=tableCategory},

                        new Product{ Name="rack_1",Category=rackCategory},
                        new Product{ Name="rack_2",Category=rackCategory},
                        new Product{ Name="rack_3",Category=rackCategory},
                        new Product{ Name="rack_4",Category=rackCategory},
                        new Product{ Name="rack_5",Category=rackCategory},

                        new Product{ Name="wardrobe_1",Category=racksCabinetsShelvesCategory},
                        new Product{ Name="wardrobe_2",Category=racksCabinetsShelvesCategory},
                        new Product{ Name="wardrobe_3",Category =racksCabinetsShelvesCategory},
                        new Product{ Name="wardrobe_4",Category=racksCabinetsShelvesCategory},
                        new Product{ Name="wardrobe_5",Category=racksCabinetsShelvesCategory},
                        new Product{ Name="wardrobe_6",Category=racksCabinetsShelvesCategory},
                        new Product{ Name="wardrobe_7",Category=racksCabinetsShelvesCategory},
                        new Product{ Name="wardrobe_8",Category=racksCabinetsShelvesCategory},

                        new Product{ Name="showcase_1",Category=showCaseCategory},
                        new Product{ Name="showcase_2",Category=showCaseCategory},
                        new Product{ Name="showcase_3",Category=showCaseCategory},
                        new Product{ Name="showcase_4",Category=showCaseCategory},
                        new Product{ Name="showcase_5",Category=showCaseCategory},
                        new Product{ Name="showcase_6",Category=showCaseCategory},
                        new Product{ Name="showcase_7",Category=showCaseCategory},

                        new Product{ Name="podium_1",Category=podiumCategory},
                        new Product{ Name="podium_2",Category=podiumCategory},
                        new Product{ Name="podium_3",Category=podiumCategory},
                        new Product{ Name="podium_4",Category=podiumCategory},
                        new Product{ Name="podium_5",Category=podiumCategory},
                        new Product{ Name="podium_6",Category=podiumCategory},
                        new Product{ Name="podium_7",Category=podiumCategory},

                        new Product{ Name="other_furniture_1",Category=otherFurnitureCategory},
                        new Product{ Name="other_furniture_2",Category=otherFurnitureCategory},
                        new Product{ Name="other_furniture_3",Category=otherFurnitureCategory},
                        new Product{ Name="other_furniture_4",Category=otherFurnitureCategory},

                        new Product{ Name="kitchen_1",Category=kitcheEquipmentCategory},
                        new Product{ Name="kitchen_2",Category=kitcheEquipmentCategory},
                        new Product{ Name="kitchen_3",Category=kitcheEquipmentCategory},
                        new Product{ Name="kitchen_4",Category=kitcheEquipmentCategory},
                        new Product{ Name="kitchen_5",Category=kitcheEquipmentCategory},
                        new Product{ Name="kitchen_6",Category=kitcheEquipmentCategory},

                        new Product{ Name="sockets_1",Category=socketsCategory},
                        new Product{ Name="sockets_2",Category=socketsCategory},
                        new Product{ Name="sockets_3",Category=socketsCategory},
                        new Product{ Name="sockets_4",Category=socketsCategory},
                        new Product{ Name="sockets_5",Category=socketsCategory},

                        new Product{ Name="lighting_1",Category=lightingCategory},
                        new Product{ Name="lighting_2",Category=lightingCategory},
                        new Product{ Name="lighting_3",Category=lightingCategory},
                        new Product{ Name="lighting_4",Category=lightingCategory},
                        new Product{ Name="lighting_5",Category=lightingCategory},
                        new Product{ Name="lighting_6",Category=lightingCategory},
                        new Product{ Name="lighting_7",Category=lightingCategory},
                    };

                    await context.Set<Product>().AddRangeAsync(products);
                };
            };

            context.SaveChanges();
        }
    }
}