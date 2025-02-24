namespace AuthwithIdentity.Configurations
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder app) => app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

    }
}
