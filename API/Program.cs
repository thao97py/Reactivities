using System.Net;
using API.Extensions;
using API.Middleware;
using API.SignalR;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt => 
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
});
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
// builder.Services.AddDbContext<DataContext>(opt =>{
//     opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
// });
// builder.Services.AddCors(opt=>
//     opt.AddPolicy("CorsPolicy",policy=>{
//         policy.AllowAnyHeader().WithOrigins("http://localhost:3000");
//     })
// );
// builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(List.Handler).Assembly));
// builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

//Enhance security
app.UseXContentTypeOptions(); //stops a browser from trying to MIME-sniff the content type 
app.UseReferrerPolicy(opt => opt.NoReferrer()); //prevent browser from referrer policy
app.UseXXssProtection(opt => opt.EnabledWithBlockMode()); //add cross-site scripting protection header
app.UseXfo(opt => opt.Deny()); // prevent clickjacking
app.UseCsp(opt => opt   //prevent XSS attacks
    .BlockAllMixedContent()
    .StyleSources( s=> s.Self().CustomSources("https://fonts.googleapis.com"))
    .FontSources( s=> s.Self().CustomSources("https://fonts.gstatic.com","data:"))
    .FormActions( s => s.Self())
    .FrameAncestors( s=> s.Self())
    .ImageSources( s=> s.Self().CustomSources("https://res.cloudinary.com","blob:"))
    .ScriptSources(s => s.Self())
);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else{
    app.Use(async (context,next) => {
        context.Response.Headers.Add("Strict-Transport-Security","max-age=31536000");
        await next.Invoke();
    });
}

// app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.UseDefaultFiles(); //for production
app.UseStaticFiles(); //for production

app.MapControllers();
app.MapHub<ChatHub>("/chat");
app.MapFallbackToController("Index","Fallback");

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try{
    var context = services.GetRequiredService<DataContext>();
    var userMamanger = services.GetRequiredService<UserManager<AppUser>>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context, userMamanger);
}catch (Exception ex){
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}

app.Run();
