using PubLibIS.DAL.Interfaces;
using PubLibIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PubLibIS.DAL.Repositories.EntityFramework
{
    public class BackupFileRepository : IBackupFileRepository
    {
        private LibraryEntityFrameworkContext context;

        public BackupFileRepository(LibraryEntityFrameworkContext context)
        {
            this.context = context;
        }

        public int Create(BackupFile backupFile)
        {
            return context.BackupFiles.Add(backupFile).Id;
        }

        public void Delete(int fileId)
        {
            BackupFile file = context.BackupFiles.Find(fileId);
            context.BackupFiles.Remove(file);
        }

        public BackupFile Get(int fileId)
        {
            return context.BackupFiles.Find(fileId);
        }

        public BackupFile GetByFileNameBase64(string FileNameBase64)
        {
            return context.BackupFiles.FirstOrDefault(file => file.FileNameBase64 == FileNameBase64);
        }

        public IEnumerable<BackupFile> GetList(int User_Id)
        {
            return context.BackupFiles.Where(file => file.User_Id == User_Id).ToList();
        }

        public IEnumerable<BackupFile> GetList(int User_Id, IEnumerable<int> idList)
        {
            return context.BackupFiles.Where(file => file.User_Id == User_Id && idList.Contains(file.Id)).ToList();

        }

        public IEnumerable<BackupFile> GetList(int User_Id, int skip, int take)
        {
            return context.BackupFiles.Where(file => file.User_Id == User_Id ).Skip(skip).Take(take).ToList();

        }

        public void Update(BackupFile backupFile)
        {
            var current = Get(backupFile.Id);
            context.Entry(current).CurrentValues.SetValues(backupFile);
        }
    }
}
