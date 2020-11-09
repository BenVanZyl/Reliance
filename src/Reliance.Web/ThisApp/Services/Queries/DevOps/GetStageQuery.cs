﻿using Microsoft.AspNetCore.Http;
using Reliance.Web.Client;
using Reliance.Web.ThisApp.Domain.DevOps;
using Reliance.Web.ThisApp.Infrastructure;
using SnowStorm.Infrastructure.QueryExecutors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reliance.Web.ThisApp.Services.Queries.DevOps
{
    public class GetStageQuery : IQueryResultSingle<Stage>
    {
        private readonly string _name;
        private readonly long? _orgId;
        private readonly long? _id;
        public GetStageQuery(long organisationId, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ThisAppException(StatusCodes.Status412PreconditionFailed, Messages.Err417MissingObjectData("Stage"));
            _name = name;
            _orgId = organisationId;
        }
        public GetStageQuery(long id)
        {
            _id = id;
        }
        public IQueryable<Stage> Execute(IQueryableProvider queryableProvider)
        {
            var baseQuery = queryableProvider.Query<Stage>();

            if (_id.HasValue)
                baseQuery = baseQuery.Where(w => w.Id == _id);

            if (!string.IsNullOrWhiteSpace(_name))
                baseQuery = baseQuery.Where(w => w.OrganisationId == _orgId && w.Name == _name);

            return baseQuery.AsQueryable();
        }
    }
}