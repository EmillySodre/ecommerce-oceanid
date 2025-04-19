using prototipo1204.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using prototipo1204.Repositorios.Interface;
using prototipo1204.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Configurar o Entity Framework Core para usar MySQL
builder.Services.AddDbContext<oceanidDBContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("prototipoBanco"),
        new MySqlServerVersion(new Version(8, 0, 31)) // Ajuste conforme sua versão do MySQL
    )
);

// Adicionando autenticação com cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Login"; // Caminho para a página de login
    });
// Adicionando HttpContextAccessor (necessário para acessar o contexto HTTP)
builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Injeção de dependência do repositório de login
builder.Services.AddScoped<ILoginRepositorio, LoginRepositorio>();


// Adicionando o serviço de sessão
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Define o tempo de expiração da sessão
    options.Cookie.HttpOnly = true;  // Aumenta a segurança
    options.Cookie.IsEssential = true;  // Necessário para conformidade com a GDPR
});




var app = builder.Build();

// Ativar autenticação e autorização
app.UseAuthentication(); // Ativa a autenticação com cookies
app.UseAuthorization();  // Ativa a autorização

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
