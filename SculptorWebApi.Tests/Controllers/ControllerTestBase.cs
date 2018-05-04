using Autofac.Extras.Moq;
using SculptorWebApi.DataAccess.Interfaces;
using SculptorWebApi.Tests.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SculptorWebApi.Tests.Controllers
{
    public abstract class ControllerTestBase<CurrentController, CurrentContext> where CurrentContext : DbContext, new()
    {
        protected AutoMock Mock { get; set; }
        protected DbContext Context { get; set; }
        protected TestUnitOfWork Unit { get; set; }
        protected CurrentController TestedController { get; set; }

        public ControllerTestBase()
        {
            Mock = AutoMock.GetLoose();
            Context = new CurrentContext();
            Unit = new TestUnitOfWork(Context);
            Mock.Provide<IUnitOfWork>(Unit);
            SetupMock();
            TestedController = Mock.Create<CurrentController>();
        }

        protected abstract void SetupMock();
    }
}
