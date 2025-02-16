using Application.DTO;
using Core.Entities;
using Core.IRepositories;
using Infrastructure.Repositories.Caching;
using Microsoft.Extensions.Options;

namespace Shop.Middlewares
{
    public class SeedUsersMiddleware
    {
        private readonly RequestDelegate _next;
        private RedisKeysOptions redisKeysOptions;
        public SeedUsersMiddleware(RequestDelegate next, IOptions<RedisKeysOptions> redisKeysOptions)
        {
            _next = next;
            this.redisKeysOptions = redisKeysOptions.Value;
        }

        public async Task Invoke(HttpContext httpContext, IRedisCacheService cacheService, IUnitOfWork unitOfWork)
        {
            var cacheUsers = await cacheService.GetAsync<List<User>>(redisKeysOptions.AllUser);

            if (cacheUsers == null || cacheUsers.Count == 0)
            {
                var users = await unitOfWork.Users.GetAllDapperAsync();
                if (users != null && users.Any())
                {
                    await cacheService.SetAsync(redisKeysOptions.AllUser, users, TimeSpan.FromHours(24));
                }
            }

            await _next(httpContext);
        }
    }
}
