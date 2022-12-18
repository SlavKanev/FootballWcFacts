namespace FootballWcFacts.Models
{
    /// <summary>
    /// Error view model
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Request identifier
        /// </summary>
        public string? RequestId { get; set; }
        /// <summary>
        /// Shos request id
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}