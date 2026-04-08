using Web_API_Demo.Model;

namespace Web_API_Demo.Repositories
{
    public class ShirtRepository
    {
        private static List<Shirt> shirts = new List<Shirt>()
        {
            new Shirt() { Id = 1, Brand = "Nike", Colour = "Black", Size = 10, Sex = "Female" },
            new Shirt() { Id = 2, Brand = "Puma", Colour = "Red", Size = 12, Sex = "Male" },
            new Shirt() { Id = 3, Brand = "RHCP", Colour = "Grey", Size = 10, Sex = "Female" },
            new Shirt() { Id = 4, Brand = "TBK", Colour = "Pink", Size = 6, Sex = "Female" },
            new Shirt() { Id = 5, Brand = "TBS", Colour = "Black", Size = 8, Sex = "Male" },
            new Shirt() { Id = 6, Brand = "DC", Colour = "White", Size = 11, Sex = "Male" },
            new Shirt() { Id = 7, Brand = "Adidas", Colour = "Grey", Size = 10, Sex = "Female" },
            new Shirt() { Id = 8, Brand = "[no-name]", Colour = "White", Size = 12, Sex = "Male" },
            new Shirt() { Id = 9, Brand = "Nike", Colour = "Gold", Size = 10, Sex = "Female" },
            new Shirt() { Id = 10, Brand = "RHCP", Colour = "Red", Size = 10, Sex = "Female" }
        };

        #region GET

        public static bool ShirtExists(int id) => shirts.Any(i => i.Id == id);

        public static List<Shirt> GetShirts() => shirts;

        public static Shirt? GetShirtById(int id) => shirts.FirstOrDefault(s => s.Id == id);

        public static Shirt? GetShirtByProperties(Shirt shirtData) => shirts.FirstOrDefault(s =>
            !string.IsNullOrWhiteSpace(shirtData.Brand) &&
            s.Brand.Equals(shirtData?.Brand, StringComparison.OrdinalIgnoreCase) &&
            !string.IsNullOrWhiteSpace(shirtData.Colour) &&
            s.Colour.Equals(shirtData?.Colour, StringComparison.OrdinalIgnoreCase) &&
            shirtData.Size != 0 &&
            s.Size == shirtData?.Size &&
            !string.IsNullOrWhiteSpace(shirtData.Sex) &&
            s.Sex.Equals(shirtData.Sex, StringComparison.OrdinalIgnoreCase));

        #endregion

        #region ADD

        public static int? AddShirt(Shirt shirt)
        {
            if (shirt == null)
                return null;

            int maxId = shirts.Max(s => s.Id) ?? 0;
            shirt.Id = maxId + 1;

            shirts.Add(shirt);

            return shirt.Id;
        }

        #endregion

        #region UPDATE

        public static bool UpdateShirt(Shirt shirtData)
        {
            var shirtToUpdate = GetShirtById((int)shirtData.Id);

            if (shirtToUpdate == null)
                return false;

            shirtToUpdate.Brand = shirtData.Brand;
            shirtToUpdate.Colour = shirtData.Colour;
            shirtToUpdate.Size = shirtData.Size;
            shirtToUpdate.Sex = shirtData.Sex;

            return true;
        }

        #endregion

        #region DELETE

        public static bool DeleteShirt(int id)
        {
            if (id <= 0)
                return false;

            var shirtToDelete = GetShirtById(id);

            if (shirtToDelete == null)
                return false;

            return shirts.Remove(shirtToDelete);
        }

        #endregion
    }
}
