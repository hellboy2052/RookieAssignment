using System.Threading;
using System.Threading.Tasks;
using API.Data;
using API.Services.Security;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShareVM;

namespace API.Services.Profiles
{
    public class Detail
    {
        public class Query : IRequest<ResultVm<ProfileVm>>
        {
            public string Username { get; set; }
        }

        public class Handler : IRequestHandler<Query, ResultVm<ProfileVm>>
        {
            private readonly IMapper _mapper;
            private readonly MyDbContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(MyDbContext context, IMapper mapper, IUserAccessor userAccessor)
            {
                this._userAccessor = userAccessor;
                this._context = context;
                this._mapper = mapper;
            }

            public async Task<ResultVm<ProfileVm>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _context.Users
                    .ProjectTo<ProfileVm>(_mapper.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .SingleOrDefaultAsync(x => x.Username == request.Username);
                if (user == null) return null;

                return ResultVm<ProfileVm>.Success(user);
            }
        }
    }
}