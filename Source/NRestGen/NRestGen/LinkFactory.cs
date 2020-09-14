using System;
using System.Collections.Generic;

namespace NRestGen
{
    public sealed class LinkFactory<ResourceT>
        where ResourceT : class
    {
        private readonly Request<ResourceT> _request;

        public LinkFactory(Request<ResourceT> request)
        {
            _request = request ?? throw new ArgumentNullException(nameof(request));
        }

        public List<Link> Manufacter(List<Link> links)
        {
            foreach (var link in links)
            {
                Manufacter(link);
            }
            return links;
        }

        private void Manufacter(Link link)
        {
            link.Href = link.Rel switch
            {
                //Link.RelSelf => CreateSelfUrl(),
                //Link.RelPrevPage => CreatePrevPageUrl(),
                //Link.RelNextPage => CreateNextPageUrl(),
                _ => null
            };
        }

        //private Uri CreateNextPageUrl()
        //{
        //    var skip = _request.Options?.Skip?.Value ?? 0;
        //    var take = _request.Options?.Top?.Value ?? Int32.MaxValue;
        //}

        //private Uri CreatePrevPageUrl()
        //{
        //    var skip = _request.Options?.Skip?.Value ?? 0;
        //    var take = _request.Options?.Top?.Value ?? Int32.MaxValue;
        //}

        //private Uri CreateSelfUrl()
        //{
        //    throw new NotImplementedException();
        //}


        // ----------------------------------------------------------------------


    }
}
