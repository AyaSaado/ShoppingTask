namespace ShoppingTask.Domain.Jwt;

public interface IJwtProvider
{
    SecurityToken Generate(User user, List<string> roles);
}
