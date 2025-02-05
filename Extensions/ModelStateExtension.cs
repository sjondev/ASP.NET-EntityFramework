﻿using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BlogApi.Extensions
{
    public static class ModelStateExtension
    {
        // Extendendo lógica para pegar erros de validação
        public static List<string> GetErrors(this ModelStateDictionary modelState)
        {
            var result = new List<string>();
            foreach (var item in modelState.Values)
                result.AddRange(item.Errors.Select(error => error.ErrorMessage));

            return result;
        }
    }
}
