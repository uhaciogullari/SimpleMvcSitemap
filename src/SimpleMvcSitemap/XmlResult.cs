using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleMvcSitemap.Routing;
using SimpleMvcSitemap.Serialization;


namespace SimpleMvcSitemap
{
    class XmlResult<T> : ActionResult
    {
        private readonly IBaseUrlProvider baseUrlProvider;
        private readonly T data;
        private readonly IUrlValidator urlValidator;


        internal XmlResult(T data, IUrlValidator urlValidator)
        {
            this.data = data;
            this.urlValidator = urlValidator;
        }

        internal XmlResult(T data, IBaseUrlProvider baseUrlProvider) : this(data, new UrlValidator(new ReflectionHelper()))
        {
            this.baseUrlProvider = baseUrlProvider;
        }


        public override async Task ExecuteResultAsync(ActionContext context)
        {
            urlValidator.ValidateUrls(data, baseUrlProvider ?? new BaseUrlProvider(context.HttpContext.Request));

            var response = context.HttpContext.Response;

            if (context.ActionDescriptor.FilterDescriptors.Any(x => x.Filter.GetType() == typeof(ProducesAttribute)))
            {
                ProducesAttribute attribute = (ProducesAttribute)context.ActionDescriptor.FilterDescriptors.Where(x => x.Filter.GetType() == typeof(ProducesAttribute)).First().Filter;

                if (attribute.ContentTypes.Any(x => x.Contains("xml")))
                {
                    response.ContentType = attribute.ContentTypes.First(x => x.Contains("xml"));
                }
                else
                {
                    response.ContentType = "text/xml";
                }
            }
            else
            {
                response.ContentType = "text/xml";
            }


            await response.WriteAsync(new XmlSerializer().Serialize(data), Encoding.UTF8);

            await base.ExecuteResultAsync(context);
        }
    }
}