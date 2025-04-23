using RestWithASPNETUdemy.Hypermedia;

namespace RestWithASPNETErudio.HyperMedia.Abstract
{
    public interface ISupportHyperMedia
    {
        List<HyperMediaLink> Links { get; set; }
    }
}
