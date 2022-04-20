using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flurl.Http;
using Flurl;
namespace Paska
{
    public static class SisuApi
    {
        private const string apiUrl = "https://sis-tuni.funidata.fi/kori/api/";
        public async static Task<string> GetModuleJson(string groupID)
        {
            var response = await apiUrl.AppendPathSegment("modules")
                .AppendPathSegment("by-group-id")
                .SetQueryParam("universityId", "tuni-university-root-id")
                .SetQueryParam("groupId",groupID)
                .GetStringAsync();

            return response;
        }
        public async static Task<string> GetDegreeProgrammeJson(string groupID)
        {
            var response = await apiUrl.AppendPathSegment("modules")
                .AppendPathSegment(groupID)
                .GetStringAsync();

            return response;
        }
    }
}
