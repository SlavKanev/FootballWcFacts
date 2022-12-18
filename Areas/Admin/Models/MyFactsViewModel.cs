using FootballWcFacts.Core.Models.Fact;

namespace FootballWcFacts.Areas.Admin.Models
{
    /// <summary>
    /// My facts view model
    /// </summary>
    public class MyFactsViewModel
    {
        /// <summary>
        /// List of added facts
        /// </summary>
        public IEnumerable<FactServiceModel> AddedFacts { get; set; }
            = new List<FactServiceModel>();

    }
}
