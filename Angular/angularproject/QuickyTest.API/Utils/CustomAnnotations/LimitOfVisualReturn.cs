using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using QuickyTest.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace QuickyTest.API.Utils.CustomAnnotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class LimitOfVisualReturn : ValidationAttribute
    {
        private readonly string _retornoVisual;

        public LimitOfVisualReturn(string retornoVisual)
        {
            _retornoVisual = retornoVisual;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var outraPropriedadeInfo = validationContext.ObjectType.GetProperty(_retornoVisual);

            if (outraPropriedadeInfo == null)
                return new ValidationResult($"A propriedade {_retornoVisual} não foi encontrada.");


            var outraPropriedadeValue = (bool?) outraPropriedadeInfo.GetValue(validationContext.ObjectInstance);

            if (outraPropriedadeValue is null)
                return new ValidationResult("A propriedade adicionada é nula.");

            if (value is not ICollection<Prompt>)
                return new ValidationResult("O objeto não é um ICollection<Prompt>");


            var collection = (ICollection<Prompt>) value;

            if (outraPropriedadeValue.Value && collection.Count > 1)
                return new ValidationResult("Você só pode criar uma única prova quando o retorno visual está ativo.");

            if (outraPropriedadeValue.Value && collection.Count < 1 || !outraPropriedadeValue.Value && collection.Count < 1)
                return new ValidationResult("Você precisa criar ao menos uma prova.");

            return ValidationResult.Success;
        }
    }
}
