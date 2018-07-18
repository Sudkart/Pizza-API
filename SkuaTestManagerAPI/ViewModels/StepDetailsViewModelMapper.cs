using STM.Core.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STMAPI.ViewModels
{
    public static class StepDetailsViewModelMapper
    {

        public static StepDetailsViewModel ToViewModel(this StepDetails entity)
        {

            return new StepDetailsViewModel
            {
                StepId = entity.TestStepId,
                StepName = entity.Description,
                Status = entity.Status
            };
        }

        public static StepDetails ToEntity(this StepDetailsViewModel viewModel)
        {
            return new StepDetails
            {
                TestStepId = viewModel.StepId,
                Description = viewModel.StepName,
                Status = viewModel.Status
            };
        }

















    }
}
