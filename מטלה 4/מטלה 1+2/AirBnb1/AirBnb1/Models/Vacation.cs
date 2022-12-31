namespace AirBnb1.Models
{
    public class Vacation
    {

        int id;
        string startDate;
        string endDate;

        private static List<Vacation> OrderList = new List<Vacation>();
        public int Id { get => id; set => id = value; }
        public string UserId { get; set; }
        public int FlatId { get; set; }
        public string StartDate { get => startDate ; set => startDate= value; }

        public string EndDate { get => endDate; set => endDate = value; }

        public static List<Vacation> Read()
        {
            DBservices dbs = new DBservices();
            OrderList= dbs.ReadVacations();
            return OrderList;
        }

        public int Insert()
        { 
                foreach (Vacation item in OrderList)
                {
                    if ((item.FlatId==FlatId) && ((DateTime.Parse(item.StartDate) <= DateTime.Parse(StartDate) && DateTime.Parse(item.EndDate) >= DateTime.Parse(EndDate)) ||
                        (DateTime.Parse(item.StartDate) >= DateTime.Parse(StartDate) && DateTime.Parse(item.EndDate) >= DateTime.Parse(EndDate) && DateTime.Parse(EndDate) >= DateTime.Parse(item.StartDate)) ||
                        (DateTime.Parse(item.StartDate) <= DateTime.Parse(StartDate) && DateTime.Parse(StartDate) <= DateTime.Parse(item.EndDate) && DateTime.Parse(item.EndDate) <= DateTime.Parse(EndDate)) ||
                        (DateTime.Parse(item.StartDate) >= DateTime.Parse(StartDate) && DateTime.Parse(item.EndDate) <= DateTime.Parse(EndDate))))
                        return 0;    
                }
                DBservices dbs = new DBservices();
                return dbs.InsertVacation(this);
        }

        public List<Vacation> GetOredersByDates(string SDate, string EDate)
        {
            List<Vacation> OredersByDates = new List<Vacation>();
            foreach (Vacation v in OrderList)
            {
                if(Convert.ToInt32(v.StartDate) >= Convert.ToInt32(SDate) && Convert.ToInt32(v.EndDate) <= Convert.ToInt32(EDate))
                    OredersByDates.Add(v);
            }
            return OredersByDates;
        }

    }
}
