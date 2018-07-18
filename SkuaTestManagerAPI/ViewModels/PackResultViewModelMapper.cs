using STM.Core.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STMAPI.ViewModels
{
    public static class PackResultViewModelMapper
    {

        public static PackResultViewModel ToViewModel(this PackResult entity,PieChartData pieChartData)
        {

            return new PackResultViewModel
            {
                PackId = entity.PackId,
                PackName = entity.PackName,
                EntityId = entity.EntityId,
                TypeOfPackId = entity.TypeOfPackId,
                ProjectId = entity.ProjectId,
                Status = entity.Status,
                TestGroupId = entity.TestGroupId,
                RunDate = entity.RunDate.ToString("MM/dd/yyyy hh:mm:ss tt"),
                PassPercentage = entity.PassPercentage,
                FailPercentage = entity.FailPercentage,
                SkipPercentage = entity.SkipPercentage,
                ProgresStatus = entity.ProgresStatus,
                RanAt = entity.RanAt,
                RunAt = entity.RunAt,
                PassedSteps = entity.PassedSteps,
                FailedSteps = entity.FailedSteps,
                SkippedSteps = entity.SkippedSteps,
                SuiteName = pieChartData.SuiteName,
                Browser = pieChartData.Browser,
                CreatedDate = pieChartData.CreatedDate,
                Country = pieChartData.Country,
                TestType = pieChartData.TestType,
                EnvironmentName = pieChartData.EnvironmentName,
                StartTime = pieChartData.StartTime,
                EndTime = pieChartData.EndTime

            };
        }

        public static PackResult ToEntity(this PackResultViewModel viewModel)
        {
            return new PackResult
            {
                PackId = viewModel.PackId,
                PackName = viewModel.PackName,
                ProjectId =viewModel.ProjectId,
                EntityId = viewModel.EntityId,
                TypeOfPackId=viewModel.TypeOfPackId,
                Status = viewModel.Status,
                TestGroupId = viewModel.TestGroupId,
               // RunDate = viewModel.RunDate,
                PassPercentage = viewModel.PassPercentage,
                ProgresStatus = viewModel.ProgresStatus,
                RanAt = viewModel.RanAt,
                RunAt = viewModel.RunAt
            };
        }

















    }
}
