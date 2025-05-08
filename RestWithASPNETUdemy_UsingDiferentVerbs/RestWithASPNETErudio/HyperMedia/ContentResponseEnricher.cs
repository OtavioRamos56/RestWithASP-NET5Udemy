using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using RestWithASPNETErudio.HyperMedia.Abstract;
using RestWithASPNETErudio.HyperMedia.Utils;

namespace RestWithASPNETErudio.HyperMedia
{
    public abstract class ContentResponseEnricher<T> : IResponseEnricher where T : ISupportHyperMedia
    {
        public ContentResponseEnricher()
        {
        }
        public bool CanEnrich(Type contentType)
        {
            return contentType == typeof(T) || contentType == typeof(List<T>) || contentType == typeof(PagedSearchVO<T>);
        }
        protected abstract Task EnrichContent(T content, IUrlHelper urlHelper);
        bool IResponseEnricher.CanEnrich(ResultExecutingContext response)
        {
            if (response.Result is OkObjectResult okObjectResult)
            {
                return CanEnrich(okObjectResult.Value.GetType());
            }
            return false;
        }

        public async Task Enrich(ResultExecutingContext response)
        {
            var urlHelper = new UrlHelperFactory().GetUrlHelper(response);
            if (response.Result is OkObjectResult okObjectResult)
            {
                if (okObjectResult.Value is T model)
                {
                    await EnrichContent(model, urlHelper);
                }
                else if (okObjectResult.Value is List<T> collection)
                {
                    ConcurrentBag<T> bag = new ConcurrentBag<T>(collection);
                    Parallel.ForEach(bag, (element) =>
                    {
                        EnrichContent(element, urlHelper).Wait();
                    });
                }
                else if (okObjectResult.Value is PagedSearchVO<T> pagedSearch)
                {
                    Parallel.ForEach(pagedSearch.List.ToList(), (element) =>
                    {
                        EnrichContent(element, urlHelper).Wait();
                    });
                }
            }
            await Task.CompletedTask;
        }
    }
}
