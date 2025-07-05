using Microsoft.Extensions.Logging;
using SQLite;
using YokaiSupermarketPriceTracker.Infrastructure;
using YokaiSupermarketPriceTracker.Infrastructure.Repositories;
using YokaiSupermarketPriceTracker.Interfaces;
using ZXing.Net.Maui.Controls;

namespace YokaiSupermarketPriceTracker;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseBarcodeReader() // necessário para ZXing.Net.MAUI
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "app.db");
        builder.Services.AddSingleton(new AppDbContext(dbPath));
        
        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IProductVariationRepository, ProductVariationRepository>();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}