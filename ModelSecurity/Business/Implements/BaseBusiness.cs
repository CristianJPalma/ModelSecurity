using AutoMapper;
using Data.Interfaces;
using Entity.DTOs;
using Entity.Model;
using Microsoft.Extensions.Logging;

namespace Business.Implements
{
    public class BaseBusiness<T, D> : ABaseBusiness<T, D>
        where T : BaseModel
        where D : BaseDto
    {
        protected readonly IMapper _mapper;
        protected readonly IBaseData<T> _data;
        protected readonly ILogger<BaseBusiness<T,D>> _logger;

        public BaseBusiness(IBaseData<T> data, IMapper mapper, ILogger<BaseBusiness<T,D>> logger)
        {
            _data = data;
            _mapper = mapper;
            _logger = logger;
        }

        public override async Task<IEnumerable<D>> GetAllAsync()
        {
            try
            {
                var entities = await _data.GetAllAsync();
                return _mapper.Map<List<D>>(entities.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en {nameof(GetAllAsync)}");
                throw;
            }
        }

        public override async Task<D> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _data.GetByIdAsync(id);
                if (entity == null)
                    return null;
                return _mapper.Map<D>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en {nameof(GetByIdAsync)} con Id={id}");
                throw;
            }
        }

        public override async Task<D> CreateAsync(D dto)
        {
            try
            {
                var entity = _mapper.Map<T>(dto);
                await _data.AddAsync(entity);
                var created = await _data.GetByIdAsync(entity.Id);
                 return _mapper.Map<D>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en {nameof(CreateAsync)}");
                throw;
            }
        }

        public override async Task<D> UpdateAsync(D dto)
        {
            try
            {
                var entity = _mapper.Map<T>(dto);
                _data.Update(entity);
                // No hay método async, pero podemos hacer await Task.CompletedTask para mantener async signature
                await Task.CompletedTask;
                var updated = await _data.GetByIdAsync(entity.Id);
                return _mapper.Map<D>(updated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en {nameof(UpdateAsync)}");
                throw;
            }
        }

        public override async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await _data.GetByIdAsync(id);
                if (entity == null)
                    return false;
                _data.Delete(entity);
                await Task.CompletedTask;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en {nameof(DeleteAsync)} con Id={id}");
                throw;
            }
        }
    }
}