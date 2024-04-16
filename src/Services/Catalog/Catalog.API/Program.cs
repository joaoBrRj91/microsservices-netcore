#region Add Services To The Container - DI
var builder = WebApplication.CreateBuilder(args);
#endregion


#region Configure The HTTP Request Pipeline
var app = builder.Build();
app.Run();
#endregion
