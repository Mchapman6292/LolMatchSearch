using Newtonsoft.Json.Linq;

namespace LolMatchFilterNew.Domain.Interfaces.IApiHelper
{
    public interface IApiHelper
    {
        Task<string> WriteToDocxDocumentAsync(string title, List<string> content = null);

        Task<string> WriteListDictToWordDocAsync<T>(T data, string customFileName = null);



        int? GetNullableInt32Value(JObject obj, string key);

        string GetStringValue(JObject obj, string key);
        string? GetNullableStringValue(JObject obj, string key);


        List<string> GetValuesAsList(JObject obj, string key);

        List<string>? GetNullableValuesAsList(JObject obj, string key);

        DateTime GetDateTimeFromJobject(JObject obj, string key);
        DateTime? GetNullableDateTimeFromJobject(JObject obj, string key);

        DateOnly? GetNullableDateOnlyFromJobject(JObject obj, string key);

        public DateTime ConvertDateTimeOffSetToUTC(object inputDateTime);
        DateTime? ParseNullableDateTime(JObject obj, string key);



        (int TotalObjects, int NullObjects, int NullProperties) CountObjectsAndNullProperties(IEnumerable<JObject> enumerable);


        string NormalizeOverviewPageToName(string overviewPage);







    }
}
