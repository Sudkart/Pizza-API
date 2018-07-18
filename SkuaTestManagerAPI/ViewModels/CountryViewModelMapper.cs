using STM.Core.EntityLayer;


namespace STMAPI.ViewModels
{
    public static class CountryViewModelMapper
    {
        public static CountryViewModel ToViewModel(this Countries entity)
        {

            return new CountryViewModel
            {
                Id = entity.Id,
                Country = entity.Country
            };
        }


    }
}
