var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World. This my Yarp Api Gateway!");

app.Run();
