using MovieManagement.DataAccess.Entities;

namespace MovieManagement.DataAccess.CQRS.Commands;

public class AddMovieCommand : CommandBase<Movie, Movie>
{
    public override async Task<Movie> Execute(MovieManagementStorageContext context)
    {
        await context.Movies.AddAsync(Parameter!);
        await context.SaveChangesAsync();
        return Parameter!;
    }
}
