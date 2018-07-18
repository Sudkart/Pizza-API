using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using STM.Core.EntityLayer;

namespace STMAPI.ViewModels
{
    public static class UrlViewModelMapper
    {
        public static UrlViewModel ToViewModel(this Url entity)
        {
            return new UrlViewModel
            {
                UrlId = entity.UrlId,
                EntityId = entity.EntityId,
                ProjectId = entity.ProjectId,
                EnvironmentId = entity.EnvironmentId,
                BaseUrl = entity.BaseUrl,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate


            };
        }

        public static Url ToEntity(this UrlViewModel viewModel)
        {
            return new Url
            {
                UrlId = viewModel.UrlId,
                EntityId = viewModel.EntityId,
                ProjectId = viewModel.ProjectId,
                EnvironmentId = viewModel.EnvironmentId,
                BaseUrl = viewModel.BaseUrl,
                CreatedBy = viewModel.CreatedBy,
                CreatedDate = viewModel.CreatedDate,
                UpdatedBy = viewModel.UpdatedBy,
                UpdatedDate = viewModel.UpdatedDate
                
            };
        }




    }
}
