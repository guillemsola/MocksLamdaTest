namespace Mocks.Lamda.Test
{
    using System;
    using System.Linq.Expressions;

    using NUnit.Framework;

    using Rhino.Mocks;

    [TestFixture]
    public class RhinoLamdaTest
    {
        [Test]
        public void LamdaAnyArgumentTest()
        {
            var repository = MockRepository.GenerateStub<IRepository<Entity>>();
            repository.Stub(r => r.Find(AreEqual<Entity>(p => p.Name == "test1")))
                .Return("result1");
            repository.Stub(r => r.Find(AreEqual<Entity>(p => p.Name == "test2")))
                .Return("result2");

            var result1 = repository.Find(p => p.Name == "test1");
            var result2 = repository.Find(p => p.Name == "test2");

            Assert.AreEqual("result1", result1);
            Assert.AreEqual("result2", result2);
        }

        [Test]
        public void ToStringLamdaMockTest()
        {
            var repository = MockRepository.GenerateStub<IRepository<Entity>>();
            var a = new Func<Entity, bool>(d => d.Name == "test1");
            var b = new Func<Entity, bool>(d => d.Name == "test1");

            repository.Stub(r => r.FindEntity(AreEqual<Entity>(p => p.Name == "test1")))
                      .Return(new Entity { Name = "result1" });

            repository.Stub(r => r.FindEntity(AreEqual<Entity>(p => p.Name == "test2")))
                      .Return(new Entity { Name = "result2" });
            
            var result1 = repository.FindEntity(p => p.Name == "test1");
            var result2 = repository.FindEntity(p => p.Name == "test2");

            Assert.AreEqual("result1", result1.Name);
            Assert.AreEqual("result2", result2.Name);
        }

        public static Expression<Func<T, bool>> AreEqual<T>(Expression<Func<T, bool>> expr)
        {
            return Arg<Expression<Func<T, bool>>>.Matches(t => t.ToString() == expr.ToString());
        }
    }
}
