using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Queue.Utils
{
    public static class ActionType
    {
        public enum Action
        {
            Login = 1,
            Logout = 2,
            ResetPassword = 3,
            FailedLogin = 4,
            Insert = 5,
            Edit = 6,
            Delete = 7
        }
    }
}