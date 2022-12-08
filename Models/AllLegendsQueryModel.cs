using FootballWcFacts.Core.Models.Legend;

namespace FootballWcFacts.Models
{
    public class AllLegendsQueryModel
    {

        public const int LegendsPerPage = 3;

        public string? Position { get; set; }

        public LegendSorting Sorting { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalLegendsCount { get; set; }

        public IEnumerable<string> Positions { get; set; } = Enumerable.Empty<string>();

        public IEnumerable<LegendServiceModel> Legends { get; set; } = Enumerable.Empty<LegendServiceModel>();

    }
}
