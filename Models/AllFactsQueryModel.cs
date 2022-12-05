using FootballWcFacts.Core.Models.Fact;

namespace FootballWcFacts.Models
{
    public class AllFactsQueryModel
    {
        public const int FactsPerPage = 5;

        public string? Tournament { get; set; }

        public FactSorting Sorting { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalFactsCount { get; set; }

        public IEnumerable<string> Tournaments { get; set; } = Enumerable.Empty<string>();

        public IEnumerable<FactServiceModel> Facts { get; set; } = Enumerable.Empty<FactServiceModel>();
    }
}
