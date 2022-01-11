using Data;
using Domain;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test;

namespace Services.Tests
{
    [TestClass()]
    public class ToDoServiceTests
    {
        Mock<DbSet<ToDo>> _dbSetMock = new Mock<DbSet<ToDo>>();
        Mock<ToDoDbContext> _dbContextMock = new Mock<ToDoDbContext>();

        public ToDoServiceTests()
        {

        }

        [TestMethod()]
        public async Task GetAllTest()
        {
            var data = DataMocks.GetMockToDos();

            _dbSetMock.As<IDbAsyncEnumerable<ToDo>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<ToDo>(data.GetEnumerator()));

            _dbSetMock.As<IQueryable<ToDo>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<ToDo>(data.Provider));

            _dbSetMock.As<IQueryable<ToDo>>().Setup(x => x.Expression).Returns(data.Expression);
            _dbSetMock.As<IQueryable<ToDo>>().Setup(x => x.ElementType).Returns(data.ElementType);
            _dbSetMock.As<IQueryable<ToDo>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            _dbContextMock.Setup(x => x.ToDos).Returns(_dbSetMock.Object);

            var sut = new ToDoService(_dbContextMock.Object);
            var toDos = await sut.GetAll();

            toDos.Count().Should().Be(3);
        }

        [TestMethod()]
        public async Task GetByIdTest()
        {
            var data = DataMocks.GetMockToDos();

            _dbSetMock.As<IDbAsyncEnumerable<ToDo>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<ToDo>(data.GetEnumerator()));

            _dbSetMock.As<IQueryable<ToDo>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<ToDo>(data.Provider));

            _dbSetMock.As<IQueryable<ToDo>>().Setup(x => x.Expression).Returns(data.Expression);
            _dbSetMock.As<IQueryable<ToDo>>().Setup(x => x.ElementType).Returns(data.ElementType);
            _dbSetMock.As<IQueryable<ToDo>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            _dbContextMock.Setup(x => x.ToDos).Returns(_dbSetMock.Object);

            var sut = new ToDoService(_dbContextMock.Object);
            var toDo = await sut.GetById("79a34314-818a-4c40-aa20-f0cc1e5ee3ad");

            toDo.Title.Should().Be("Test ToDo One");
        }

        [TestMethod()]
        public async Task CreateTest()
        {
            _dbContextMock.Setup(x => x.ToDos).Returns(_dbSetMock.Object);

            var sut = new ToDoService(_dbContextMock.Object);
            await sut.Create(new ToDoDto { Title = "New ToDo Item" });

            _dbSetMock.Verify(x => x.Add(It.IsAny<ToDo>()), Times.Once());
        }

        [TestMethod()]
        public async Task UpdateTest()
        {
            var data = DataMocks.ToDoDto();

            _dbContextMock.Setup(x => x.ToDos).Returns(_dbSetMock.Object);

            var sut = new ToDoService(_dbContextMock.Object);
            await sut.Update(data);

            _dbSetMock.Verify(x => x.Attach(It.IsAny<ToDo>()), Times.Once());
        }

        [TestMethod()]
        public async Task DeleteTest()
        {
            var data = DataMocks.ToDoDto();

            _dbContextMock.Setup(x => x.ToDos).Returns(_dbSetMock.Object);

            var sut = new ToDoService(_dbContextMock.Object);
            await sut.Delete("79a34314-818a-4c40-aa20-f0cc1e5ee3ad");

            _dbSetMock.Verify(x => x.Remove(It.IsAny<ToDo>()), Times.Once());
        }
    }
}