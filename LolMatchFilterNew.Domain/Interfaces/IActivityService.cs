using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Activity = System.Diagnostics.Activity;

namespace LolMatchFilterNew.Domain.Interfaces.IActivityService
{
    public interface IActivityService
    {
        Task<Activity> StartActivityAsync(string name);
        Task<Activity> StartChildActivityAsync(Activity parent, string name);
        Task StopActivityAsync(Activity activity);
    }
}
