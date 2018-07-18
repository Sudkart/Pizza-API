using STM.Core.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STMAPI.ViewModels
{
    public static class ScenarioResultViewModelMapper
    {

        public static ScenarioResultModel ToViewModel(this ScenarioResult entity)
        {

            return new ScenarioResultModel
            {
                ScenarioId = entity.ScenarioId,
                SubSenarioId= entity.SubScenarioId,
                TestStepId = entity.TestStepId,
                Status = entity.Status,
                TestGroupId= entity.TestGroupId
            };
        }

    }
}
