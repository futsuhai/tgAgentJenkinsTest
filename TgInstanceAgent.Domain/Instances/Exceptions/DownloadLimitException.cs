namespace TgInstanceAgent.Domain.Instances.Exceptions;

/// <summary>
/// Исключение, возникающее, при загрузке файла, если их лимит исчерпан
/// </summary>
public class DownloadLimitException() : Exception("Download limit reached");