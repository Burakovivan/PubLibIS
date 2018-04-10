using PubLibIS.DAL.UnitsOfWork;
using System.Collections.Generic;
using PubLibIS.ViewModels;
using System.Linq;
using PubLibIS.DAL.Interfaces;
using AutoMapper;
using System.IO;
using System.Text;
using System;
using PubLibIS.Domain.Entities;

namespace PubLibIS.BLL.Services
{
    public class BackupFileService
    {
        private IUnitOfWork db;
        private IMapper mapper;

        public BackupFileService(IUnitOfWork uow, IMapper mapper)
        {
            this.db = uow;
            this.mapper = mapper;
        }
        public BackupFileViewModel GetBackupFileViewModel(int id) {
            return mapper.Map<BackupFileViewModel>(db.BackupFiles.Get(id));
        }
        
        public int CreateBackupFile(BackupFileViewModel backupFileViewModel,string pathToFolder){
            var bacupFile = new BackupFile
            {
                EncodingCodePage = Encoding.UTF8.CodePage,
                FileNameBase64 = backupFileViewModel.FileNameBase64,
                PathToFolder = pathToFolder
            };
            return db.BackupFiles.Create(bacupFile);
        }

        public void DeleteBackupFile(BackupFileViewModel backupFileViewModel){
            db.BackupFiles.Delete(backupFileViewModel.Id);
        }
        public string GetBase64EncodedFileName(string fileName, int encodingCodePage){
            return Convert.ToBase64String(Encoding.GetEncoding(encodingCodePage).GetBytes(fileName));
        }
        public string GetFilePath(string fileNameBase64){
            BackupFile backupFile = db.BackupFiles.GetByFileNameBase64(fileNameBase64);
            string  fileName = Encoding.GetEncoding(backupFile.EncodingCodePage).GetString(Convert.FromBase64String(backupFile.FileNameBase64));
            string filePath = Path.Combine(backupFile.PathToFolder, fileName);
            return filePath;
        }
       
    }
}
