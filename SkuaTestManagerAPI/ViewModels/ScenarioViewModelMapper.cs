using STM.Core.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STMAPI.ViewModels
{
    public static class ScenarioViewModelMapper
    {

        public static ScenarioViewModel ToViewModel(this Scenario entity)
        {

            return new ScenarioViewModel
            {
             Scen_Id = entity.Scen_Id,
             ProjectId = entity.ProjectId,
             ProjectName = entity.ProjectName,
             MainScenarioId = entity.MainScenarioId,
             MainScenarioName = entity.MainScenarioName,
             MainScenarioDescription = entity.MainScenarioDescription,
             RunMode = entity.RunMode,
             Status = entity.Status,
             Active = entity.Active,
             TestScenExecDate = entity.TestScenExecDate,
             TestScenExecBy = entity.TestScenExecBy,
             TestScenLastStatus = entity.TestScenLastStatus,
             CreatedBy = entity.CreatedBy,
             CreatedDate = entity.CreatedDate,
             UpdatedBy = entity.UpdatedBy,
             UpdatedDate = entity.UpdatedDate
            };
        }

        public static Scenario ToEntity(this ScenarioViewModel viewModel)
        {
            return new Scenario
            {
                Scen_Id = viewModel.Scen_Id,
                ProjectId = viewModel.ProjectId,
                MainScenarioId = viewModel.MainScenarioId,
                MainScenarioName = viewModel.MainScenarioName,
                MainScenarioDescription = viewModel.MainScenarioDescription,
                RunMode = viewModel.RunMode,
                Status = viewModel.Status,
                Active = viewModel.Active,
                TestScenExecDate = viewModel.TestScenExecDate,
                TestScenExecBy = viewModel.TestScenExecBy,
                CreatedBy = viewModel.CreatedBy,
                CreatedDate = viewModel.CreatedDate,
                UpdatedBy = viewModel.UpdatedBy,
                UpdatedDate = viewModel.UpdatedDate


            };
        }

    }
}


