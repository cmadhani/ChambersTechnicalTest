namespace DocumentManagement.Models
{
    public interface ISingleResponse<TModel>
    {
        string ErrorMessage { get; set; }
        TModel Model { get; set; }
    }
}
