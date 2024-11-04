using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Xceed.Document.NET;

namespace LolMatchFilterNew.Application.Tests.YoutubeTitleTests
{
    public class YoutubeTitleTest
    {
        private readonly IAppLogger _appLogger;

        // Example format "VIT vs MAD Highlights | LEC Summer 2023 W1D1 | Team Vitality vs MAD Lions"
        private readonly Regex FormatOne = new Regex(@"^([A-Z]+)\svs\s([A-Z]+)\sHighlights\s\|\sLEC\s(Spring|Summer)\s(\d{4})\sW\d+D\d+\s\|\s(.*?)\svs\s(.*)$");




        public YoutubeTitleTest(IAppLogger appLogger)
    {
            _appLogger = appLogger;
        }

        public (bool isValid, List<string> errors) CheckYoutubeTitleMatch()
        {
            throw new NotImplementedException();
        }
    }
}
