

#region Add Services To The Container - DI
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
#endregion

#region Configure The HTTP Request Pipeline - Middleware
app.Run();
#endregion