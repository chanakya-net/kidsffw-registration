namespace kidsffw.Common.DTO;

public class UpdateUserTransactionDtoRequest
{
    public int registrationId { get; set; }
    public string TransactionId { get; set; }
    public string TransactionStatus { get; set; }
    public string TransactionAmount { get; set; }
}