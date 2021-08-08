namespace Ant.Todo.Api.Database.Configurations
{
    public static class TodoConstants
    {
        public static class UserId
        {
            public const int MaxLength = 50;
            public const int MinLenght = 5;
        }
        
        public static class Title
        {
            public const int MaxLength = 25;
            public const int MinLength = 1;
        }
        
        public static class Description
        {
            public const int MaxLength = 100;
        }
    }
}