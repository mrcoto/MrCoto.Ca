using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Services;
using MrCoto.Ca.Domain.Common.Entities;
using MrCoto.Ca.Domain.Modules.GeneralModule.Constants;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;
using MrCoto.Ca.Infrastructure;

namespace MrCoto.Ca.WebApiTests.Configuration
{
    public class TestUtil
    {
        private readonly IServiceProvider _serviceProvider;

        public TestUtil(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public HttpContent AsJsonContent<T>(T data)
        {
            var json = JsonSerializer.Serialize(data);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
        
        public T Deserialize<T>(string json)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            return JsonSerializer.Deserialize<T>(json, options);
        }

        public CaContext GetDbContext() => _serviceProvider.GetRequiredService<CaContext>();

        public async Task SaveEntity<T>(T entity) where T : class
        {
            var db = GetDbContext();
            await db.Set<T>().AddAsync(entity);
            await db.SaveChangesAsync();
        }
        
        public async Task SaveEntities(params Object[] entities)
        {
            var db = GetDbContext();
            foreach (var entity in entities)
            {
                if (entity is IList list)
                {
                    foreach (var entry in list)
                    {
                        await db.AddAsync(entry);
                    }
                }
                else
                {
                    await db.AddAsync(entity);
                }
            }
            await db.SaveChangesAsync();
        }

        public async Task<bool> IsSoftDeleted<T>(T entity) where T : Entity<long>, ISoftDeletable
        {
            var db = GetDbContext();
            var dbEntity = await db.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == entity.Id);
            return dbEntity.DeletedAt != null;
        }

        public async Task<User> GetUser(long id = 0L)
        {
            id = id > 0 ? id : GeneralConstants.DefaultId;
            return await GetDbContext().Users.FirstOrDefaultAsync(x => x.Id == id);
        }
        
        public async Task<AuthenticationHeaderValue> GetAccessTokenHeader(long id = 0L)
        {
            var accessTokenService = _serviceProvider.GetRequiredService<IAccessTokenService>();
            var user = await GetUser(id);
            var accessToken = await accessTokenService.GetAccessToken(user);
            return new AuthenticationHeaderValue("Bearer", accessToken.Token);
        }
        
    }
}