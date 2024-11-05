using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Activity = System.Diagnostics.Activity;

namespace LolMatchFilterNew.Domain.Interfaces.IApiHelper
{
    public interface IApiHelper
    {
        Task<string> WriteToDocxDocumentAsync(string title, List<string> content = null);

        Task<string> WriteListDictToWordDocAsync<T>(T data, string customFileName = null);
        int GetInt32Value(JObject obj, string key);
        List<string> GetValuesAsList(JObject obj, string key);
        string GetStringValue(JObject obj, string key);
        DateTime GetDateTimeFromJObject(JObject obj, string key);

        public DateTime ConvertDateTimeOffSetToUTC(object inputDateTime);

        DateTime ParseDateTime(JObject obj, string key);

        (int TotalObjects, int NullObjects, int NullProperties) CountObjectsAndNullProperties(IEnumerable<JObject> enumerable);





    }
}
