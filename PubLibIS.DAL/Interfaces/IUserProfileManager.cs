using PubLibIS.Domain.Entities;
using System;

namespace PubLibIS.DAL.Interfaces
{
    public interface IUserProfileManager : IDisposable
    {
        void Create(UserProfile item);
    }
}
