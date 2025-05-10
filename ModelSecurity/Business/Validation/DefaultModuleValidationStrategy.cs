using System.ComponentModel.DataAnnotations;
using Entity.DTOs;

public class DefaultModuleValidationStrategy : IModuleValidationStrategy
{
    public void Validate(ModuleDto moduleDto)
    {
        if (moduleDto == null)
            throw new ValidationException("El objeto módulo no puede ser nulo");
        if (string.IsNullOrWhiteSpace(moduleDto.Name))
            throw new ValidationException("El Name del módulo es obligatorio");
    }
}