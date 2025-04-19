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
        new MySqlServerVersion(new Version(8, 0, 31)) // Ajuste conforme sua vers�o do MySQL
    )
);

// Adicionando autentica��o com cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Login"; // Caminho para a p�gina de login
    });
// Adicionando HttpContextAccessor (necess�rio para acessar o contexto HTTP)
builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Inje��o de depend�ncia do reposit�rio de login
builder.Services.AddScoped<ILoginRepositorio, LoginRepositorio>();


// Adicionando o servi�o de sess�o
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Define o tempo de expira��o da sess�o
    options.Cookie.HttpOnly = true;  // Aumenta a seguran�a
    options.Cookie.IsEssential = true;  // Necess�rio para conformidade com a GDPR
});




var app = builder.Build();

// Ativar autentica��o e autoriza��o
app.UseAuthentication(); // Ativa a autentica��o com cookies
app.UseAuthorization();  // Ativa a autoriza��o

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
