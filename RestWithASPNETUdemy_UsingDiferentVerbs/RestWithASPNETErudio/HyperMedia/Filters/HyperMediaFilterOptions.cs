using RestWithASPNETErudio.HyperMedia.Abstract;

namespace RestWithASPNETErudio.HyperMedia.Filters
{
    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> ContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();
    }
}
