using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PuntoCuatroSQLite.Data;
using PuntoCuatroSQLite.Models;

namespace PuntoCuatro.Controllers
{
    public class MuestrasController : Controller
    {
        private readonly LaboratorioContext _context;

        public MuestrasController(LaboratorioContext context)
        {
            _context = context;
        }

        // GET: Muestras
        public async Task<IActionResult> Index()
        {
            return View(await _context.Muestras.ToListAsync());
        }

        // GET: Muestras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var muestra = await _context.Muestras
                .FirstOrDefaultAsync(m => m.Id == id);
            if (muestra == null)
            {
                return NotFound();
            }

            return View(muestra);
        }

        // GET: Muestras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Muestras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Matriz,FechaToma")] Muestra muestra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(muestra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(muestra);
        }

        // GET: Muestras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var muestra = await _context.Muestras.FindAsync(id);
            if (muestra == null)
            {
                return NotFound();
            }
            return View(muestra);
        }

        // POST: Muestras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Matriz,FechaToma")] Muestra muestra)
        {
            if (id != muestra.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(muestra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MuestraExists(muestra.Id))
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
            return View(muestra);
        }

        // GET: Muestras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var muestra = await _context.Muestras
                .FirstOrDefaultAsync(m => m.Id == id);
            if (muestra == null)
            {
                return NotFound();
            }

            return View(muestra);
        }

        // POST: Muestras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var muestra = await _context.Muestras.FindAsync(id);
            if (muestra != null)
            {
                _context.Muestras.Remove(muestra);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MuestraExists(int id)
        {
            return _context.Muestras.Any(e => e.Id == id);
        }
    }
}

