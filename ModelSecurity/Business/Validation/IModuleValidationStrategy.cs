using Entity.DTOs;

public interface IModuleValidationStrategy
{
    void Validate(ModuleDto moduleDto);
}