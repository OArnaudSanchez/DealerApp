using DealerApp.Core.CustomEntities;

namespace DealerApp.API.Responses
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }

        public MetaData Meta { get; set; }

        public ApiResponse(T data)
        {
            Data = data;
        }
    }
}