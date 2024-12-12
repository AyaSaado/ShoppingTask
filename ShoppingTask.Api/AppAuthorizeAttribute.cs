namespace ShoppingTask.Api
{
    public class AppAuthorizeAttribute : AuthorizeAttribute
    {
        public AppAuthorizeAttribute(params Roles[] roles)
        {
            Roles = string.Join(",", roles.Select(x => x.ToString()));
            AuthenticationSchemes = "Bearer";
        }
    }
}
