
namespace ShoppingTask.Domain;

public static class Extensions
{
    
    public static string GenerateRandomString(int stringlen)
    {
        Random rand = new Random();
        int randValue;
        StringBuilder str = new StringBuilder();
        char letter;
        for (int i = 0; i < stringlen; i++)
        {
            randValue = rand.Next(0, 64);
            letter = Convert.ToChar(randValue + 65);
            str = str.Append(letter);
        }
        return str.ToString();
    }

    public static async Task<PaginationResponseDTO<T>> PaginateAsync<T>(
       this IQueryable<T> source,
        CancellationToken cancellationToken,
        int page = 1,
        int size = 10
        
    )
        where T : class
    {
        if (page <= 0)
        {
            page = 1;
        }

        if (size <= 0)
        {
            size = 10;
        }
        var total = await source.CountAsync(cancellationToken);
        var pages = (int) Math.Ceiling((decimal)total / size);
        var result =await source.Skip((page - 1) * size).Take(size).ToListAsync(cancellationToken); // Materialize the sequence

        return new PaginationResponseDTO<T>(Values: result, Pages: pages);
    }

    

}
