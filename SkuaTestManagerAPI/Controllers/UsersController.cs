using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STMAPI.Responses;
using STMAPI.ViewModels;
using STM.Core.EntityLayer;
using Npgsql;
//using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using STM.Core.Repositories;
using System.Net.Http;
//using STMAPI.Messages;




namespace STMAPI
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
       private IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository) {
           _userRepository = userRepository;
       }
            
        //Get Users
        /// <summary>
        /// Retrieves a list of Users
        /// </summary>
        [HttpGet]
        [Route("User")]
        public async Task<IActionResult> GetUsersAsync()
         {
            var response = new ListModelResponse<UserViewModel>();

            try
            {
               // response.PageSize = (Int32)pageSize;
               // response.PageNumber = (Int32)pageNumber;

                var usersDataModel = await _userRepository
                        .GetUsers();
                response.Model = usersDataModel.Select(item => item.ToViewModel());
                      

                response.Message = String.Format("Total of records: {0}", response.Model.Count());
                

            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }

            return response.ToHttpResponse();
        }


        //Update User
        /// <summary>
        /// Updates user based on id
        /// </summary>
        [HttpPut]
        [Route("UpdateUser/{userId}")]
        public async Task<string> UpdateUserAsync([FromBody]UserViewModel request, int userId)
        {
            //HttpRequest res = null;
             var response = new ListModelResponse<UserViewModel>();
            var usersDataModel = false;
            try
            {
                usersDataModel = await _userRepository.UpdateUser(request.ToEntity(), userId);

                if (usersDataModel)
                    //  response.Message = String.Format("Record Updated Successfully");
                    response.Message = Messages.SuccessMsg;
              
                else
                    //  response.Message = String.Format("Record Updation failed");
                    response.Message = Messages.FailMsg;
                
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }

            return response.Message;
     
        }



        //Create User
        /// <summary>
        /// Create a new User
        /// </summary>
        [HttpPost]
        [Route("CreateUser")]

        public async Task<string> CreateUserAsync([FromBody]UserViewModel request)
        {
           // HttpRequest res = null;
            var response = new ListModelResponse<UserViewModel>();
            var usersDataModel = false;
            try
            {

                usersDataModel = await _userRepository.CreateUser(request.ToEntity());

                if (usersDataModel)
                    // response.Message = String.Format("Created User Successfully");
                    response.Message = Messages.SuccessMsg;
                else
                    //response.Message = String.Format("Create User failed");
                    response.Message = Messages.FailMsg;
            }

            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }

            return response.Message;


        }


        //Delete User
        /// <summary>
        /// Deletes User based on id
        /// </summary>
         [HttpPost("DeleteUser")]
        public async Task<string> DeleteUserAsync([FromBody]UserViewModel request)
        {

            var response = new ListModelResponse<UserViewModel>();
            var usersDataModel = false;
            try
            {
                usersDataModel = await _userRepository.DeleteUser(request.ToEntity());

                if (usersDataModel)
                    // response.Message = String.Format("Record Deleted Successfully");
                    response.Message = Messages.SuccessMsg;
                else
                    // response.Message = String.Format("Record Deletion failed");
                    response.Message = Messages.FailMsg;
             }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }

            return response.Message;




        }





    }
}
