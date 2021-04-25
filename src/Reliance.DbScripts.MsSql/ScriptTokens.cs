using System;
using System.Collections.Generic;
using System.Text;

namespace Reliance.DbScripts.MsSql
{
    /// <summary>
    /// String token to help determine what scripts needs to run when
    /// Can be changed to read values from the app seetings file.
    /// </summary>
    public static class ScriptTokens
    {
        public const string Predeployment = "00_Predeployment";

        public const string DeployDbObjects = "01_DbObjects";

        public const string DeployDataAllEnvironments = "02_Data.All";
        public const string DeployDataLocalDev = "02_Data.LocalDev";
        public const string DeployDataTesting = "02_Data.Testing";
        public const string DeployDataUat = "02_Data.Uat";
        public const string DeployDataProduction = "02_Data.Production";

        //public const string Rollback = "99_Rollback";  //TODO: Investigate rollback options and implementations

        public enum DeploymentEnvironments
        {
            All,
            LocalDev,
            Testing,
            Uat,
            Production
        }
    }
}
