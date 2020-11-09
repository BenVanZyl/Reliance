using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Reliance.Web.ThisApp.Infrastructure.QueryExecutors
{
    public interface IQueryExecutor
    {
        Task<T> Execute<T>(IQueryResult<T> query);
        Task<List<T>> Execute<T>(IQueryResultList<T> query) where T : class, SnowStorm.Infrastructure.Domain.IDomainEntity;
        Task<List<CastDto>> Execute<T, CastDto>(IQueryResultList<T> query) where T : class, SnowStorm.Infrastructure.Domain.IDomainEntity;
        Task<T> Execute<T>(IQueryResultSingle<T> query, bool defaultIfMissing = true) where T : class, SnowStorm.Infrastructure.Domain.IDomainEntity;
        Task<CastDto> Execute<T, CastDto>(IQueryResultSingle<T> query, bool defaultIfMissing = true) where T : class, SnowStorm.Infrastructure.Domain.IDomainEntity;

        ICastingQueryExecutor<TDto> CastTo<TDto>();

        Task<T> Add<T>(T domainEntity, bool saveChanges = true) where T : class, SnowStorm.Infrastructure.Domain.IDomainEntity;
        Task<bool> Delete<T>(T domainEntity, bool saveChanges = true) where T : class, SnowStorm.Infrastructure.Domain.IDomainEntity;
        Task Save();
    }

    public interface IQueryResult<T>
    {
        Task<T> Execute(SnowStorm.Infrastructure.QueryExecutors.IQueryableProvider queryableProvider);
    }

    public interface IQueryResultList<out T> where T : class, SnowStorm.Infrastructure.Domain.IDomainEntity
    {
        IQueryable<T> Execute(SnowStorm.Infrastructure.QueryExecutors.IQueryableProvider queryableProvider);
    }

    public interface IQueryResultSingle<out T> where T : class, SnowStorm.Infrastructure.Domain.IDomainEntity
    {
        IQueryable<T> Execute(SnowStorm.Infrastructure.QueryExecutors.IQueryableProvider queryableProvider);
    }

    public interface ICastingQueryExecutor<TDto>
    {
        Task<List<TDto>> Execute<T>(IQueryResultList<T> query) where T : class, SnowStorm.Infrastructure.Domain.IDomainEntity;
        Task<TDto> Execute<T>(IQueryResultSingle<T> query, bool defaultIfMissing = true) where T : class, SnowStorm.Infrastructure.Domain.IDomainEntity;

        //Task<TDto> GetForId<T>(long id, Func<IQueryable<T>, IQueryable<T>> includes) where T : class, IDomainEntityWithId;
    }


    public class QueryExecutor : IQueryExecutor
    {
        private readonly SnowStorm.Infrastructure.Domain.AppDbContext _dbContext;
        private readonly SnowStorm.Infrastructure.QueryExecutors.IQueryableProvider _queryableProvider;
        private readonly IMapper _mapper;

        public QueryExecutor(SnowStorm.Infrastructure.Domain.AppDbContext dbContext, SnowStorm.Infrastructure.QueryExecutors.IQueryableProvider queryableProvider, IMapper mapper)
        {
            _dbContext = dbContext;
            _queryableProvider = queryableProvider;
            _mapper = mapper;
        }

        public Task<T> Execute<T>(IQueryResult<T> query)
        {
            return Execute(() => query.Execute(_queryableProvider), _dbContext, query);
        }

        public Task<List<T>> Execute<T>(IQueryResultList<T> query) where T : class, SnowStorm.Infrastructure.Domain.IDomainEntity
        {
            return Execute(() => query.Execute(_queryableProvider).ToListAsync(), _dbContext, query);
        }

        //automapper
        public Task<List<CastDto>> Execute<T, CastDto>(IQueryResultList<T> query) where T : class, SnowStorm.Infrastructure.Domain.IDomainEntity
        {
            var q = query.Execute(_queryableProvider)
                    .ProjectTo<CastDto>(_mapper.ConfigurationProvider);
            return q.ToListAsync();
        }

        public Task<T> Execute<T>(IQueryResultSingle<T> query, bool defaultIfMissing = true) where T : class, SnowStorm.Infrastructure.Domain.IDomainEntity
        {
            return Execute(async () =>
            {
                var result = await query.Execute(_queryableProvider).FirstOrDefaultAsync();
                if (!defaultIfMissing && result == null)
                    throw new Exception($"'{typeof(T).Name}': Status404 - NotFound");

                return result;
            }, _dbContext, query);
        }

        //automapper
        public Task<CastDto> Execute<T, CastDto>(IQueryResultSingle<T> query, bool defaultIfMissing = true) where T : class, SnowStorm.Infrastructure.Domain.IDomainEntity
        {
            return Execute(async () =>
            {
                var result = await query.Execute(_queryableProvider)
                        .ProjectTo<CastDto>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();

                if (!defaultIfMissing && result == null)
                    throw new Exception($"'{typeof(T).Name}': Status404 - NotFound");

                return result;
            }, _dbContext, query);
        }

        public ICastingQueryExecutor<TDto> CastTo<TDto>()
        {
            return new CastingBuilder<TDto>(_dbContext, _queryableProvider, _mapper);
        }

        internal static async Task<T> Execute<T>(Func<Task<T>> getResult, DbContext dbContext, object query)
        {
            var stopwatch = new System.Diagnostics.Stopwatch();
            //var stringBuilder = new StringBuilder();
            try
            {
                stopwatch.Start(); //TODO: log query time events...
                var result = await getResult();
                stopwatch.Stop();

                return result;
            }
            catch (Exception ex)
            {
                //TODO: log errors
                stopwatch.Stop();
                throw ex;
            }
        }

        Task<T> IQueryExecutor.Add<T>(T domainEntity, bool saveChanges)
        {
            throw new NotImplementedException();
        }

        Task<bool> IQueryExecutor.Delete<T>(T domainEntity, bool saveChanges)
        {
            throw new NotImplementedException();
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }
    }

    internal class CastingBuilder<TDto> : ICastingQueryExecutor<TDto>
    {
        private readonly DbContext _dbContext;
        private readonly SnowStorm.Infrastructure.QueryExecutors.IQueryableProvider _queryableProvider;
        private readonly IMapper _mapper;


        public CastingBuilder(DbContext dbContext, SnowStorm.Infrastructure.QueryExecutors.IQueryableProvider queryableProvider, IMapper mapper)
        {
            _dbContext = dbContext;
            _queryableProvider = queryableProvider;
            _mapper = mapper;
        }

        public Task<List<TDto>> Execute<T>(IQueryResultList<T> query) where T : class, SnowStorm.Infrastructure.Domain.IDomainEntity //, Expression<Func<TDto, TKeyBy>> orderBy, SortOrder sortOrder)
        {
            return QueryExecutor.Execute( () =>
            {
                var queryable = query.Execute(_queryableProvider).ProjectTo<TDto>(_mapper.ConfigurationProvider);

                //if (orderBy != null)
                //    queryable = sortOrder == SortOrder.Descending ? queryable.OrderByDescending(orderBy) : queryable.OrderBy(orderBy);

                return queryable.ToListAsync();

            }, _dbContext, query);
        }

        public Task<TDto> Execute<T>(IQueryResultSingle<T> query, bool defaultIfMissing) where T : class, SnowStorm.Infrastructure.Domain.IDomainEntity
        {
            try
            {
                return QueryExecutor.Execute(async () =>
                {
                    var result = await query.Execute(_queryableProvider).ProjectTo<TDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();

                    if (!defaultIfMissing && result == null)
                        throw new Exception($"Error executing projectable single item query over '{typeof(T).Name}' (with no default if missing): no results returned");

                    return result;

                }, _dbContext, query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
