using STM.Core.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STMAPI.ViewModels
{
    public static class TestStepViewModelMapper
    {
        public static TestStepViewModel ToViewModel(this TestStep entity)
        {

            return new TestStepViewModel
            {
                Step_Id = entity.Step_Id,
                ProjectId = Convert.ToInt32(entity.ProjectId),
                Scen_Id = Convert.ToInt32(entity.Scen_Id),
                Sub_Scen_Id= Convert.ToInt32(entity.Sub_Scen_Id),
                TestStepId = entity.TestStepId,
                ActionId = entity.ActionId,
                ObjectId = entity.ObjectId,
                Xpath = entity.Xpath,
                Description = entity.Description,
                RunMode =entity.RunMode,
                Status = entity.Status,
                Active = entity.Active,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate,
                TestData = entity.TestData

            };
        }

        public static TestStep ToEntity(this TestStepViewModel viewModel)
        {
            return new TestStep
            {
                Step_Id = viewModel.Step_Id,
                ProjectId = Convert.ToString(viewModel.ProjectId),
                Scen_Id= Convert.ToString(viewModel.Scen_Id),
                Sub_Scen_Id = Convert.ToString(viewModel.Sub_Scen_Id),
                TestStepId = viewModel.TestStepId,
                ActionId = viewModel.ActionId,
                ObjectId = viewModel.ObjectId,
                Xpath = viewModel.Xpath,
                Description = viewModel.Description,
                RunMode = viewModel.RunMode,
                Status = viewModel.Status,
                Active = viewModel.Active,
                CreatedBy = viewModel.CreatedBy,
                CreatedDate = viewModel.CreatedDate,
                UpdatedBy = viewModel.UpdatedBy,
                UpdatedDate = viewModel.UpdatedDate,
                TestData = viewModel.TestData
            };

        }

            
    }
}
