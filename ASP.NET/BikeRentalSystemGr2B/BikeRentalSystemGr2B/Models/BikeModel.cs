using BikeRentalSystemGr2B.ViewModels;

namespace BikeRentalSystemGr2B.Models
{
    public class BikeModel
    {
        public int Id { get; set; }
        public string Producer { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public BikeTypeModel BikeTypeModel { get; set; }
        public int NumberOfBikes { get; set; }
        public int NumberOfGears { get; set; }

        

    }
}
