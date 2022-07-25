namespace kidsffw.Common.DTO;

public class UpdateUserTransactionDtoRequest
{
    public int RegistrationId { get; set; } 
    public string TransactionId { get; set; } = string.Empty;
    public string TransactionStatus { get; set; } = string.Empty;
    public string TransactionAmount { get; set; } = string.Empty;
}