using MediatR;
using Reliance.Web.Client.Api;
using Reliance.Web.Domain;
using SnowStorm.Infrastructure.QueryExecutors;
using System.Threading;
using System.Threading.Tasks;

namespace Reliance.Web.Services.Commands
{
    public class PostRepositryDetailsCommand : IRequest<bool>
    {
        public PostRepositoryDetailsDto Data;

        public PostRepositryDetailsCommand(PostRepositoryDetailsDto data)
        {
            Data = data;
        }
    }

    public class PostRepositryDetailsCommandHandler : IRequestHandler<PostRepositryDetailsCommand, bool>
    {
        private readonly IQueryExecutor _executor;
        //private readonly IUserContext _userContext;

        public PostRepositryDetailsCommandHandler(IQueryExecutor executor)
        {
            _executor = executor;
            //_userContext = userContext;
        }

        public async Task<bool> Handle(PostRepositryDetailsCommand message, CancellationToken cancellationToken)
        {
            var r = await Repository.Create(_executor, message.Data.RepositoryName, message.Data.OwnerId);

            foreach (var sDto in message.Data.Solutions)
            {
                var s = await Solution.Create(_executor, sDto.SolutionName, r.Id);

                foreach (var pDto in sDto.Projects)
                {
                    var p = await Project.Create(_executor, pDto.ProjectName, s.Id);

                    foreach (var pkgDto in pDto.Packages)
                    {
                        var pkg = await Package.Create(_executor, pkgDto);

                        var pp = await ProjectPackage.Create(_executor, p.Id, pkg.Id);
                    }
                }
            }

            return true;
        }
    }
}
