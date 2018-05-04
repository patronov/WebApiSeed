using Autofac.Extras.Moq;
using SculptorWebApi.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SculptorWebApi.Tests.Services
{
    public abstract class ServiceTestsBase<ITestedService, CurrentContext> where CurrentContext : DbContext, new()
    {
        protected AutoMock Mock { get; set; }
        protected DbContext Context { get; set; }
        protected TestUnitOfWork Unit { get; set; }
        protected ITestedService TestedService { get; set; }

        public ServiceTestsBase()
        {
            Mock = AutoMock.GetLoose();
            Context = new CurrentContext();
            Unit = new TestUnitOfWork(Context);
            Mock.Provide<IUnitOfWork>(Unit);
            SetupMock();
            TestedService = Mock.Create<ITestedService>();
        }

        protected abstract void SetupMock();
    }
}
