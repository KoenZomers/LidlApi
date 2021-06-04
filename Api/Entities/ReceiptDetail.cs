using System.Text.Json.Serialization; 
using System.Collections.Generic; 
using System; 

namespace KoenZomers.Lidl.Api.Entities
{ 
    /// <summary>
    /// Detailed receipt information
    /// </summary>
    public class ReceiptDetail
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("barCode")]
        public string BarCode { get; set; }

        [JsonPropertyName("sequenceNumber")]
        public string SequenceNumber { get; set; }

        [JsonPropertyName("workstation")]
        public string Workstation { get; set; }

        [JsonPropertyName("itemsLine")]
        public List<ItemsLine> ItemsLine { get; set; }

        [JsonPropertyName("taxes")]
        public List<Tax> Taxes { get; set; }

        [JsonPropertyName("totalTaxes")]
        public TotalTaxes TotalTaxes { get; set; }

        [JsonPropertyName("couponsUsed")]
        public List<object> CouponsUsed { get; set; }

        [JsonPropertyName("returnedTickets")]
        public List<object> ReturnedTickets { get; set; }

        [JsonPropertyName("isFavorite")]
        public bool? IsFavorite { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("totalAmount")]
        public decimal? TotalAmount { get; set; }

        [JsonPropertyName("sumAmount")]
        public decimal? SumAmount { get; set; }

        [JsonPropertyName("storeCode")]
        public string StoreCode { get; set; }

        [JsonPropertyName("currency")]
        public Currency Currency { get; set; }

        [JsonPropertyName("payments")]
        public List<Payment> Payments { get; set; }

        [JsonPropertyName("tenderChange")]
        public List<object> TenderChange { get; set; }

        [JsonPropertyName("fiscalDataAt")]
        public object FiscalDataAt { get; set; }

        [JsonPropertyName("isEmployee")]
        public bool? IsEmployee { get; set; }

        [JsonPropertyName("linesScannedCount")]
        public int? LinesScannedCount { get; set; }

        [JsonPropertyName("totalDiscount")]
        public string TotalDiscount { get; set; }

        [JsonPropertyName("taxExcemptTexts")]
        public string TaxExcemptTexts { get; set; }

        [JsonPropertyName("invoiceRequestId")]
        public object InvoiceRequestId { get; set; }

        [JsonPropertyName("invoiceId")]
        public object InvoiceId { get; set; }

        [JsonPropertyName("ustIdNr")]
        public object UstIdNr { get; set; }

        [JsonPropertyName("languageCode")]
        public string LanguageCode { get; set; }

        [JsonPropertyName("fiscalDataCompanyTax")]
        public object FiscalDataCompanyTax { get; set; }

        [JsonPropertyName("fiscalDataStoreTax")]
        public object FiscalDataStoreTax { get; set; }

        [JsonPropertyName("fiscalDataBkp")]
        public object FiscalDataBkp { get; set; }

        [JsonPropertyName("fiscalDataPkp")]
        public object FiscalDataPkp { get; set; }

        [JsonPropertyName("fiscalDataRin")]
        public object FiscalDataRin { get; set; }

        [JsonPropertyName("fiscalDataSalesRegime")]
        public object FiscalDataSalesRegime { get; set; }

        [JsonPropertyName("fiscalDataFik")]
        public object FiscalDataFik { get; set; }

        [JsonPropertyName("operatorId")]
        public object OperatorId { get; set; }

        [JsonPropertyName("fiscalDataDe")]
        public FiscalDataDe FiscalDataDe { get; set; }

        [JsonPropertyName("htmlPrintedReceipt")]
        public object HtmlPrintedReceipt { get; set; }
    }
}