namespace kidsffw.Common.DTO;

using System.Text.Json.Serialization;

public class RazorPayPayload
{
    
}

public class AcquirerData
{
    [JsonPropertyName("bank_transaction_id")]
    public string BankTransactionId { get; set; } = String.Empty;
}


public class Entity
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
    [JsonPropertyName("entity")]
    public string EntityType { get; set; } = string.Empty;
    [JsonPropertyName("amount")]
    public int Amount { get; set; }
    [JsonPropertyName("currency")]
    public string Currency { get; set; } = string.Empty;
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
    [JsonPropertyName("order_id")]
    public string OrderId { get; set; } = string.Empty;
    [JsonPropertyName("invoice_id")]
    public object InvoiceId { get; set; }
    [JsonPropertyName("international")]
    public bool International { get; set; } 
    [JsonPropertyName("method")]
    public string Method { get; set; } = string.Empty;
    [JsonPropertyName("amount_refunded")]
    public int AmountRefunded { get; set; }
    [JsonPropertyName("refund_status")]
    public object RefundStatus { get; set; }  = string.Empty;
    [JsonPropertyName("captured")]
    public bool Captured { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
    [JsonPropertyName("card_id")]
    public object CardId { get; set; } ;
    [JsonPropertyName("bank")]
    public string Bank { get; set; } = string.Empty;
    [JsonPropertyName("wallet")]
    public object Wallet { get; set; } = string.Empty;
    [JsonPropertyName("vpa")]
    public object Vpa { get; set; } = string.Empty;
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
    [JsonPropertyName("contact")]
    public string Contact { get; set; } = string.Empty;
    [JsonPropertyName("notes")]
    public List<object> Notes { get; set; } 
    [JsonPropertyName("fee")]
    public object Fee { get; set; }
    [JsonPropertyName("tax")]
    public object Tax { get; set; }
    [JsonPropertyName("error_code")]
    public object ErrorCode { get; set; }
    [JsonPropertyName("error_description")]
    public object ErrorDescription { get; set; }
    [JsonPropertyName("error_source")]
    public object ErrorSource { get; set; }
    [JsonPropertyName("error_step")]
    public object ErrorStep { get; set; }
    [JsonPropertyName("error_reason")]
    public object ErrorReason { get; set; }
    [JsonPropertyName("acquirer_data")]
    public AcquirerData AcquirerData { get; set; }
    [JsonPropertyName("created_at")]
    public int CreatedAt { get; set; }
}