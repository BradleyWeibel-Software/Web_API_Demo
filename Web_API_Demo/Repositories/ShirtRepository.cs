using Web_API_Demo.Data;
using Web_API_Demo.Model;

namespace Web_API_Demo.Repositories
{
    public class ShirtRepository
    {
        private readonly ApplicationDBContext database;

        public ShirtRepository(ApplicationDBContext db)
        {
            this.database = db;
        }

        #region GET

        public bool ShirtExists(int id) => database.Shirts.Any(i => i.Id == id);

        public List<Shirt> GetShirts() => database.Shirts.ToList();

        public Shirt? GetShirtById(int id) => database.Shirts.FirstOrDefault(s => s.Id == id);

        public Shirt? GetShirtByProperties(Shirt shirtData) => database.Shirts.FirstOrDefault(s =>
            !string.IsNullOrWhiteSpace(shirtData.Brand) &&
            s.Brand.ToLower() == shirtData.Brand.ToLower() &&
            !string.IsNullOrWhiteSpace(shirtData.Colour) &&
            s.Colour.ToLower() == shirtData.Colour.ToLower() &&
            shirtData.Size != 0 &&
            s.Size == shirtData.Size &&
            !string.IsNullOrWhiteSpace(shirtData.Sex) &&
            s.Sex.ToLower() == shirtData.Sex.ToLower());

        #endregion

        #region ADD

        public int? AddShirt(Shirt shirt)
        {
            if (shirt == null)
                return null;

            shirt.Id = null;
            database.Shirts.Add(shirt);
            database.SaveChanges();

            return shirt.Id;
        }

        #endregion

        #region UPDATE

        public bool UpdateShirt(Shirt shirtData)
        {
            var shirtToUpdate = GetShirtById((int)shirtData.Id);

            if (shirtToUpdate == null)
                return false;

            shirtToUpdate.Brand = shirtData.Brand;
            shirtToUpdate.Colour = shirtData.Colour;
            shirtToUpdate.Size = shirtData.Size;
            shirtToUpdate.Sex = shirtData.Sex;

            return database.SaveChanges() > 0;
        }

        #endregion

        #region DELETE

        public bool DeleteShirt(int id)
        {
            if (id <= 0)
                return false;

            var shirtToDelete = GetShirtById(id);

            if (shirtToDelete == null)
                return false;

            database.Shirts.Remove(shirtToDelete);

            return database.SaveChanges() > 0;
        }

        #endregion
    }
}
