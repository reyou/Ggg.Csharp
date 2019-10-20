namespace Api
{
    public interface IUserService
    {
        User GetById(in int userId);
    }
}