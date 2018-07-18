
using STM.Core.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace STMAPI.ViewModels
{
    public static class ResultsViewModelMapper
    {
        public static ResultsViewModel ToViewModel(this Results entity)
        {

            return new ResultsViewModel
            {
                PackName = entity.PackName,
                PackId =entity.PackId,
                ProjectName= entity.ProjectName,
                Status = entity.Status,
                RunningAt= entity.RunningAt,
                TestGroupId = entity.TestGroupId,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                PercentCompleted = entity.PercentCompleted,
                PercentFailed = entity.PercentFailed,
                PercentPassed = entity.PercentPassed,
                PercentSkipped = entity.PercentSkipped
              
            };
        }

        //public static User ToEntity(this ResultsViewModel viewModel)
        //{
        //    return new Results
        //    {
        //        // Name = viewModel.ProductName,
        //        //ProductNumber = viewModel.ProductNumber
        //        UserId = viewModel.UserId,
        //        UserName = viewModel.UserName,
        //        Email = viewModel.Email,
        //        Password = viewModel.Password,
        //        LastLoginDate = viewModel.LastLoginDate,
        //        RoleId = viewModel.RoleId,
        //        Active = viewModel.Active,
        //        CreatedDate = viewModel.CreatedDate,
        //        CreatedBy = viewModel.CreatedBy,
        //        UpdatedDate = viewModel.UpdatedDate,
        //        Salt = viewModel.Salt
        //        // UserId = 37,
        //        //  UserName = "rs.bali@skua.co.in",
        //        // Email = "rs.bali@skua.co.in"




        //    };
        //}


    }
}
