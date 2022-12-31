namespace AirBnb1.Models
{
    public class Flat
    {
        
        double price;
        int id;
        double discount = 0.9;

        private static List<Flat> Flatlist = new List<Flat>();
        public int Id { get => id; set => id = value; }
        public string City { get; set; }
        public string Address { get; set; }
        public int NumberOfRooms { get; set; }

        public double Price { get => price; set => price = Discount(value); }

        public static List<Flat> Read()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadFlats();
        }

        public int Insert()
        {

            DBservices dbs = new DBservices();
            return dbs.InsertFlat(this);

            //try
            //{
            //    foreach (Flat item in Flatlist)
            //    {
            //        if (item.id == id)
            //            return false;
            //    }
            //    Flatlist.Add(this);
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("Didn't succeed inserting " + ex.Message);
            //}
            //return true;
        }

        public double Discount( double Sprice)
        { 
            if (NumberOfRooms>1 && Sprice >100)
            {
                return Sprice * discount;
            }
            else
                return Sprice;
        }


        public List<Flat> GetFlatsByCTnPC(string ct, double pc)
        {
            List<Flat> FlatsByPC = new List<Flat>();
            foreach (Flat f in Flatlist)
            {
                if(f.City == ct && f.Price< pc)
                    FlatsByPC.Add(f);
            }
            return FlatsByPC;
        }

    }
}
