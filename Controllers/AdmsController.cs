﻿using System;
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
    public class AdmsController : Controller
    {
        private readonly oceanidDBContext _context;

        public AdmsController(oceanidDBContext context)
        {
            _context = context;
        }

        // GET: Adms
        public async Task<IActionResult> Index()
        {
            var oceanidDBContext = _context.Adms.Include(a => a.usuario);
            return View(await oceanidDBContext.ToListAsync());
        }

        // GET: Adms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adm = await _context.Adms
                .Include(a => a.usuario)
                .FirstOrDefaultAsync(m => m.idAdm == id);
            if (adm == null)
            {
                return NotFound();
            }

            return View(adm);
        }

        // GET: Adms/Create
        public IActionResult Create()
        {
            ViewData["idUser"] = new SelectList(_context.Usuarios, "idUser", "idUser");
            return View();
        }

        // POST: Adms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idAdm,nomeAdm,emailAdm,idUser")] Adm adm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["idUser"] = new SelectList(_context.Usuarios, "idUser", "idUser", adm.idUser);
            return View(adm);
        }

        // GET: Adms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adm = await _context.Adms.FindAsync(id);
            if (adm == null)
            {
                return NotFound();
            }
            ViewData["idUser"] = new SelectList(_context.Usuarios, "idUser", "idUser", adm.idUser);
            return View(adm);
        }

        // POST: Adms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idAdm,nomeAdm,emailAdm,idUser")] Adm adm)
        {
            if (id != adm.idAdm)
            {
                return NotFound();
            }

            
                try
                {
                    _context.Update(adm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdmExists(adm.idAdm))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["idUser"] = new SelectList(_context.Usuarios, "idUser", "idUser", adm.idUser);
            return View(adm);
        }

        // GET: Adms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adm = await _context.Adms
                .Include(a => a.usuario)
                .FirstOrDefaultAsync(m => m.idAdm == id);
            if (adm == null)
            {
                return NotFound();
            }

            return View(adm);
        }

        // POST: Adms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adm = await _context.Adms.FindAsync(id);
            if (adm != null)
            {
                _context.Adms.Remove(adm);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdmExists(int id)
        {
            return _context.Adms.Any(e => e.idAdm == id);
        }
    }
}
