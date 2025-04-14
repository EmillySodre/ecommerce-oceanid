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
    public class EnderecoClientesController : Controller
    {
        private readonly oceanidDBContext _context;

        public EnderecoClientesController(oceanidDBContext context)
        {
            _context = context;
        }

        // GET: EnderecoClientes
        public async Task<IActionResult> Index()
        {
            var oceanidDBContext = _context.EnderecoClientes.Include(e => e.cliente).Include(e => e.endereco);
            return View(await oceanidDBContext.ToListAsync());
        }

        // GET: EnderecoClientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enderecoCliente = await _context.EnderecoClientes
                .Include(e => e.cliente)
                .Include(e => e.endereco)
                .FirstOrDefaultAsync(m => m.idEndCliente == id);
            if (enderecoCliente == null)
            {
                return NotFound();
            }

            return View(enderecoCliente);
        }

        // GET: EnderecoClientes/Create
        public IActionResult Create()
        {
            ViewData["idCliente"] = new SelectList(_context.Clientes, "idCliente", "idCliente");
            ViewData["idEndereco"] = new SelectList(_context.Enderecos, "idEnd", "idEnd");
            return View();
        }

        // POST: EnderecoClientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idEndCliente,idCliente,idEndereco")] EnderecoCliente enderecoCliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enderecoCliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["idCliente"] = new SelectList(_context.Clientes, "idCliente", "idCliente", enderecoCliente.idCliente);
            ViewData["idEndereco"] = new SelectList(_context.Enderecos, "idEnd", "idEnd", enderecoCliente.idEndereco);
            return View(enderecoCliente);
        }

        // GET: EnderecoClientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enderecoCliente = await _context.EnderecoClientes.FindAsync(id);
            if (enderecoCliente == null)
            {
                return NotFound();
            }
            ViewData["idCliente"] = new SelectList(_context.Clientes, "idCliente", "idCliente", enderecoCliente.idCliente);
            ViewData["idEndereco"] = new SelectList(_context.Enderecos, "idEnd", "idEnd", enderecoCliente.idEndereco);
            return View(enderecoCliente);
        }

        // POST: EnderecoClientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idEndCliente,idCliente,idEndereco")] EnderecoCliente enderecoCliente)
        {
            if (id != enderecoCliente.idEndCliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enderecoCliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnderecoClienteExists(enderecoCliente.idEndCliente))
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
            ViewData["idCliente"] = new SelectList(_context.Clientes, "idCliente", "idCliente", enderecoCliente.idCliente);
            ViewData["idEndereco"] = new SelectList(_context.Enderecos, "idEnd", "idEnd", enderecoCliente.idEndereco);
            return View(enderecoCliente);
        }

        // GET: EnderecoClientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enderecoCliente = await _context.EnderecoClientes
                .Include(e => e.cliente)
                .Include(e => e.endereco)
                .FirstOrDefaultAsync(m => m.idEndCliente == id);
            if (enderecoCliente == null)
            {
                return NotFound();
            }

            return View(enderecoCliente);
        }

        // POST: EnderecoClientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enderecoCliente = await _context.EnderecoClientes.FindAsync(id);
            if (enderecoCliente != null)
            {
                _context.EnderecoClientes.Remove(enderecoCliente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnderecoClienteExists(int id)
        {
            return _context.EnderecoClientes.Any(e => e.idEndCliente == id);
        }
    }
}
