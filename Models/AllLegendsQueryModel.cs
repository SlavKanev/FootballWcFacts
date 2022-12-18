using FootballWcFacts.Core.Models.Legend;

namespace FootballWcFacts.Models
{
    /// <summary>
    /// All Legends query model
    /// </summary>
    public class AllLegendsQueryModel
    {
        /// <summary>
        /// Const legends per page
        /// </summary>

        public const int LegendsPerPage = 3;
        /// <summary>
        /// Legends position
        /// </summary>
        public string? Position { get; set; }
        /// <summary>
        /// Legends sorting
        /// </summary>
        public LegendSorting Sorting { get; set; }
        /// <summary>
        /// Legends currentPage
        /// </summary>
        public int CurrentPage { get; set; } = 1;
        /// <summary>
        /// Total legends count
        /// </summary>
        public int TotalLegendsCount { get; set; }
        /// <summary>
        /// List of positions
        /// </summary>
        public IEnumerable<string> Positions { get; set; } = Enumerable.Empty<string>();
        /// <summary>
        /// List of legends
        /// </summary>
        public IEnumerable<LegendServiceModel> Legends { get; set; } = Enumerable.Empty<LegendServiceModel>();

    }
}
