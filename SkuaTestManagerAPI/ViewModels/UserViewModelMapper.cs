using STM.Core.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace STMAPI.ViewModels
{
    public static class UserViewModelMapper
    {
        public static UserViewModel ToViewModel(this User entity)
        {

            return new UserViewModel
            {
                UserId = entity.UserId,
                UserName = entity.UserName,
                Email = entity.Email,
                Password = entity.Password,
                LastLoginDate = entity.LastLoginDate,
                RoleId = entity.RoleId,
                Active = entity.Active,
                CreatedDate = entity.CreatedDate,
                CreatedBy = entity.CreatedBy,
                UpdatedDate = entity.UpdatedDate,
                UpdatedBy = entity.UpdatedBy,
                Salt = entity.Salt
                


            };
        }

        public static User ToEntity(this UserViewModel viewModel)
        {
            return new User
            {
                // Name = viewModel.ProductName,
                //ProductNumber = viewModel.ProductNumber
                UserId = viewModel.UserId,
                UserName = viewModel.UserName,
                Email = viewModel.Email,
                Password = viewModel.Password,
                LastLoginDate = viewModel.LastLoginDate,
                RoleId = viewModel.RoleId,
                Active = viewModel.Active,
                CreatedDate = viewModel.CreatedDate,
                CreatedBy = viewModel.CreatedBy,
                UpdatedDate = viewModel.UpdatedDate,
                Salt = viewModel.Salt
              // UserId = 37,
             //  UserName = "rs.bali@skua.co.in",
              // Email = "rs.bali@skua.co.in"




            };
        }


    }
}
