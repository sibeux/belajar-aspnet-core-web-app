using DIPractice.Abstraction;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

public class UserEndpoint : IEndpoint
{
    public void Map(WebApplication app)
    {
        // Ini adalah minimal API. lebih cepat dan simple daripada pakai controller.
        // Di sini jugalah yang akan menangani DI seperti constuctor di controller.
        // Alternatif lain juga untuk tangkap route value.
        app.MapGet("/friend/{NamaLengkap}", ([FromRoute(Name = "NamaLengkap")] string name, IUsersService usersService) =>
        {
            return Results.Text(usersService.getFriendName(name), "text/plain");
        });
    }
}
