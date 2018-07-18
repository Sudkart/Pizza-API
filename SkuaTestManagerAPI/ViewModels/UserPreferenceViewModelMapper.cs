using STM.Core.EntityLayer;

namespace STMAPI.ViewModels
{
    public static class UserPreferenceViewModelMapper
    {

        public static UserPreferenceViewModel ToViewModel(this UserPreference entity)
        {

            return new UserPreferenceViewModel
            {
                projectName = entity.projectName,
                auth_Token = entity.auth_Token

            };
        }
    }
}
