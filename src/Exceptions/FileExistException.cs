namespace LS.Avatars.Api.Exceptions;

public class FileExistException : BaseException
{
    public override ErrorTypes ErrorType => ErrorTypes.FileExists;
}