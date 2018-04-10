using PubLibIS.DAL.UnitsOfWork;

namespace PubLibIS.BLL.Services
{
    public class ServiceCreator
    {
        public UserService CreateUserService(string connection)
        {
            return new UserService(new LibraryUnitOfWorkEntityFramework(connection));
        }
    }
}
