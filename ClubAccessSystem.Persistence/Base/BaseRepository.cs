using AutoMapper;
using ClubAccessSystem.Domain.Repositories;
using ClubAccessSystem.Domain.Result;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;


namespace ClubAccessSystem.Persistence.Base
{
    public class BaseRepository<TEntity, TData> : IBaseRepository<TEntity, TData> where TEntity : class
    {
        private readonly DbContext _dbcontext;
        private DbSet<TEntity> _entities;
        private readonly IMapper _mapper;

        public BaseRepository(DbContext dbContext, IMapper mapper)
        {
            _dbcontext = dbContext;
            _entities = _dbcontext.Set<TEntity>();
            _mapper = mapper;
        }

        public async Task<OperationResult<TData>> Delete(TEntity entity)
        {
            if (entity == null) return new OperationResult<TData> { Success = false, Message = "Entity cannot be null" };
            return await SafeExecuteAsync(async () =>
            {
                _entities.Remove(entity);
                await _dbcontext.SaveChangesAsync();
                return new OperationResult<TData>
                {
                    Success = true,
                    Message = "Entity deleted successfully."
                };
            });
        }

        public async Task<OperationResult<List<TData>>> GetAll()
        {
            return await SafeExecuteAsync<List<TData>>(async () => // Especifica el tipo genérico
            {
                var entities = await _entities.ToListAsync();
                var data = MapEntitiesToData(entities);
                return new OperationResult<List<TData>>
                {
                    Success = true,
                    Data = data // Ahora es compatible
                };
            });
        }

        public async Task<OperationResult<TData>> GetById(int id)
        {
            if (id == 0) return new OperationResult<TData> { Success = false, Message = "ID cannot be null" };

            return await SafeExecuteAsync(async () =>
            {
                var entity = await _entities.FindAsync(id);
                if (entity == null)
                {
                    return new OperationResult<TData>
                    {
                        Success = false,
                        Message = "Entity not found."
                    };
                }
                var data = MapEntityToData(entity); // Mapear la entidad a TData
                return new OperationResult<TData>
                {
                    Success = true,
                    Data = data,
                    Message = "Entity retrieved successfully."
                };
            });
        }

        public async Task<OperationResult<TData>> Save(TEntity entity)
        {
            if (entity == null) return new OperationResult<TData> { Success = false, Message = "Entity cannot be null" };

            return await SafeExecuteAsync(async () =>
            {
                await _entities.AddAsync(entity);
                await _dbcontext.SaveChangesAsync();
                return new OperationResult<TData>
                {
                    Success = true,
                    Message = "Entity saved successfully."
                };
            });
        }

        public async Task<OperationResult<TData>> Update(TEntity entity)
        {
            if (entity == null) return new OperationResult<TData> { Success = false, Message = "Entity cannot be null" };

            return await SafeExecuteAsync(async () =>
            {
                _dbcontext.ChangeTracker.Clear();
                // Desconectar cualquier entidad existente con el mismo ID
                var entry = _dbcontext.Entry(entity);
                if (entry.State == EntityState.Detached)
                {
                    _entities.Attach(entity);
                }

                entry.State = EntityState.Modified;
                await _dbcontext.SaveChangesAsync();

                var data = MapEntityToData(entity);
                return new OperationResult<TData>
                {
                    Success = true,
                    Message = "Entity updated successfully.",
                    Data = data
                };
            });
        }

        public async Task<OperationResult<TResult>> SafeExecuteAsync<TResult>(Func<Task<OperationResult<TResult>>> operation)
        {
            try
            {
                return await operation();
            }
            catch (MySqlException ex)
            {
                return new OperationResult<TResult>
                {
                    Success = false,
                    Message = $"A MySQL error occurred: {ex.Message}"
                };
            }
            catch (DbUpdateException ex)
            {
                return new OperationResult<TResult>
                {
                    Success = false,
                    Message = $"An error occurred while updating the database: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<TResult>
                {
                    Success = false,
                    Message = $"An unexpected error occurred: {ex.Message}"
                };
            }
        }

        // Método para mapear una entidad a TData
        private TData MapEntityToData(TEntity entity)
        {
            return _mapper.Map<TData>(entity);
        }

        // Método para mapear una lista de entidades a List<TData>
        private List<TData> MapEntitiesToData(List<TEntity> entities)
        {
            return _mapper.Map<List<TData>>(entities);
        }
    }
}