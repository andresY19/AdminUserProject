using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Queue.Common
{
    public class CommonEnum
    {
        public enum GenericErrors
        {
            OK = 1,
            ErrorLogin = 2,
            RequiredFields = 3,
            GeneralError = 4,
            SaveOk = 5,
            InvalidToken = 6
        }
    }
}