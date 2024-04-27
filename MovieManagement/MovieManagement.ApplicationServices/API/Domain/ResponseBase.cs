namespace MovieManagement.ApplicationServices.API.Domain;

public abstract class ResponseBase<T>
{
    public T? Data {  get; set; }
}