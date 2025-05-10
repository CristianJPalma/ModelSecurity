using Entity.DTOs;

public interface IFormValidationStrategy
{
    void Validate(FormDto formDto);
}