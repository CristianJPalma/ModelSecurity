using AutoMapper;
using Data.UnitOfWork;
using Microsoft.Extensions.Logging;

public abstract class BaseBusiness<TEntity, TDto>
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IMapper _mapper;
    protected readonly ILogger _logger;

    protected BaseBusiness(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public virtual async Task<IEnumerable<TDto>> GetAllAsync()
    {
        var entities = await GetAllEntitiesAsync();
        return _mapper.Map<IEnumerable<TDto>>(entities);
    }

    protected abstract Task<IEnumerable<TEntity>> GetAllEntitiesAsync();
}