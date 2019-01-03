using Microsoft.VisualStudio.TestTools.UnitTesting;
using JHashimoto.DataPipeline2Doc.InMemory;

namespace JHashimoto.DataPipeline2Doc.Tests {
    [TestClass]
    public class DataPipelineDefinitionRepositoryTest {
        [TestMethod]
        public void GetJsonTest() {
            var repository = new InMemoryDataPipelineDefinitionRepository();
            var actual = repository.FindById("test");
            var notExpected = string.Empty;
            Assert.AreNotEqual(notExpected, actual);
        }
    }
}
