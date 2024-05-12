using Microsoft.Extensions.Hosting;

namespace Cars.Models
{
    public class Brand
    {
        public int Id { get; set; }

        public string? Email { get; set; }
        public string? Name { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }


        public Brand() { 
            Vehicles = new List<Vehicle>();
        }

    }
}
