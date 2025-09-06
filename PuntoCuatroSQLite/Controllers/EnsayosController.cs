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
    public class EnsayosController : Controller
    {
        private readonly LaboratorioContext _context;

        public EnsayosController(LaboratorioContext context)
        {
            _context = context;
        }

        // GET: Ensayos
        public async Task<IActionResult> Index()
        {
            var laboratorioContext = _context.Ensayos.Include(e => e.Muestra);
            return View(await laboratorioContext.ToListAsync());
        }

        // GET: Ensayos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ensayo = await _context.Ensayos
                .Include(e => e.Muestra)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ensayo == null)
            {
                return NotFound();
            }

            return View(ensayo);
        }

        // GET: Ensayos/Create
        public IActionResult Create()
        {
            ViewData["MuestraId"] = new SelectList(_context.Muestras, "Id", "Nombre");
            return View();
        }

        // POST: Ensayos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MuestraId,Tipo,Resultado,Fecha")] Ensayo ensayo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ensayo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MuestraId"] = new SelectList(_context.Muestras, "Id", "Nombre", ensayo.MuestraId);
            return View(ensayo);
        }

        // GET: Ensayos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ensayo = await _context.Ensayos.FindAsync(id);
            if (ensayo == null)
            {
                return NotFound();
            }
            ViewData["MuestraId"] = new SelectList(_context.Muestras, "Id", "Nombre", ensayo.MuestraId);
            return View(ensayo);
        }

        // POST: Ensayos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MuestraId,Tipo,Resultado,Fecha")] Ensayo ensayo)
        {
            if (id != ensayo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ensayo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnsayoExists(ensayo.Id))
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
            ViewData["MuestraId"] = new SelectList(_context.Muestras, "Id", "Nombre", ensayo.MuestraId);
            return View(ensayo);
        }

        // GET: Ensayos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ensayo = await _context.Ensayos
                .Include(e => e.Muestra)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ensayo == null)
            {
                return NotFound();
            }

            return View(ensayo);
        }

        // POST: Ensayos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ensayo = await _context.Ensayos.FindAsync(id);
            if (ensayo != null)
            {
                _context.Ensayos.Remove(ensayo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnsayoExists(int id)
        {
            return _context.Ensayos.Any(e => e.Id == id);
        }
    }
}


