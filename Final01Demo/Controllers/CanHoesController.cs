using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Final01Demo.Data;
using Final01Demo.Models;
using Newtonsoft.Json;

namespace Final01Demo.Controllers
{
    public class CanHoesController : Controller
    {
        private readonly FinalDbContext _context;

        public CanHoesController(FinalDbContext context)
        {
            _context = context;
        }

        // GET: CanHoes
        public async Task<IActionResult> Index()
        {
            //var finalDbContext = _context.canHos.Include(c => c.toaNha);
            //return View(await finalDbContext.ToListAsync());

            //Lay ra can ho trong csdl
            var canHoes = await _context.canHos.Include(c => c.toaNha).ToListAsync();

            var deletedCanHoesJson = HttpContext.Session.GetString("DeletedCanHoes");// lay ra chuoi Json tu Session

            var deletedCanHos = string.IsNullOrEmpty(deletedCanHoesJson) ? new List<CanHo>() : JsonConvert.DeserializeObject<List<CanHo>>(deletedCanHoesJson);

            foreach(var canHo in deletedCanHos)
            {
                canHo.IsDeleted = true;
                canHoes.Add(canHo);
            }

            canHoes = canHoes.OrderBy(c=>c.ID).ToList();


            return View(canHoes);



        }

        // GET: CanHoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var canHo = await _context.canHos
                .Include(c => c.toaNha)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (canHo == null)
            {
                return NotFound();
            }

            return View(canHo);
        }

        // GET: CanHoes/Create
        public IActionResult Create()
        {
            ViewData["IDToaNha"] = new SelectList(_context.toaNha, "ID", "ID");
            return View();
        }

        // POST: CanHoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Ten,DienTich,SoNha,IDToaNha")] CanHo canHo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(canHo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IDToaNha"] = new SelectList(_context.toaNha, "ID", "ID", canHo.IDToaNha);
            return View(canHo);
        }

        // GET: CanHoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var canHo = await _context.canHos.FindAsync(id);
            if (canHo == null)
            {
                return NotFound();
            }
            ViewData["IDToaNha"] = new SelectList(_context.toaNha, "ID", "ID", canHo.IDToaNha);
            return View(canHo);
        }

        // POST: CanHoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,Ten,DienTich,SoNha,IDToaNha")] CanHo canHo)
        {
            if (id != canHo.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(canHo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CanHoExists(canHo.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IDToaNha"] = new SelectList(_context.toaNha, "ID", "ID", canHo.IDToaNha);
            return View(canHo);
        }

        // GET: CanHoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var canHo = await _context.canHos
                .Include(c => c.toaNha)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (canHo == null)
            {
                return NotFound();
            }

            return View(canHo);
        }

        // POST: CanHoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var canHo = await _context.canHos.FindAsync(id);
            if (canHo != null)
            {

                var deletedCanHoesJson = HttpContext.Session.GetString("DeletedCanHoes");// lay ra chuoi Json tu Session

                var deletedCanHos = string.IsNullOrEmpty(deletedCanHoesJson) ? new List<CanHo>() : JsonConvert.DeserializeObject<List<CanHo>>(deletedCanHoesJson);
                
                deletedCanHos.Add(canHo);
                HttpContext.Session.SetString("DeletedCanHoes", JsonConvert.SerializeObject(deletedCanHos));

                _context.canHos.Remove(canHo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Rollback(string id)
        {
            var deletedCanHoesJson = HttpContext.Session.GetString("DeletedCanHoes");// lay ra chuoi Json tu Session
            var deletedCanHos = string.IsNullOrEmpty(deletedCanHoesJson) ? new List<CanHo>() : JsonConvert.DeserializeObject<List<CanHo>>(deletedCanHoesJson);
            var canHo = deletedCanHos.FirstOrDefault(ch=>ch.ID == id);
            if(canHo != null)
            {
                _context.canHos.Add(canHo);
                _context.SaveChanges();
                deletedCanHos.Remove(canHo);
                HttpContext.Session.SetString("DeletedCanHoes", JsonConvert.SerializeObject(deletedCanHos));
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CanHoExists(string id)
        {
            return _context.canHos.Any(e => e.ID == id);
        }
    }
}
