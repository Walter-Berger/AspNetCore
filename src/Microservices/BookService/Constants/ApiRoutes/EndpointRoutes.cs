namespace BookService.Constants.ApiRoutes;

public class EndpointRoutes
{
    public class Book
    {
        public const string Base = "/api/books";

        public const string Create = Base;
        public const string GetAll = Base;
        public const string Get = $"{Base}/{{id:Guid}}";
        public const string Update = $"{Base}/{{id:Guid}}";
        public const string Delete = $"{Base}/{{id:Guid}}";
        public const string Loan = $"{Base}/{{id:Guid}}/loan";
        public const string Buy = $"{Base}/{{id:Guid}}/buy";
    }
}