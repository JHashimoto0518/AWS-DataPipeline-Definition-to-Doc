using System;
using System.Collections.Generic;
using System.Text;

namespace JHashimoto.DataPipeline2Doc.Domain.DataPipelineDefinition {
    interface IDataPipelineDefinitionRepository {
        string FindById(string path);
    }
}
