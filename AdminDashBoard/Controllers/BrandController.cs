using DomainLayer.Contracts;
using DomainLayer.Models.ProductModule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AdminDashBoard.Controllers
{


    //[Authorize(Roles = "Admin")]
    public class BrandController(IUnitOfWork _unitOfWork) : Controller
	{
		public async Task<IActionResult> Index()
		{
			var brands = await _unitOfWork.GetRepository<ProductBrand , int>().GetAllAsync();
            return View(brands);
		}

		public async Task<IActionResult> Create(ProductBrand brand)
		{
			var BrandRepository = _unitOfWork.GetRepository<ProductBrand, int>();
			try
			{
                if (ModelState.IsValid)
				{

					var brandExists = await BrandRepository.GetByIdAsync(brand.Id);
					if (brandExists == null)
					{
						await BrandRepository.AddAsync(brand);
						await _unitOfWork.SaveChanges();
						return RedirectToAction(nameof(Index));
					}
					else
					{
						ModelState.AddModelError("BrandName", "Brand is already exist");
						return View(nameof(Index), await BrandRepository.GetAllAsync());
					}
                }
                return RedirectToAction(nameof(Index));
			}
			catch (System.Exception)
			{
				ModelState.AddModelError("Name", "Please enter new name");
				return View("Index", await BrandRepository.GetAllAsync());
			}
		}
		public async Task<IActionResult> Delete(int id)
		{
			var BrandRepository = _unitOfWork.GetRepository<ProductBrand, int>();
            var brand = await BrandRepository.GetByIdAsync(id);
			BrandRepository.Delete(brand);
            await _unitOfWork.SaveChanges();
			return RedirectToAction(nameof(Index));
		}
	}
}
