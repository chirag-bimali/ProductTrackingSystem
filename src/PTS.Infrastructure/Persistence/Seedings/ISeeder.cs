namespace PTS.Infrastructure.Persistence.Seedings;

public interface ISeeder
{
    int Order { get; }   // lower runs first
    Task SeedAsync(CancellationToken ct = default);
}