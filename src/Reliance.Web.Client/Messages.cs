using System;
using System.Collections.Generic;
using System.Text;

namespace Reliance.Web.Client
{
    public static class Messages
    {
        #region Errors

        public const string Err500 = "Oops... Something went wrong.  Nothing to see here... Try another browser... but if this problem persist, then please contact us.";

        public const string Err417InvalidId = "Invalid Id";

        public const string Err401Unauhtorised = "You are not authorised.";

        public static string Err417InvalidObjectId(string objectName) => $"Invalid {objectName} Id";

        public static string Err417InvalidObjectData(string objectName) => $"Invalid {objectName} Data";

        public static string Err417MissingObjectData(string objectName) => $"Missing Data: {objectName}";

        public static string Err409ObjectExists(string objectName) => $"Object already exists: {objectName}";

        #endregion
    }
}
