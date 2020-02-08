using BlazorTMDB.Shared.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorTMDB.Server.Controllers
{
    public interface ITMDBController
    {
        Task<string> GetImageString(string filePath);
        List<Result> ParseTMDBType(JObject myJObject);
        Task<Response> Search();
    }
}