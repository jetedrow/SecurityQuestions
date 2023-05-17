
// Setup DI Stack
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SecurityQuestions.Console;
using SecurityQuestions.Data;
using SecurityQuestions.Features;

var serviceProvider = new ServiceCollection()
    .AddSingleton<AppCore>()
    .AddScoped<MediatrLoader>()
    .AddDbContext<QuestionContext>(cfg => cfg.UseSqlite("Data Source=app.db;"))
    .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<MediatrLoader>())
    .BuildServiceProvider();

await serviceProvider.GetRequiredService<QuestionContext>().Database.MigrateAsync();

await serviceProvider.GetRequiredService<AppCore>().RunAsync();
