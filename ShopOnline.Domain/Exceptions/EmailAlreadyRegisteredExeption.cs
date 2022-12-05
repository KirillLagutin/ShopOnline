namespace ShopOnline.Domain.Exceptions;

[Serializable]
public class EmailAlreadyRegisteredExeption : Exception
{
    public string AccountEmail { get; }

    public EmailAlreadyRegisteredExeption(string message, string email)
        : base(message)
    {
        AccountEmail = email;
    }
}