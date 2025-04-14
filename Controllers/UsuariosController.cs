using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using prototipo1204.Data;
using prototipo1204.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Org.BouncyCastle.Crypto.Tls;

namespace prototipo1204.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly oceanidDBContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuariosController(oceanidDBContext context, IHttpContextAccessor httpcontextacessor)
        {
            _context = context;
            _httpContextAccessor = httpcontextacessor;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.idUser == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idUser,emailUser,senhaUser,telefoneUser,datacad_User")] Usuario usuario)
        {
            
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Clientes"); // Alterado para redirecionar para Clientes/Index
            
            return View(usuario); // Se houver erro de validação, retorna para a view com os erros
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idUser,emailUser,senhaUser,telefoneUser,datacad_User")] Usuario usuario)
        {
            if (id != usuario.idUser)
            {
                return NotFound();
            }

            
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.idUser))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.idUser == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.idUser == id);
        }


        [HttpPost]

        public async Task<IActionResult> Cadastro(Usuario usuario)
        {
            usuario.datacad_User = DateTime.Now;
            //Cliente cliente = await _context.Clientes.FirstOrDefault(c => c.idUser == usuario.idUser);
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            // Criando a lista de claims
            //Claims são um tipo de identificadores do usuario
            var claims = new List<Claim> // guarda os dados dos usuarios
            {
                new Claim(ClaimTypes.Name, usuario.emailUser),
                new Claim(ClaimTypes.SerialNumber, Convert.ToString(usuario.idUser)),
                // Convert.ToInt32(User.FindFirst(ClaimTypes.SerialNumber)?.Value)
                new Claim(ClaimTypes.Role, "Usuario")
            };
            //[Authorize(Roles = "Usuario")] para tipos especificos
            //[Authorize] logado

            //Criando o Claim de identidade do usuario, juntamente de coockies
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //Permite que o usuario continue logado mesmo se fechar o navegador
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true // Mantém o cookie ao fechar o navegador
            };
            //Vai logar o usuario com o HTTP usando tanto os coockies quanto a identidade do usuario
            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
           
            ViewBag.SuccessMessage = "Cadastro efetuado com sucesso!!!";
            return RedirectToAction("Index", "Home");

            
            


        }



    }
}
