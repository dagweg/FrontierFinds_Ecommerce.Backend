using Ecommerce.Domain.Common.Entities;

namespace Ecommerce.Infrastructure;

public static partial class Seeding
{
  public static class Categories
  {
    #region Categories (Refactored - Removed Duplicates)
    public static Category Electronics = Category.Create(1, "Electronics", "electronics").Value;
    public static Category Fashion = Category.Create(2, "Fashion", "fashion").Value;
    public static Category HomeAndLiving = Category
      .Create(3, "Home and Living", "home-and-living")
      .Value;
    public static Category BooksAndStationary = Category
      .Create(4, "Books & Stationery", "books-stationery")
      .Value; // Renamed to Books & Stationery to encompass stationery
    public static Category SportsAndOutdoors = Category
      .Create(5, "Sports & Outdoors", "sports-and-outdoors")
      .Value;
    public static Category BeautyAndPersonalCare = Category
      .Create(6, "Beauty & Personal Care", "beauty-and-personal-care")
      .Value;
    public static Category ToysAndGames = Category
      .Create(7, "Toys & Games", "toys-and-games")
      .Value; // Top-level Toys & Games
    public static Category Grocery = Category.Create(8, "Grocery", "grocery").Value;
    public static Category Automotive = Category.Create(9, "Automotive", "automotive").Value;
    public static Category PetSupplies = Category.Create(10, "Pet Supplies", "pet-supplies").Value;
    public static Category OfficeAndBusiness = Category
      .Create(11, "Office & Business", "office-business")
      .Value;
    public static Category Entertainment = Category
      .Create(12, "Entertainment", "entertainment")
      .Value;
    public static Category TravelAndLuggage = Category
      .Create(13, "Travel & Luggage", "travel-luggage")
      .Value;
    public static Category JewelryAndWatches = Category
      .Create(14, "Jewelry & Watches", "jewelry-watches")
      .Value;
    public static Category DIYAndTools = Category.Create(15, "DIY & Tools", "diy-tools").Value;
    public static Category BabyProducts = Category
      .Create(16, "Baby Products", "baby-products")
      .Value; // Renamed from ToysAndBabyProducts to just BabyProducts - Toys are already top-level
    #endregion

    #region  Sub-Categories (Electronics)
    public static Category Electronics_MobilePhones = Category
      .Create(17, "Mobile Phones", "mobile-phones", Electronics)
      .Value;
    public static Category Electronics_Laptops = Category
      .Create(18, "Laptops", "laptops", Electronics)
      .Value;
    public static Category Electronics_Desktops = Category
      .Create(19, "Desktops", "desktops", Electronics)
      .Value;
    public static Category Electronics_Tablets = Category
      .Create(20, "Tablets", "tablets", Electronics)
      .Value;
    public static Category Electronics_TVs = Category.Create(21, "TVs", "tvs", Electronics).Value;
    public static Category Electronics_Audio = Category
      .Create(22, "Audio", "audio", Electronics)
      .Value;
    public static Category Electronics_Cameras = Category
      .Create(23, "Cameras", "cameras", Electronics)
      .Value;
    #endregion

    #region Sub-Categories (Fashion)
    public static Category Fashion_MensClothing = Category
      .Create(24, "Men's Clothing", "mens-clothing", Fashion)
      .Value;
    public static Category Fashion_WomensClothing = Category
      .Create(25, "Women's Clothing", "womens-clothing", Fashion)
      .Value;
    public static Category Fashion_KidsClothing = Category
      .Create(26, "Kids Clothing", "kids-clothing", Fashion)
      .Value;
    public static Category Fashion_Shoes = Category.Create(27, "Shoes", "shoes", Fashion).Value;
    public static Category Fashion_Accessories = Category
      .Create(28, "Accessories", "accessories", Fashion)
      .Value;
    #endregion

    #region Sub-Categories (Home and Living)
    public static Category HomeAndLiving_Furniture = Category
      .Create(29, "Furniture", "furniture", HomeAndLiving)
      .Value;
    public static Category HomeAndLiving_Decor = Category
      .Create(30, "Decor", "decor", HomeAndLiving)
      .Value;
    public static Category HomeAndLiving_KitchenAndDining = Category
      .Create(31, "Kitchen & Dining", "kitchen-dining", HomeAndLiving)
      .Value;
    public static Category HomeAndLiving_Bedding = Category
      .Create(32, "Bedding", "bedding", HomeAndLiving)
      .Value;
    public static Category HomeAndLiving_Bath = Category
      .Create(33, "Bath", "bath", HomeAndLiving)
      .Value;
    #endregion

    #region Sub-Categories (Books & Stationery)
    public static Category BooksAndStationery_FictionBooks = Category
      .Create(34, "Fiction Books", "fiction-books", BooksAndStationary)
      .Value;
    public static Category BooksAndStationery_NonFictionBooks = Category
      .Create(35, "Non-Fiction Books", "non-fiction-books", BooksAndStationary)
      .Value;
    public static Category BooksAndStationery_AcademicBooks = Category
      .Create(36, "Academic Books", "academic-books", BooksAndStationary)
      .Value;
    public static Category BooksAndStationery_StationerySupplies = Category
      .Create(37, "Stationery Supplies", "stationery-supplies", BooksAndStationary)
      .Value;
    public static Category BooksAndStationery_ArtAndCraftMaterials = Category
      .Create(38, "Art & Craft Materials", "art-craft-materials", BooksAndStationary)
      .Value;
    #endregion

