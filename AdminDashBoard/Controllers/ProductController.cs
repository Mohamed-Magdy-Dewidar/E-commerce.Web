using AdminDashBoard.AttachmentServices;
using AdminDashBoard.Models;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.ProductModule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashBoard.Controllers
{


    //[Authorize(Roles = "Admin")]
    public class ProductController(IUnitOfWork _unitOfWork , IMapper _mapper , IAttachmentService attachmentService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var products = await _unitOfWork.GetRepository<Product ,int >().GetAllAsync();
            if(products == null)
            {
                ModelState.AddModelError(string.Empty, "No products found.");
                return View(new List<ProductViewModel>());
            }
            var mappedProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);
            return View(mappedProducts);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (productViewModel.Image != null)
                    {
                        //productViewModel.PictureUrl = attachmentService.UploadFile(productViewModel.Image, "products");
                        productViewModel.PictureUrl = await attachmentService.UploadFileAsync(productViewModel.Image ,"products");
                    }                    
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Image", "Image upload failed: " + ex.Message);
                    return View(productViewModel);
                }
                var mappedProduct = _mapper.Map<ProductViewModel, Product>(productViewModel);
                await _unitOfWork.GetRepository<Product,int>().AddAsync(mappedProduct);
                await _unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(productViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(id);
            var mappedProduct = _mapper.Map<Product,ProductViewModel>(product);
            return View(mappedProduct);
        }



        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                var productRepository = _unitOfWork.GetRepository<Product, int>();
                var product = await productRepository.GetByIdAsync(id);
                if (product == null) return NotFound();

                if (productViewModel.Image != null)
                {
                    if (!string.IsNullOrEmpty(product.PictureUrl))
                        await attachmentService.DeleteFileAsync(product.Id);

                    productViewModel.PictureUrl = await attachmentService.UploadFileAsync(productViewModel.Image, "products");
                }


                _mapper.Map(productViewModel, product); // Map into the tracked entity
                productRepository.Update(product); // Safe update

                var result = await _unitOfWork.SaveChanges();
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(productViewModel);
        }






        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(id);
            var mappedProduct = _mapper.Map<Product,ProductDeleteViewModel>(product);
            return View(mappedProduct); 
        }





        [HttpPost]
        [ValidateAntiForgeryToken] // optional, but good security
        public async Task<IActionResult>Delete(int id , ProductDeleteViewModel productDeleteViewModel)
        {
            if (id != productDeleteViewModel.Id)
                return NotFound();
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid product data.");
                return View(productDeleteViewModel);
            }
                
            

            var ProductRepository = _unitOfWork.GetRepository<Product, int>();
            try
            {
                var product = await ProductRepository.GetByIdAsync(id);            
                if (product == null)
                {
                    ModelState.AddModelError(string.Empty, "Product not found.");
                    return View(productDeleteViewModel);
                }
                if (!string.IsNullOrEmpty(product?.PictureUrl))
                {
                    await attachmentService.DeleteFileAsync(product.Id);
                }
                ProductRepository.Delete(product);
                await _unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the product: " + ex.Message);
            }
            return View(productDeleteViewModel);
        }
    
    
    }
}
