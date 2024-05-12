using Cars.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Vehicle.Controllers
{
    public class BrandController : Controller
    {
        public static Db db = new Db();
        public Brand[] brandsList;
        public IActionResult Index()
        {
            brandsList = db.Brands.Include(b => b.Vehicles).ToArray();
            return View(brandsList);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult BrandCreate(Brand newBrand)
        {
            db.Brands.Add(newBrand);
            db.SaveChanges();
            Debug.WriteLine(newBrand.Name + newBrand.Email);
            brandsList = db.Brands.ToArray();
            return RedirectToAction("Index");
        }

        [Route("Brand/Detail/{brandId}")]
        public IActionResult Detail(int brandId)
        {
            var brand = db.Brands.FirstOrDefault(b  => b.Id == brandId);
            Debug.WriteLine(brandId);
            return View(brand);

        }

        public IActionResult Edit(Brand nb)
        {
            Debug.WriteLine(nb.Email);
            var brand = db.Brands.FirstOrDefault(b => b.Id == nb.Id);
            brand.Name = nb.Name;
            brand.Email = nb.Email;
            db.SaveChanges();
            brandsList = db.Brands.ToArray();
            return RedirectToAction("Index");
        }

        [Route("Brand/Delete/{brandId}")]
        public IActionResult Delete(int brandId)
        {
            var brand = db.Brands.FirstOrDefault(b => b.Id == brandId);
            db.Brands.Remove(brand);
            db.SaveChanges();
            Debug.WriteLine(brandId);
            return RedirectToAction("Index");

        }
    }
}
