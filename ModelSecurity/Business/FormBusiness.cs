﻿using AutoMapper;
using Data.UnitOfWork;
using Entity.DTOs;
using Entity.Model;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

namespace Business
{
    public class FormBusiness : BaseBusiness<Form, FormDto>
    {
        private readonly IFormValidationStrategy _validationStrategy;

        public FormBusiness(
            IUnitOfWork unitOfWork,
            ILogger<FormBusiness> logger,
            IMapper mapper,
            IFormValidationStrategy validationStrategy)
            : base(unitOfWork, mapper, logger)
        {
            _validationStrategy = validationStrategy;
        }

        protected override async Task<IEnumerable<Form>> GetAllEntitiesAsync()
        {
            return await _unitOfWork.Forms.GetAllAsync();
        }

        public async Task<FormDto> CreateFormAsync(FormDto formDto)
        {
            try
            {
                _validationStrategy.Validate(formDto);
                var form = _mapper.Map<Form>(formDto);
                form.CreateAt = DateTime.Now;

                await _unitOfWork.Forms.AddAsync(form);
                await _unitOfWork.SaveAsync();

                return _mapper.Map<FormDto>(form);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear formulario: {FormNombre}", formDto?.Name ?? "null");
                throw new ExternalServiceException("Base de datos", "Error al crear el formulario", ex);
            }
        }

        public async Task<IEnumerable<FormDto>> GetAllFormsAsync()
        {
            try
            {
                var forms = await _unitOfWork.Forms.GetAllAsync();
                return _mapper.Map<IEnumerable<FormDto>>(forms); // Usar AutoMapper
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los formularios");
                throw new ExternalServiceException("Base de datos", "Error al recuperar la lista de formularios", ex);
            }
        }

        public async Task<FormDto> GetFormByIdAsync(int id)
        {
            if (id <= 0)
                throw new ValidationException("id", "El ID del formulario debe ser mayor que cero");

            try
            {
                var form = await _unitOfWork.Forms.GetByIdAsync(id);
                if (form == null)
                    throw new EntityNotFoundException("Form", id);

                return _mapper.Map<FormDto>(form); // Usar AutoMapper
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el formulario con ID: {FormId}", id);
                throw new ExternalServiceException("Base de datos", $"Error al recuperar el formulario con ID {id}", ex);
            }
        }

        public async Task UpdateFormAsync(FormDto formDto)
        {
            if (formDto == null || formDto.Id <= 0)
                throw new ValidationException("Id", "El formulario a actualizar debe tener un ID válido");

            _validationStrategy.Validate(formDto); // Usar strategy aquí

            try
            {
                var existing = await _unitOfWork.Forms.GetByIdAsync(formDto.Id);
                if (existing == null)
                    throw new EntityNotFoundException("Form", formDto.Id);

                _mapper.Map(formDto, existing); // Actualizar entidad con AutoMapper
                _unitOfWork.Forms.Update(existing);
                await _unitOfWork.SaveAsync(); // Guardar cambios en la base de datos
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar formulario con ID: {FormId}", formDto.Id);
                throw;
            }
        }

        public async Task PatchFormAsync(FormDto formDto)
        {
            if (formDto == null || formDto.Id <= 0)
                throw new ValidationException("Id", "El formulario a actualizar debe tener un ID válido");

            try
            {
                var existing = await _unitOfWork.Forms.GetByIdAsync(formDto.Id);
                if (existing == null)
                    throw new EntityNotFoundException("Form", formDto.Id);

                if (!string.IsNullOrEmpty(formDto.Name))
                    existing.Name = formDto.Name;

                if (!string.IsNullOrEmpty(formDto.Code))
                    existing.Code = formDto.Code;

                if (formDto.Active != null)
                    existing.Active = formDto.Active;

                _unitOfWork.Forms.Update(existing);
                await _unitOfWork.SaveAsync(); // Guardar cambios en la base de datos
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar parcialmente el formulario con ID: {FormId}", formDto.Id);
                throw;
            }
        }

        public async Task DisableFormAsync(int id)
        {
            if (id <= 0)
                throw new ValidationException("id", "El ID del formulario debe ser mayor que cero");

            try
            {
                var result = await _unitOfWork.Forms.DisableAsync(id);
                if (!result)
                    throw new ExternalServiceException("Base de datos", "No se pudo desactivar el formulario");

                await _unitOfWork.SaveAsync(); // Guardar cambios en la base de datos
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al desactivar formulario con ID: {FormId}", id);
                throw;
            }
        }

        public async Task DeleteFormAsync(int id)
        {
            if (id <= 0)
                throw new ValidationException("id", "El ID del formulario debe ser mayor que cero");

            try
            {
                var existing = await _unitOfWork.Forms.GetByIdAsync(id);
                if (existing == null)
                    throw new EntityNotFoundException("Form", id);

                _unitOfWork.Forms.Delete(existing);
                await _unitOfWork.SaveAsync(); // Guardar cambios en la base de datos
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar formulario con ID: {FormId}", id);
                throw;
            }
        }
    }
}