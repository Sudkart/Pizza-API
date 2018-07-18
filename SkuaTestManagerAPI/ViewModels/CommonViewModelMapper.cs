using System.Collections.Generic;
using STM.Core.EntityLayer;


namespace STMAPI.ViewModels
{
    public static class CommonViewModelMapper
    {
        public static CommonViewModel ToViewModel(this Common entity)
        {

            return new CommonViewModel
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public static RunAtModel ToViewModelRunAt(this Common entity)
        {

            return new RunAtModel
            {
                id = entity.Id,
                name = entity.Name
            };
        }

    }
}
