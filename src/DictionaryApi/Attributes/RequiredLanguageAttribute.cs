using System.ComponentModel.DataAnnotations;
using DictionaryApi.Entities;
using DictionaryApi.Models;

namespace DictionaryApi.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class RequiredBothLanguageAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var translationModels = (value as IEnumerable<TranslationModel>)!.ToList();
        
        if(translationModels.Count == 0)
            return new ValidationResult($"{validationContext.DisplayName} is empty");

        var georgianTranslation = translationModels.FirstOrDefault(translationModel => translationModel.Language == Language.Ka);
        if (georgianTranslation is null || string.IsNullOrEmpty(georgianTranslation.Value))
            return new ValidationResult($"{validationContext.DisplayName} does not contain value for language {Language.Ka}");
        
        var englishTranslation = translationModels.FirstOrDefault(translationModel => translationModel.Language == Language.Ka);
        if (englishTranslation is null || string.IsNullOrEmpty(englishTranslation.Value))
            return new ValidationResult($"{validationContext.DisplayName} does not contain value for language {Language.Ka}");

        
        return base.IsValid(value, validationContext);
    }
}