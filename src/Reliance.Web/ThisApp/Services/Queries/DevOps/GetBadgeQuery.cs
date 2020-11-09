using Microsoft.EntityFrameworkCore;
using Reliance.Web.ThisApp.Domain.DevOps;
using SnowStorm.Infrastructure.QueryExecutors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reliance.Web.ThisApp.Services.Queries.DevOps
{
    public class GetBadgeQuery : IQueryResultSingle<Badge>
    {
        private long? _id = null;
        private long? _appId = null;
        private long? _stageId = null;

        public bool IncludeApp { get; set; } = false;
        public bool IncludeStage { get; set; } = false;

        public GetBadgeQuery IncludeAll()
        {
            IncludeApp = true;
            IncludeStage = true;
            return this;
        }

        public GetBadgeQuery(long id)
        {
            _id = id;
        }

        public GetBadgeQuery(long appId, long stageId)
        {
            _appId = appId;
            _stageId = stageId;
        }

        public IQueryable<Badge> Execute(IQueryableProvider queryableProvider)
        {
            var baseQuery = queryableProvider.Query<Badge>();

            if (IncludeApp)
                baseQuery = baseQuery.Include(i => i.App);

            if (IncludeStage)
                baseQuery = baseQuery.Include(i => i.Stage);

            if (_id.HasValue)
                baseQuery = baseQuery.Where(w => w.Id == _id);

            if (_appId.HasValue)
                baseQuery = baseQuery.Where(w => w.AppId == _appId && w.StageId == _stageId);

            return baseQuery.AsQueryable();
        }
    }
}
