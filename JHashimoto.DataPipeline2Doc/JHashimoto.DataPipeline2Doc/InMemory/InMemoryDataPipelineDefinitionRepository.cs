using System;
using System.Collections.Generic;
using System.Text;
using JHashimoto.DataPipeline2Doc.Domain.DataPipelineDefinition;

namespace JHashimoto.DataPipeline2Doc.InMemory {
    internal class InMemoryDataPipelineDefinitionRepository : IDataPipelineDefinitionRepository {
        public string FindById(string path) {
            return "test:aaa";
        }
    }
}
