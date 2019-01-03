using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataPipeline2Doc.Domain.DataPipelineDefinition;

namespace DataPipeline2Doc.Tests {
    [TestClass]
    public class DataPipelineDefinitionRepositoryTest {
        [TestMethod]
        public void GetJsonTest() {
            var repository = new DataPipelineDefinitionRepository();
            var actual = repository.GetJson();
            var expected = "test:aaa";
            Assert.AreEqual(expected, actual);
        }
    }
}
