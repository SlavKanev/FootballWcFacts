using FootballWcFacts.Core.Models.Fact;

namespace FootballWcFacts.Areas.Admin.Models
{
    public class MyFactsViewModel
    {

        public IEnumerable<FactServiceModel> AddedFacts { get; set; }
            = new List<FactServiceModel>();

    }
}
