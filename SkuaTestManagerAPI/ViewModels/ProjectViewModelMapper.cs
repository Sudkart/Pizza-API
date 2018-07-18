using STM.Core.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STMAPI.ViewModels
{
    public static class ProjectViewModelMapper
    {

        public static ProjectViewModel ToViewModel(this Project entity)
        {

            return new ProjectViewModel
            {
                ProjectId = entity.ProjectId,
                ProjectName = entity.ProjectName,
                ProjectDesc = entity.ProjectDesc,
                ProjectCode = entity.ProjectCode,
                ProjectOwner = entity.ProjectOwner,
                Active = entity.Active,
                CountryId = entity.CountryId,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate
            
            };
        }

        public static Project ToEntity(this ProjectViewModel viewModel)
        {
            return new Project
            {
                ProjectId = viewModel.ProjectId,
               ProjectName = viewModel.ProjectName,
               ProjectDesc = viewModel.ProjectDesc,
               ProjectOwner = viewModel.ProjectOwner,
                ProjectCode = viewModel.ProjectCode,
               Active = viewModel.Active,
               CountryId = viewModel.CountryId,
               CreatedDate = viewModel.CreatedDate,
               UpdatedBy = viewModel.UpdatedBy,
               UpdatedDate = viewModel.UpdatedDate
            };
        }

















    }
}
