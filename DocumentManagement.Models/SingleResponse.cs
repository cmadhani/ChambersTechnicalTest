namespace DocumentManagement.Models
{
    public class SingleResponse<TModel> : ISingleResponse<TModel>
    {
        public string ErrorMessage { get; set; }

        public TModel Model { get; set; }
    }
}