    #region Sub-Categories (Toys & Games)
    public static Category ToysAndGames_ActionFigures = Category
      .Create(39, "Action Figures", "action-figures", ToysAndGames)
      .Value;
    public static Category ToysAndGames_BoardGames = Category
      .Create(40, "Board Games", "board-games", ToysAndGames) // Board Games under Toys & Games
      .Value;
    public static Category ToysAndGames_Puzzles = Category
      .Create(41, "Puzzles", "puzzles", ToysAndGames)
      .Value;
    public static Category ToysAndGames_EducationalToys = Category
      .Create(42, "Educational Toys", "educational-toys", ToysAndGames)
      .Value;
    #endregion

    #region Sub-Categories (Grocery)
    public static Category Grocery_PantryStaples = Category
      .Create(43, "Pantry Staples", "pantry-staples", Grocery)
      .Value;
    public static Category Grocery_Snacks = Category.Create(44, "Snacks", "snacks", Grocery).Value;
    public static Category Grocery_Beverages = Category
      .Create(45, "Beverages", "beverages", Grocery)
      .Value;
    public static Category Grocery_BakingSupplies = Category
      .Create(46, "Baking Supplies", "baking-supplies", Grocery)
      .Value;
    #endregion

    #region Sub-Categories (Automotive)
    public static Category Automotive_CarAccessories = Category
      .Create(47, "Car Accessories", "car-accessories", Automotive)
      .Value;
    public static Category Automotive_MotorbikeAccessories = Category
      .Create(48, "Motorbike Accessories", "motorbike-accessories", Automotive)
      .Value;
    public static Category Automotive_ToolsAndEquipment = Category
      .Create(49, "Tools & Equipment", "tools-equipment", Automotive)
      .Value;
    public static Category Automotive_TiresAndBatteries = Category
      .Create(50, "Tires & Batteries", "tires-batteries", Automotive)
      .Value;
    #endregion

    #region Sub-Categories (Pet Supplies)
    public static Category PetSupplies_PetFood = Category
      .Create(51, "Pet Food", "pet-food", PetSupplies)
      .Value;
    public static Category PetSupplies_PetAccessories = Category
      .Create(52, "Pet Accessories", "pet-accessories", PetSupplies)
      .Value;
    public static Category PetSupplies_PetCareProducts = Category
      .Create(53, "Pet Care Products", "pet-care-products", PetSupplies)
      .Value;
    #endregion

    #region Sub-Categories (Office & Business)
    public static Category OfficeAndBusiness_OfficeSupplies = Category
      .Create(54, "Office Supplies", "office-supplies", OfficeAndBusiness)
      .Value;
    public static Category OfficeAndBusiness_OfficeFurniture = Category
      .Create(55, "Office Furniture", "office-furniture", OfficeAndBusiness)
      .Value;
    public static Category OfficeAndBusiness_BusinessElectronics = Category
      .Create(56, "Business Electronics", "business-electronics", OfficeAndBusiness)
      .Value;
    #endregion

    #region Sub-Categories (Entertainment)
    public static Category Entertainment_MusicalInstruments = Category
      .Create(57, "Musical Instruments", "musical-instruments", Entertainment)
      .Value;
    public static Category Entertainment_GamingConsoles = Category
      .Create(58, "Gaming Consoles", "gaming-consoles", Entertainment)
      .Value;
    public static Category Entertainment_MovieDVDsAndBluRays = Category
      .Create(59, "Movie DVDs & Blu-rays", "movie-dvds-blu-rays", Entertainment)
      .Value;
    #endregion

    #region Sub-Categories (Travel & Luggage)
    public static Category TravelAndLuggage_SuitcasesAndBags = Category
      .Create(60, "Suitcases & Bags", "suitcases-bags", TravelAndLuggage)
      .Value;
    public static Category TravelAndLuggage_TravelAccessories = Category
      .Create(61, "Travel Accessories", "travel-accessories", TravelAndLuggage)
      .Value;
    public static Category TravelAndLuggage_Backpacks = Category
      .Create(62, "Backpacks", "backpacks", TravelAndLuggage)
      .Value;
    #endregion

    #region Sub-Categories (Jewelry & Watches)
    public static Category JewelryAndWatches_FineJewelry = Category
      .Create(63, "Fine Jewelry", "fine-jewelry", JewelryAndWatches)
      .Value;
    public static Category JewelryAndWatches_FashionJewelry = Category
      .Create(64, "Fashion Jewelry", "fashion-jewelry", JewelryAndWatches)
      .Value;
    public static Category JewelryAndWatches_LuxuryWatches = Category
      .Create(65, "Luxury Watches", "luxury-watches", JewelryAndWatches)
      .Value;
    public static Category JewelryAndWatches_CasualWatches = Category
      .Create(66, "Casual Watches", "casual-watches", JewelryAndWatches)
      .Value;
    #endregion

