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
        Task<string> WriteToDocxDocumentAsync(Activity activity, string title, List<string> content = null);
    }
}
