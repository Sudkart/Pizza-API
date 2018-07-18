using STM.Core.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STMAPI.ViewModels
{
    public static  class ObjectViewModelMapper
    {
        public static ObjectViewModel ToViewModel(this TestObject entity)
        {
            return new ObjectViewModel
            {
                ObjectId = entity.ObjectId,
                ProjectName = entity.ProjectName,
                ObjectName = entity.ObjectName,
                IdentityType = entity.IdentityType,
                ObjectValue = entity.ObjectValue,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate
            };


        }

        public static TestObject ToEntity(this ObjectViewModel viewModel)
        {

            return new TestObject
            {
             
                ObjectId = viewModel.ObjectId,
                ProjectName = viewModel.ProjectName,
                ObjectName = viewModel.ObjectName,
                IdentityType = viewModel.IdentityType,
                ObjectValue = viewModel.ObjectValue,
                CreatedBy = viewModel.CreatedBy,
                CreatedDate = viewModel.CreatedDate,
                UpdatedBy = viewModel.UpdatedBy,
                UpdatedDate = viewModel.UpdatedDate


            };


        }




    }
}
