using PubLibIS.Domain.Entities;
using System.Collections.Generic;

namespace PubLibIS.DAL.Interfaces
{
    public interface IBackupFileRepository
    {
        BackupFile Get(int fileId);
        BackupFile GetByFileNameBase64(string FileNameBase64);
        IEnumerable<BackupFile> GetList(int User_Id);
        IEnumerable<BackupFile> GetList(int User_Id, IEnumerable<int> idList);
        IEnumerable<BackupFile> GetList(int User_Id, int skip, int take);
        int Create(BackupFile backupFile);
        void Update(BackupFile backupFile);
        void Delete(int fileId);
    }
}
