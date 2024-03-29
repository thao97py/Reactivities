using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace API.Extensions
{
    public static class HttpExtension
    {
        public static void AddPaginationHeader(this HttpResponse response, int currentPage, 
                int itemsPerPage, int totalItems, int totalPages)
        {
            var paginationHeader = new  //anonymous object
            {
                currentPage,
                itemsPerPage,
                totalItems,
                totalPages
            };
            response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader));
            response.Headers.Add("Access-Control-Expose-Headers","Pagination");
        }
    }
}