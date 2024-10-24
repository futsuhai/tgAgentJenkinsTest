namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Exceptions;

/// <summary>
/// Представляет исключение, которое вызывается, когда запрашиваемый файл уже загружен локально
/// </summary>
public class FileAlreadyDownloadedException() : Exception("The inputFile you are trying to upload has already been uploaded");