using Cars.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;

namespace Vehicle.Controllers
{
    public class VehicleController : Controller
    {
        public static Db db = new Db();
        public Cars.Models.Vehicle[] vehiclesList;
        public IActionResult Index()
        {
            vehiclesList = db.Vehicles.ToArray();
            return View(vehiclesList);
        }

        public IActionResult Create()
        {
            var brandList = db.Brands.ToArray();
            ViewBag.Brands = brandList;
            return View();
        }

        public IActionResult VehicleCreate(string CarPlate, int Km, string Model,string Color, string GearType, string brand)
        {
            var intID = Int32.Parse(brand);
            Debug.WriteLine(intID);
            Brand? selectedBrand = db.Brands.FirstOrDefault(b => b.Id == intID);

            if (selectedBrand != null)
            {
                var newVehicle = new Cars.Models.Vehicle()
                {
                    CarPlate = CarPlate,
                    Km = Km,
                    Model = Model,
                    Color = Color,
                    GearType = GearType,
                    brand = selectedBrand
                };

                Debug.WriteLine("Found!!" + selectedBrand.Email + newVehicle.brand.Email);
                selectedBrand.Vehicles.Add(newVehicle);
                db.Vehicles.Add(newVehicle);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [Route("Vehicle/Detail/{vehicleId}")]
        public IActionResult Detail(int vehicleId)
        {
            var vehicle = db.Vehicles.FirstOrDefault(b  => b.Id == vehicleId);
            var brandList = db.Brands.ToArray();
            ViewBag.Brands = brandList;
            Debug.WriteLine(vehicleId);
            return View(vehicle);

        }

        [Route("Vehicle/Delete/{vehicleId}")]
        public IActionResult Delete(int vehicleId)
        {
            Cars.Models.Vehicle? vhc = db.Vehicles.FirstOrDefault(v => v.Id == vehicleId);
            db.Vehicles.Remove(vhc);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
