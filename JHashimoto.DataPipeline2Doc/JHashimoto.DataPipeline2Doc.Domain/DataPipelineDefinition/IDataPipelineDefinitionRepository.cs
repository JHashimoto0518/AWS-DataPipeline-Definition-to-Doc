using System;
using System.Collections.Generic;
using System.Text;

namespace JHashimoto.DataPipeline2Doc.Domain.DataPipelineDefinition {
    public interface IDataPipelineDefinitionRepository {
        string FindById(string key);
    }
}
