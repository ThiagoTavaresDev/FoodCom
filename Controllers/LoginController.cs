using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Org.BouncyCastle.Crypto.Generators;
using ProjetoFoodCom.Data;
using ProjetoFoodCom.Models;

namespace ProjetoFoodCom.Controllers;
[Route("[controller]")]
public class LoginController : Controller
{

    private readonly AppDbContext _context;

    public LoginController(AppDbContext context)
    {
        _context = context; 
    }


    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("/Login")]
    public async Task<IActionResult> Login(string email, string password, string returnUrl = null)
    {

        var usuario = _context.Clientes.FirstOrDefault(u => u.Email == email && u.Senha == password);

        if(usuario != null && usuario.Email != "admin@foodcom" && usuario.Senha != "1234")
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.Role, "User"),
                new Claim("ClienteId", usuario.Id.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");

        }

        if (email == "admin@foodcom" && password == "1234")
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.Role, "Admin")
            };
           
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");

        }

      
        ViewData["Error"] = "Credenciais inválidas.";
        return View();
    }

    [HttpGet]
    [Route("/Logout")]
    public async Task<IActionResult> Logout()
    {
       await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
       return RedirectToAction("Login", "Login");
    }


    [HttpGet]
    [Route("/Cadastro")]

    public IActionResult Cadastro()
    {
        return View();
    }


    [HttpPost]
    [Route("/Cadastro")]

    public IActionResult Cadastro(string nome,string email, string senha, string telefone, string endereco)
    {

        if(_context.Clientes.Any(c => c.Email == email)){
            ModelState.AddModelError("ClienteCadastrado", "Usuário já cadastrado!");
            return View();
        }

        var Cliente = new Cliente
        {   Nome = nome,
            Email = email,
            Senha = senha,
            Telefone = telefone,
            Endereco = endereco,
            DataCadastro = DateTime.Now
        };
        _context.Clientes.Add(Cliente);
        _context.SaveChanges();
        return RedirectToAction("Login", "Login");

    }

}
    