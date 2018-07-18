using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using STM.Core.EntityLayer;

namespace STMAPI.ViewModels
{
    public static class ProjKeyValueViewModelMapper
    {
        public static ProjKeyValueViewModel ToViewModel(this ProjKeyValue entity)
        {

            return new ProjKeyValueViewModel
            {
                VarId = entity.VarId,
                EntityId = entity.EntityId,
                ProjectId = entity.ProjectId,
                EnvironmentId = entity.EnvironmentId,
                VarName = entity.VarName,
                VarValue = entity.VarValue,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate
            

            };
        }

        public static ProjKeyValue ToEntity(this ProjKeyValueViewModel viewModel)
        {
            return new ProjKeyValue
            {
                VarId = viewModel.VarId,
                EntityId = viewModel.EntityId,
                ProjectId = viewModel.ProjectId,
                EnvironmentId = viewModel.EnvironmentId,
                VarName = viewModel.VarName,
                VarValue = viewModel.VarValue,
                CreatedBy = viewModel.CreatedBy,
                CreatedDate = viewModel.CreatedDate,
                UpdatedBy =  viewModel.UpdatedBy,
                UpdatedDate = viewModel.UpdatedDate

            };
        }




    }
}
