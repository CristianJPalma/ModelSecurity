using System.ComponentModel.DataAnnotations;
using Entity.DTOs;

public class DefaultFormValidationStrategy : IFormValidationStrategy
{
    public void Validate(FormDto formDto)
    {
        if (formDto == null)
            throw new ValidationException("El objeto formulario no puede ser nulo");
        if (string.IsNullOrWhiteSpace(formDto.Code))
            throw new ValidationException("El Code del formulario es obligatorio");
    }
}