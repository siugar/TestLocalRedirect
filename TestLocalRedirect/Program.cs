using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;

var builder = WebApplication.CreateBuilder(args);

#region LocalizationServices

builder.Services.AddPortableObjectLocalization(options => options.ResourcesPath = "Resources");
//builder.Services.Replace(ServiceDescriptor.Singleton<ILocalizationFileLocationProvider, ModularPoFileLocationProvider>());


const string culture = "zh-Hant";
builder.Services
    .Configure<RequestLocalizationOptions>(options => options
    .AddSupportedCultures("en", "zh-Hant")        
    .AddSupportedUICultures("en", "zh-Hant")
    .DefaultRequestCulture = new RequestCulture(culture: culture, uiCulture: culture)
    );
    
builder.Services.AddRazorPages().AddViewLocalization().AddDataAnnotationsLocalization().AddRazorRuntimeCompilation();

#endregion

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseRequestLocalization();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
