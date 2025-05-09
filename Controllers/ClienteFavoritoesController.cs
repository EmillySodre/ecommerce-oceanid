using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using prototipo1204.Data;
using prototipo1204.Models;

namespace prototipo1204.Controllers
{
    public class ClienteFavoritoesController : Controller
    {
        private readonly oceanidDBContext _context;

        public ClienteFavoritoesController(oceanidDBContext context)
        {
            _context = context;
        }

        // GET: ClienteFavoritoes
        public async Task<IActionResult> Index()
        {
            var oceanidDBContext = _context.ClienteFavoritos.Include(c => c.Cliente).Include(c => c.Produto);
            return View(await oceanidDBContext.ToListAsync());
        }

        // GET: ClienteFavoritoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteFavorito = await _context.ClienteFavoritos
                .Include(c => c.Cliente)
                .Include(c => c.Produto)
                .FirstOrDefaultAsync(m => m.IdClienteFav == id);
            if (clienteFavorito == null)
            {
                return NotFound();
            }

            return View(clienteFavorito);
        }

        // GET: ClienteFavoritoes/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "idCliente", "idCliente");
            ViewData["IdProd"] = new SelectList(_context.Produtos, "idProd", "idProd");
            return View();
        }

        // POST: ClienteFavoritoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdClienteFav,IdCliente,IdProd,Ativo")] ClienteFavorito clienteFavorito)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clienteFavorito);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "idCliente", "idCliente", clienteFavorito.IdCliente);
            ViewData["IdProd"] = new SelectList(_context.Produtos, "idProd", "idProd", clienteFavorito.IdProd);
            return View(clienteFavorito);
        }

        // GET: ClienteFavoritoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteFavorito = await _context.ClienteFavoritos.FindAsync(id);
            if (clienteFavorito == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "idCliente", "idCliente", clienteFavorito.IdCliente);
            ViewData["IdProd"] = new SelectList(_context.Produtos, "idProd", "idProd", clienteFavorito.IdProd);
            return View(clienteFavorito);
        }

        // POST: ClienteFavoritoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdClienteFav,IdCliente,IdProd,Ativo")] ClienteFavorito clienteFavorito)
        {
            if (id != clienteFavorito.IdClienteFav)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clienteFavorito);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteFavoritoExists(clienteFavorito.IdClienteFav))
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
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "idCliente", "idCliente", clienteFavorito.IdCliente);
            ViewData["IdProd"] = new SelectList(_context.Produtos, "idProd", "idProd", clienteFavorito.IdProd);
            return View(clienteFavorito);
        }

        // GET: ClienteFavoritoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteFavorito = await _context.ClienteFavoritos
                .Include(c => c.Cliente)
                .Include(c => c.Produto)
                .FirstOrDefaultAsync(m => m.IdClienteFav == id);
            if (clienteFavorito == null)
            {
                return NotFound();
            }

            return View(clienteFavorito);
        }

        // POST: ClienteFavoritoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clienteFavorito = await _context.ClienteFavoritos.FindAsync(id);
            if (clienteFavorito != null)
            {
                _context.ClienteFavoritos.Remove(clienteFavorito);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteFavoritoExists(int id)
        {
            return _context.ClienteFavoritos.Any(e => e.IdClienteFav == id);
        }
    }
}
