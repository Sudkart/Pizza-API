using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using STM.Core.EntityLayer;

namespace STMAPI.ViewModels
{
    public static class ActionViewModelMapper
    {
        public static ActionViewModel ToViewModel(this STM.Core.EntityLayer.Action entity)
        {

            return new ActionViewModel
            {
                ActionId = entity.ActionId,
                ActionGroup = entity.ActionGroup,
                ActionType = entity.ActionType,
                ActionName = entity.ActionName,
                ActionGroupName = entity.ActionGroupName,
                ActionDescription = entity.ActionDescription,
                ActionRemarks = entity.ActionRemarks,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate

            };
        }

        public static STM.Core.EntityLayer.Action ToEntity(this ActionViewModel viewModel)
        {
            return new STM.Core.EntityLayer.Action
            {
                ActionId = viewModel.ActionId,
                ActionGroup = viewModel.ActionGroup,
                ActionType = viewModel.ActionType,
                ActionGroupName = viewModel.ActionGroupName,
                ActionName = viewModel.ActionName,
                ActionDescription = viewModel.ActionDescription,
                ActionRemarks = viewModel.ActionRemarks,
                CreatedBy = viewModel.CreatedBy,
                CreatedDate = viewModel.CreatedDate,
                UpdatedBy = viewModel.UpdatedBy,
                UpdatedDate = viewModel.UpdatedDate
            };
        }
    }
}
