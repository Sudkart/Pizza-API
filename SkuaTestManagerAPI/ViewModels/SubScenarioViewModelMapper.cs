using STM.Core.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STMAPI.ViewModels
{
    public static class SubScenarioViewModelMapper
    {

        public static SubScenarioViewModel ToViewModel(this SubScenario entity)
        {

            return new SubScenarioViewModel
            {
                Sub_Scen_Id = entity.Sub_Scen_Id,
                Scen_Id = entity.Scen_Id,
                ProjectId = entity.ProjectId,
                SubScenarioId = entity.SubScenarioId,
				ScenarioName = entity.ScenarioName,
				SubScenarioName = entity.SubScenarioName,
                SubScenarioDesc = entity.SubScenarioDescription,
                RunMode = entity.RunMode,
                Status = entity.Status,
                Active = entity.Active,
                IsAutomated = entity.IsAutomated,
                IsCompleted = entity.IsCompleted,
                TestSubScenExecDate = entity.TestSubScenExecDate,
                TestSubScenExecBy = entity.TestSubScenExecBy,
                TestSubScenLastStatus = entity.TestSubScenLastStatus,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate
               
            };
        }

        public static SubScenario ToEntity(this SubScenarioViewModel viewModel)
        {
            return new SubScenario
            {
               Sub_Scen_Id = viewModel.Sub_Scen_Id,
               Scen_Id = viewModel.Scen_Id,
               ProjectId = viewModel.ProjectId,
               SubScenarioId = viewModel.SubScenarioId,
               SubScenarioName= viewModel.SubScenarioName,
               SubScenarioDescription = viewModel.SubScenarioDesc,
               RunMode= viewModel.RunMode,
               Status = viewModel.Status,
               Active = viewModel.Active,
               IsAutomated = viewModel.IsAutomated,
               IsCompleted = viewModel.IsCompleted,
               TestSubScenExecDate = viewModel.TestSubScenExecDate,
               TestSubScenExecBy = viewModel.TestSubScenExecBy,
               TestSubScenLastStatus = viewModel.TestSubScenLastStatus,
               CreatedBy = viewModel.CreatedBy,
               CreatedDate = viewModel.CreatedDate,
               UpdatedBy = viewModel.UpdatedBy,
               UpdatedDate = viewModel.UpdatedDate
             };
        }









    }
}
