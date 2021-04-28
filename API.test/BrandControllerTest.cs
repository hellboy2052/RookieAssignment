using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Data;
using API.Services.Brands;
using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Moq;
using ShareVM;
using Xunit;

namespace API.test
{
    public class BrandControllerTest
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
                var brands = new List<Brand>{
                    new Brand{
                        Name = "Asus"
                    },
                    new Brand{
                        Name = "Dell"
                    },
                    new Brand{
                        Name = "Nike"
                    },
                };
                databaseContext.Brands.AddRange(brands);
                var result = await databaseContext.SaveChangesAsync() <= 0;

                if (result) throw new Exception("Database not changed");


            }
            return databaseContext;
        }

        [Fact]
        public async Task GetBrands()
        {
            var dbContenxt = await GetDatabaseContext();

            // var brandvms = new List<BrandVm>();
            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<List<Brand>, List<BrandVm>>(It.IsAny<List<Brand>>()))
                .Returns((List<Brand> src) =>
                    src.Select(x => new BrandVm { Id = x.Id, Name = x.Name })
                    .ToList());

            var query = new List.Query();

            var handler = new List.Handler(dbContenxt, mapper.Object);

            var result = handler.Handle(query, new CancellationToken());

            var actionResult = Assert.IsType<List<BrandVm>>(result.Result.Value);

            Assert.NotEmpty(actionResult);
        }

        [Fact]
        public async Task GetBrand()
        {
            var dbContenxt = await GetDatabaseContext();

            var mapper = new Mock<IMapper>();
            // var brandvms = new BrandVm();
            mapper.Setup(m => m.Map<Brand, BrandVm>(It.IsAny<Brand>())).Returns((Brand src) => new BrandVm { Id = src.Id, Name = src.Name });

            var query = new Detail.Query() { Id = 1 };

            var handler = new Detail.Handler(dbContenxt, mapper.Object);

            var result = handler.Handle(query, new CancellationToken());

            var actionResult = Assert.IsType<BrandVm>(result.Result.Value);

            Assert.Matches("Asus", actionResult.Name);
        }
    }

}
