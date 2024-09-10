using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Interfaces.IHttpJsonServices
{
    public interface IHttpJsonService
    {
        Task<JObject> FetchJsonDataAsync(Activity activity, string url);
    }
}
