using System.Net;

namespace GoodsReseller.IntegrationTests.Infrastructure
{
    public class GoodsResellerResult
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Error { get; set; }
    }
    
    public class GoodsResellerResult<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public T Data { get; set; }
        public string Error { get; set; }
    }
}