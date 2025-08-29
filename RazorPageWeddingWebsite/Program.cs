using Microsoft.AspNetCore.Rewrite;
using RazorPageWeddingWebsite.Constants;
using RazorPageWeddingWebsite.Core.Interfaces;
using RazorPageWeddingWebsite.Core.Services.ContentHandling;
using RazorPageWeddingWebsite.Core.Services.ContentProcessing.Interfaces;
using RazorPageWeddingWebsite.Core.Services.Processors;
using RazorPageWeddingWebsite.Helpers;
using RazorPageWeddingWebsite.Helpers.Html;
using RazorPageWeddingWebsite.Helpers.Interfaces;
using RazorPageWeddingWebsite.Helpers.Renderers;
using RazorPageWeddingWebsite.Helpers.Serialisation;
using RazorPageWeddingWebsite.Helpers.Wrappers;
using RazorPageWeddingWebsite.Infrastructure.Repositories;
using RazorPageWeddingWebsite.Middleware;
using RazorPageWeddingWebsite.Services;
using RazorPageWeddingWebsite.Services.Breadcrumb;
using RazorPageWeddingWebsite.Services.Interfaces;
using Zengenti.Contensis.Delivery;



var builder = WebApplication.CreateBuilder(args);

// Register generic data service
builder.Services.AddTransient(typeof(IDataService<>), typeof(ContensisDataService<>));

// Register IContensisClient as a singleton
builder.Services.AddSingleton<IContensisClient>(sp =>
{
    var realClient = ContensisClient.Create();
    return new ContensisClientWrapper(realClient);
});

//register repositories
builder.Services.AddTransient<IContentRepository, ContensisContentRepository>();

//register helpers
builder.Services.AddScoped<ISerializationHelper, SerializationHelper>();
builder.Services.AddScoped<ICanvasPanelHelper, CanvasPanelHelperWrapper>();
builder.Services.AddScoped<IPanelHelper, PanelHelperWrapper>();
builder.Services.AddScoped<IParagraphHelper, ParagraphHelperWrapper>();
builder.Services.AddScoped<INavigationLinkHelper, NavigationLinkHelperWrapper> ();
builder.Services.AddScoped<IFormHelper, FormHelperWrapper>();
builder.Services.AddScoped<IContentFragmentHelper, ContentFragmentHelper>();
builder.Services.AddScoped<IImageHelper, ImageHelperWrapper>();
builder.Services.AddScoped<ITableHelper, TableHelperWrapper>();
builder.Services.AddScoped<IAccordionRenderer, AccordionRenderer>();
builder.Services.AddScoped<IBgCtaLinkRenderer, BgCtaLinkRenderer>();

//Processors
builder.Services.AddScoped<ITextProcessor, HtmlTextProcessor>();


// Configure logging
builder.Services.AddLogging(configure =>
    configure.AddConsole().SetMinimumLevel(LogLevel.Information));

// Add services to the container
string relativeUrlPath = WebsiteConstants.SITE_VIEW_PATH.TrimEnd('/');
builder.Services
    .AddRazorPages()
    .AddRazorPagesOptions(options =>
    {

        // Override root to always render blog post at '/'
        options.Conventions.AddPageRoute("/Home/Index", WebsiteConstants.SITE_VIEW_PATH);
        options.Conventions.AddPageRoute("/Home/Details", WebsiteConstants.SITE_VIEW_PATH + "{*slug}");

        options.Conventions.AddPageRoute("/Venues/Index", WebsiteConstants.SITE_VIEW_PATH + "Venues");
        options.Conventions.AddPageRoute("/Venues/Details", WebsiteConstants.SITE_VIEW_PATH + "Venues/{*slug}");

        options.Conventions.AddPageRoute("/Ceremonies/Details", WebsiteConstants.SITE_VIEW_PATH + "Ceremonies/{*slug}");


        options.Conventions.AddPageRoute("/Stories/Index", WebsiteConstants.SITE_VIEW_PATH + "stories");
        options.Conventions.AddPageRoute("/Stories/Details", WebsiteConstants.SITE_VIEW_PATH + "stories/{*slug}");

        options.Conventions.Add(new GlobalHeaderPageApplicationModelConvention());
    });

builder.Services.AddScoped<BreadcrumbService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddContentHandlers();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.UseHttpsRedirection(); NO SUPPORT FOR .NET 9,0 
app.UseStaticFiles();

//add rewrite options 
// Configure URL rewriting
var rewriteOptions = new RewriteOptions()
    // Rewrite root path to your specific page
    .AddRewrite(@"^$", WebsiteConstants.SITE_VIEW_PATH.TrimEnd('/'), skipRemainingRules: true);
// Rewrite /venues to the full path
//.AddRewrite(@"^venues$", WebsiteConstants.SITE_VIEW_PATH.TrimEnd('/'), skipRemainingRules: true);

app.UseRewriter(rewriteOptions);




app.UseRouting();
// app.UseHttpLogging(); NO SUPPORT FOR .NET 9,0 
app.UseMiddleware<BreadcrumbMiddleware>();
app.UseStatusCodePagesWithReExecute("/Error");
app.MapRazorPages();

DotNetEnv.Env.TraversePath().Load();

ContensisClient.Configure(
    new ContensisClientConfiguration(
        rootUrl: string.Format("https://api-{0}.cloud.contensis.com", DotNetEnv.Env.GetString("ALIAS")),
        projectApiId: DotNetEnv.Env.GetString("PROJECT_API_ID"),
        clientId: DotNetEnv.Env.GetString("CONTENSIS_CLIENT_ID"),
        sharedSecret: DotNetEnv.Env.GetString("CONTENSIS_CLIENT_SECRET")
    )
);

app.Run();
