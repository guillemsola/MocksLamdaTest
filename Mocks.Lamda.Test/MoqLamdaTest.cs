// -----------------------------------------------------------------------
// <copyright file="MoqLamdaTest.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Mocks.Lamda.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;

    using Moq;

    using NUnit.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class MoqLamdaTest
    {
        [Test]
        public void Test()
        {

            var repository = new Mock <IRepository<Entity>>();
            repository.Setup(s => s.FindEntity(AreEqual<Entity>(p => p.Name == "test1")))
                      .Returns(new Entity { Name = "result1" });

            repository.Setup(s => s.FindEntity(AreEqual<Entity>(p => p.Id == new Guid())))
                      .Returns(new Entity { Name = "result2" });

            var resultA = repository.Object.FindEntity(p => p.Name == "test1");

            var resultB = repository.Object.FindEntity(p => p.Id == new Guid());

            Assert.AreEqual("result1", resultA.Name);
            Assert.AreEqual("result2", resultB.Name);
        }

        public static Expression<Func<T, bool>> AreEqual<T>(Expression<Func<T, bool>> expr)
        {
            return Match<Expression<Func<T, bool>>>
                .Create<Expression<Func<T, bool>>>(t => t.ToString() == expr.ToString());
        }
    }
}
