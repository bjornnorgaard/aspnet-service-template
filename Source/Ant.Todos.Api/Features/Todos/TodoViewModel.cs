using System;

namespace Ant.Todos.Api.Features.Todos
{
    public class TodoViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}