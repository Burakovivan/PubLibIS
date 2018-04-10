using AutoMapper;
using PubLibIS.Domain.Entities;
using PubLibIS.ViewModels;
using System.Text;

namespace PubLibIS.BLL.MappingProfiles
{
    public class BackupFileMappingProfile : Profile
    {
        public BackupFileMappingProfile()
        {
            CreateMap<BackupFile, BackupFileViewModel>();
            //.ForMember(
            //bfvm => bfvm.FileNameBase64,
            //opt => opt.MapFrom(bf => bf.FileNameBase64));

            CreateMap<BackupFileViewModel, BackupFile>()
            .ForMember(
            bf => bf.EncodingCodePage,
            opt => opt.MapFrom(bfvm => Encoding.UTF8.CodePage))
            .ForMember(
            bf => bf.FileNameBase64,
            opt => opt.MapFrom(bfvm => bfvm.FileNameBase64
            ));
        }
    }
}