    #region Sub-Categories (DIY & Tools)
    public static Category DIYAndTools_HardwareTools = Category
      .Create(67, "Hardware Tools", "hardware-tools", DIYAndTools)
      .Value;
    public static Category DIYAndTools_GardeningEquipment = Category
      .Create(68, "Gardening Equipment", "gardening-equipment", DIYAndTools)
      .Value;
    public static Category DIYAndTools_ElectricalSupplies = Category
      .Create(69, "Electrical Supplies", "electrical-supplies", DIYAndTools)
      .Value;
    #endregion

    #region Sub-Categories (Baby Products - New Category)
    public static Category BabyProducts_BabyClothing = Category
      .Create(70, "Baby Clothing", "baby-clothing", BabyProducts)
      .Value;
    public static Category BabyProducts_BabyCareProducts = Category
      .Create(71, "Baby Care Products", "baby-care-products", BabyProducts)
      .Value;
    public static Category BabyProducts_StrollersAndCarSeats = Category
      .Create(72, "Strollers & Car Seats", "strollers-car-seats", BabyProducts)
      .Value;
    #endregion

    #region Sub-Categories (Sports & Outdoors - Refactored Camping & Hiking)
    public static Category SportsAndOutdoors_SportsEquipment = Category
      .Create(73, "Sports Equipment", "sports-equipment", SportsAndOutdoors)
      .Value;
    public static Category SportsAndOutdoors_OutdoorGear = Category
      .Create(74, "Outdoor Gear", "outdoor-gear", SportsAndOutdoors)
      .Value;
    public static Category SportsAndOutdoors_Activewear = Category
      .Create(75, "Activewear", "activewear", SportsAndOutdoors)
      .Value;
    public static Category SportsAndOutdoors_CampingAndHiking = Category // Simplified to Camping & Hiking
      .Create(76, "Camping & Hiking", "camping-hiking", SportsAndOutdoors)
      .Value;
    #endregion


    public static List<Category> GetSeed()
    {
      return
      [
        Electronics,
        Electronics_MobilePhones,
        Electronics_Laptops,
        Electronics_Desktops,
        Electronics_Tablets,
        Electronics_TVs,
        Electronics_Audio,
        Electronics_Cameras,
        Fashion,
        Fashion_MensClothing,
        Fashion_WomensClothing,
        Fashion_KidsClothing,
        Fashion_Shoes,
        Fashion_Accessories,
        HomeAndLiving,
        HomeAndLiving_Furniture,
        HomeAndLiving_Decor,
        HomeAndLiving_KitchenAndDining,
        HomeAndLiving_Bedding,
        HomeAndLiving_Bath,
        BooksAndStationary,
        BooksAndStationery_FictionBooks,
        BooksAndStationery_NonFictionBooks,
        BooksAndStationery_AcademicBooks,
        BooksAndStationery_StationerySupplies,
        BooksAndStationery_ArtAndCraftMaterials,
        SportsAndOutdoors,
        SportsAndOutdoors_SportsEquipment,
        SportsAndOutdoors_OutdoorGear,
        SportsAndOutdoors_Activewear,
        SportsAndOutdoors_CampingAndHiking, // Simplified category
        BeautyAndPersonalCare,
        ToysAndGames,
        ToysAndGames_ActionFigures,
        ToysAndGames_BoardGames,
        ToysAndGames_Puzzles,
        ToysAndGames_EducationalToys,
        Grocery,
        Grocery_PantryStaples,
        Grocery_Snacks,
        Grocery_Beverages,
        Grocery_BakingSupplies,
        Automotive,
        Automotive_CarAccessories,
        Automotive_MotorbikeAccessories,
        Automotive_ToolsAndEquipment,
        Automotive_TiresAndBatteries,
        PetSupplies,
        PetSupplies_PetFood,
        PetSupplies_PetAccessories,
        PetSupplies_PetCareProducts,
        OfficeAndBusiness,
        OfficeAndBusiness_OfficeSupplies,
        OfficeAndBusiness_OfficeFurniture,
        OfficeAndBusiness_BusinessElectronics,
        Entertainment,
        Entertainment_MusicalInstruments,
        Entertainment_GamingConsoles,
        Entertainment_MovieDVDsAndBluRays,
        TravelAndLuggage,
        TravelAndLuggage_SuitcasesAndBags,
        TravelAndLuggage_TravelAccessories,
        TravelAndLuggage_Backpacks,
        JewelryAndWatches,
        JewelryAndWatches_FineJewelry,
        JewelryAndWatches_FashionJewelry,
        JewelryAndWatches_LuxuryWatches,
        JewelryAndWatches_CasualWatches,
        DIYAndTools,
        DIYAndTools_HardwareTools,
        DIYAndTools_GardeningEquipment,
        DIYAndTools_ElectricalSupplies,
        BabyProducts, // New Top-Level Baby Products
        BabyProducts_BabyClothing,
        BabyProducts_BabyCareProducts,
        BabyProducts_StrollersAndCarSeats,
      ];
    }
  }
}
