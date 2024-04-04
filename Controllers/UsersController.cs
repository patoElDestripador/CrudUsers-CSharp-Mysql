using CrudUsers0.Data;
using CrudUsers0.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CrudUsers02.Controllers{

    public class UsersController : Controller
    {   
        public readonly BaseContext _context; 
        public UsersController(BaseContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            return View(await _context.Users.FirstOrDefaultAsync(element => element.Id == id));
        } 

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult create(User u)
        {
            _context.Users.Add(u);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int? id){
            if(id != null)
            {
                var user = await _context.Users.FindAsync(id);
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }

[HttpGet] 
public async Task<IActionResult> Update(int id)
{
    var user = await _context.Users.FindAsync(id);
    if (user == null)
    {
        return NotFound();
    }
    return View(user);
}

    [HttpPost]
    public async Task<IActionResult> Update(int id, [Bind("Id,FirstName,LastName,Email")] User user)
    {
        if (id != user.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
        }
        return View(user); 
    }
    }

}



