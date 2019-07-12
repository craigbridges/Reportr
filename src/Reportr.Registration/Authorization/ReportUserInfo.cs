namespace Reportr.Registration.Authorization
{
    using Reportr.Globalization;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Represents information about a single report user
    /// </summary>
    public class ReportUserInfo
    {
        /// <summary>
        /// Constructs the report user info with the details
        /// </summary>
        /// <param name="userKey">The user key</param>
        /// <param name="userName">The user name</param>
        /// <param name="roles">The user role names</param>
        public ReportUserInfo
            (
                string userKey,
                string userName,
                params string[] roles
            )
        {
            Validate.IsNotEmpty(userKey);
            Validate.IsNotEmpty(userName);
            Validate.IsNotNull(roles);

            this.UserKey = userKey;
            this.UserName = userName;
            this.Roles = roles;

            this.MetaData = new Dictionary<string, object>();
        }

        /// <summary>
        /// Constructs the report user info with the details
        /// </summary>
        /// <param name="userKey">The user key</param>
        /// <param name="userName">The user name</param>
        /// <param name="metaData">The meta data</param>
        /// <param name="roles">The user role names</param>
        public ReportUserInfo
            (
                string userKey,
                string userName,
                Dictionary<string, object> metaData,
                params string[] roles
            )

            : this(userKey, userName, roles)
        {
            this.MetaData = metaData;
        }

        /// <summary>
        /// Gets the user key
        /// </summary>
        public string UserKey { get; private set; }

        /// <summary>
        /// Gets the user name
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// Gets an array of user role names
        /// </summary>
        public string[] Roles { get; private set; }

        /// <summary>
        /// Gets a dictionary of user meta data
        /// </summary>
        public Dictionary<string, object> MetaData { get; private set; }

        /// <summary>
        /// Gets or sets the users culture
        /// </summary>
        public CultureInfo Culture { get; set; }

        /// <summary>
        /// Gets or sets the users preferred language
        /// </summary>
        public Language PreferredLanguage { get; set; }
    }
}
