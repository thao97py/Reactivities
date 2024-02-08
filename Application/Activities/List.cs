using MediatR;
using Persistence;
using Microsoft.Extensions.Logging;
using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Application.Interfaces;
namespace Application.Activities
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<ActivityDto>>>
        {
            public ActivityParams Params {get;set;}
        }
        public class Handler : IRequestHandler<Query, Result<PagedList<ActivityDto>>>
        {
            private readonly DataContext _context;
            private readonly ILogger<List> _logger;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;
            public Handler(DataContext context, ILogger<List> logger, IMapper mapper, IUserAccessor userAccessor){
                _userAccessor = userAccessor;
                _mapper = mapper;
                _logger = logger;
                _context = context;
            }
            public async Task<Result<PagedList<ActivityDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query =  _context.Activities
                    .Where(d => d.Date >= request.Params.StartDate )
                    .OrderBy( d=> d.Date)
                    .ProjectTo<ActivityDto>(_mapper.ConfigurationProvider, 
                        new{currentUserName = _userAccessor.GetUserName()})
                    .AsQueryable();
                
                if(request.Params.IsGoing && !request.Params.IsHost)
                {
                    query = query.Where( x => x.Attendees.Any( a=> a.Username == _userAccessor.GetUserName()));
                }

                if(request.Params.IsHost && !request.Params.IsGoing)
                {
                    query = query.Where( x => x.HostUsername == _userAccessor.GetUserName());
                }

                return Result<PagedList<ActivityDto>>.Success(
                    await PagedList<ActivityDto>.CreateAsync(query, request.Params.PageNumber, 
                        request.Params.PageSize)
                );
            }
        }
    }
}