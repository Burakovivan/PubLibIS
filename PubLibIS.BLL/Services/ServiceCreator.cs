using PubLibIS.BLL.Interfaces;
using PubLibIS.DAL.UnitsOfWork;

namespace PubLibIS.BLL.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IUserService CreateUserService(string connection)
        {
            return new UserService(new LibraryUnitOfWorkEntityFramework(connection));
        }
    }
}
