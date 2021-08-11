using System;
using System.Linq.Expressions;
using Ant.Todos.Api.Database.Models;
using Humanizer;

namespace Ant.Todos.Api.Features.Todos
{
    public static class TodoSortExpressions
    {
        public static Expression<Func<Todo, object>> Get(string propertyName)
        {
            return propertyName?.Pascalize() switch
            {
                nameof(TodoViewModel.Id) => todo => todo.Id,
                nameof(TodoViewModel.Title) => todo => todo.Title,
                nameof(TodoViewModel.Description) => todo => todo.Description,
                nameof(TodoViewModel.IsCompleted) => todo => todo.IsCompleted,
                _ => todo => todo.Id
            };
        }
    }
}