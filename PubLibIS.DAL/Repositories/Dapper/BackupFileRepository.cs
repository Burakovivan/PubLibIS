using PubLibIS.DAL.Interfaces;
using PubLibIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLibIS.DAL.Repositories.Dapper
{
    public class BackupFileRepository : Repository<BackupFile>, IBackupFileRepository
    {
        public BackupFileRepository(DapperConnectionFactory dapperConnectionFactory) : base(dapperConnectionFactory) { }

        public BackupFile GetByFileNameBase64(string FileNameBase64)
        {
            return GetList().FirstOrDefault(file => file.FileNameBase64 == FileNameBase64);
        }

        public IEnumerable<BackupFile> GetList(int User_Id)
        {
            return GetList().Where(file => file.User_Id == User_Id);
        }

        public IEnumerable<BackupFile> GetList(int User_Id, IEnumerable<int> idList)
        {
            return GetList(idList).Where(file => file.User_Id == User_Id);
        }

        public IEnumerable<BackupFile> GetList(int User_Id, int skip, int take)
        {
            return GetList(skip, take).Where(file => file.User_Id == User_Id);
        }
    }
}
