using TgInstanceAgent.Application.Abstractions.Commands.TgFiles;
using TgInstanceAgent.Application.Abstractions.Interfaces;
using TgInstanceAgent.Application.Abstractions.Queries.TgFiles;
using TgInstanceAgent.Infrastructure.Web.Components.Mapper;
using TgInstanceAgent.Infrastructure.Web.Files.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Files.Mapper;

/// <summary>
/// Класс для маппинга входных моделей для работы с файлами в команды
/// </summary>
public class FilesMapperProfile : AutoMapper.Profile
{
    /// <summary>
    /// Маппинг входных моделей в команды
    /// </summary>
    public FilesMapperProfile()
    {
        // Карта для FileIdRequestInputModel в DownloadFileCommand
        CreateMap<FileRequestInputModel, DownloadFileCommand>().MapInstanceId();
        
        // Карта для FileIdRequestInputModel в GetFileFromIdQuery
        CreateMap<FileRequestInputModel, GetFileQuery>().MapInstanceId();
        
        // Карта для FileIdRequestInputModel в DownloadAndGetFileCommand
        CreateMap<FileRequestInputModel, DownloadAndGetFileCommand>().MapInstanceId();
        
        // Карта для FileFromMessageInputModel в FileFromMessageData
        CreateMap<FileFromMessageInputModel, FileFromMessageData>();
    }
}