using ACM.Library;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ACM.Library.Test
{
    [TestClass]
    public class BuilderTest
    {
        #region Properties

        public TestContext TestContext { get; set; }
        #endregion

        [TestMethod()]
        public void BuildIntegerSequenceTest()
        {
            //Arrange
            Builder listBuilder = new Builder();

            //Act
            var integerSequence = listBuilder.BuildIntegerSequence();

            //Analyze
            foreach (var item in integerSequence)
            {
                TestContext.WriteLine(item.ToString());
            }

            //Assert
            Assert.IsNotNull(integerSequence);
        }

        [TestMethod()]
        public void BuildStringSequenceTest()
        {
            //Arrange
            Builder listBuilder = new Builder();

            //Act
            var stringSequence = listBuilder.BuildStringSequence();

            //Analyze
            foreach (var item in stringSequence)
            {
                TestContext.WriteLine(item);
            }

            Assert.IsNotNull(stringSequence);
        }
    }
}
