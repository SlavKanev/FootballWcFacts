using FootballWcFacts.Core.Models.Fact;

namespace FootballWcFacts.Models
{
    /// <summary>
    /// All facts query model
    /// </summary>
    public class AllFactsQueryModel
    {
        /// <summary>
        /// Facts per page
        /// </summary>
        public const int FactsPerPage = 2;
        /// <summary>
        /// Tournament
        /// </summary>
        public string? Tournament { get; set; }
        /// <summary>
        /// Facts sorting
        /// </summary>
        public FactSorting Sorting { get; set; }
        /// <summary>
        /// Current page
        /// </summary>
        public int CurrentPage { get; set; } = 1;
        /// <summary>
        /// Total facts count
        /// </summary>
        public int TotalFactsCount { get; set; }
        /// <summary>
        /// List of tournaments
        /// </summary>
        public IEnumerable<string> Tournaments { get; set; } = Enumerable.Empty<string>();
        /// <summary>
        /// List of facts
        /// </summary>
        public IEnumerable<FactServiceModel> Facts { get; set; } = Enumerable.Empty<FactServiceModel>();
    }
}
