﻿using Microsoft.EntityFrameworkCore;

namespace Discount.GRPC.Data;

public static class Extensions
{
    //Auto Migrations
    public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var dbcontext = scope.ServiceProvider.GetRequiredService<DiscountContext>();
        dbcontext.Database.MigrateAsync();
        return app;
    }
}
