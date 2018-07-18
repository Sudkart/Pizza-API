using STM.Core.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace STMAPI.ViewModels
{
    public static class SuiteViewModelMapper
    {

        public static SuiteViewModel ToViewModel(this Suite entity)
        {

            return new SuiteViewModel
            {
                PackId = entity.PackId,
                PackName = entity.PackName,
                ProjectId = entity.ProjectId,
                Scenarios = entity.Scenarios,
                SubScenarios = entity.SubScenarios,
                RunAt = entity.RunAt,
                TestGroup = entity.TestGroup,
                ScheduleDate = entity.ScheduleDate,
                Active = entity.Active,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy= entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate,
                No_of_Steps = entity.No_of_Steps,
                Type_Of_Pack = entity.Type_Of_Pack,
                scenario_subscenario = entity.scenario_subscenario

            };
        }
        public static Suite ToEntity(this SuiteViewModel viewModel)
        {
            return new Suite
            {
                PackId = viewModel.PackId,
                PackName = viewModel.PackName,
                ProjectId = viewModel.ProjectId,
                Scenarios = viewModel.Scenarios,
                SubScenarios = viewModel.SubScenarios,
                RunAt = viewModel.RunAt,
                TestGroup = viewModel.TestGroup,
                ScheduleDate = viewModel.ScheduleDate,
                Active = viewModel.Active,
                CreatedBy = viewModel.CreatedBy,
                CreatedDate = viewModel.CreatedDate,
                UpdatedBy = viewModel.UpdatedBy,
                UpdatedDate = viewModel.UpdatedDate,
                No_of_Steps = viewModel.No_of_Steps,
                Type_Of_Pack = viewModel.Type_Of_Pack,
                scenario_subscenario = viewModel.scenario_subscenario
            };
        }









    }
}
