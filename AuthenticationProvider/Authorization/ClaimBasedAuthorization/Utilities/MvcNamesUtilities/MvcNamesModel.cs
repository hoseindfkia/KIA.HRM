using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationProvider.Authorization.ClaimBasedAuthorization.Utilities.MvcNamesUtilities
{
    public class MvcNamesModel : IEquatable<MvcNamesModel>
    {
        public MvcNamesModel(string areaName, string controllerName, string actionName, string claimToAuthorize)
        {
            AreaName = areaName;
            ControllerName = controllerName;
            ActionName = actionName;
            ClaimToAuthorize = claimToAuthorize;
            IsClaimBasedAuthorizationRequired = !string.IsNullOrWhiteSpace(claimToAuthorize);
        }

        public MvcNamesModel(string areaName, string controllerName, string actionName)
        {
            AreaName = areaName;
            ControllerName = controllerName;
            ActionName = actionName;
            ClaimToAuthorize = null;
            IsClaimBasedAuthorizationRequired = false;
        }
        /// <summary>
        /// نام اریا
        /// </summary>
        public string AreaName { get; }
        /// <summary>
        /// نام کنترلر
        /// </summary>
        public string ControllerName { get; }
        /// <summary>
        /// نام اکشن
        /// </summary>
        public string ActionName { get; }
        /// <summary>
        /// نامی که در اتریبوت به اکشن متد می دهیم
        /// 
        /// </summary>
        public string ClaimToAuthorize { get; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsClaimBasedAuthorizationRequired { get; }

        public bool Equals(MvcNamesModel other)
        {
            // If parameter is null, return false.
            if (ReferenceEquals(other, null)) return false;

            // Optimization for a common success case.
            if (ReferenceEquals(this, other)) return true;

            // If run-time types are not exactly the same, return false.
            if (GetType() != other.GetType()) return false;

            return AreaName == other.AreaName
                   && ControllerName == other.ControllerName
                   && ActionName == other.ActionName;

            // مثال:
            //var x = new MvcNamesModel("a", "b", "c");
            //var y = new MvcNamesModel("a", "b", "d");
            //x.Equals(y); // false

        }

        public override bool Equals(object obj)
        {
            return Equals(obj as MvcNamesModel);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AreaName, ControllerName, ActionName);
        }
    }
}
