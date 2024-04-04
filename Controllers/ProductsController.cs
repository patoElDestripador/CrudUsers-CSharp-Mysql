using Microsoft.AspNetCore.Mvc;
using CrudUsers0.Data;
using Microsoft.EntityFrameworkCore;

namespace CrudUsers0.Controllers
{
    public class ProductsController : Controller
    {
        private readonly BaseContext _context;

        public ProductsController(BaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            return View(await _context.Products.FirstOrDefaultAsync(element => element.Id == id));
        }

    }
}