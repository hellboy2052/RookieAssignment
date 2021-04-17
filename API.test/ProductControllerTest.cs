using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using API.Controllers;
using API.Data;
using API.Services.Brands;
using API.Services.Security;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using ShareVM;
using Xunit;

namespace API.test
{
    public class ProductControllerTest
    {
        private async Task<MyDbContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new MyDbContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Users.CountAsync() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.Brands.Add(new Domain.Brand()
                    {
                        Name = "Asus"
                    });
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }

        [Fact]
        public async Task GetProduct()
        {
            var dbContenxt = await GetDatabaseContext();

            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<Domain.Brand, BrandVm>(It.IsAny<Domain.Brand>())).Returns(new BrandVm());

            var query = new List.Query();

            var handler = new List.Handler(dbContenxt, mapper.Object);

            var result = handler.Handle(query, new CancellationToken());

            var actionResult = Assert.IsType<List<BrandVm>>(result.Result.Value);

            Assert.NotEmpty(actionResult);




        }
    }
        
}
