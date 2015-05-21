using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BookPortal.Web.Domain
{
    public static class DataReaderExtensions
    {
        private static bool IsNullableType(Type theValueType)
        {
#if DNXCORE50
            return (theValueType.GetTypeInfo().IsGenericType && theValueType.GetGenericTypeDefinition() == typeof(Nullable<>));
#else
            return (theValueType.IsGenericType && theValueType.GetGenericTypeDefinition() == typeof(Nullable<>));
#endif
        }

        public static T GetValue<T>(this SqlDataReader theReader, string theColumnName)
        {
            // Read the value out of the reader by string (column name); returns object
            object theValue = theReader[theColumnName];

            // Cast to the generic type applied to this method (i.e. int?)
            Type theValueType = typeof(T);

            // Check for null value from the database
            if (DBNull.Value != theValue)
            {
                // We have a null, do we have a nullable type for T?
                if (!IsNullableType(theValueType))
                {
                    // No, this is not a nullable type so just change the value's type from object to T
                    return (T)Convert.ChangeType(theValue, theValueType);
                }
                else
                {
                    // Yes, this is a nullable type so change the value's type from object to the underlying type of T
                    NullableConverter theNullableConverter = new NullableConverter(theValueType);

                    return (T)Convert.ChangeType(theValue, theNullableConverter.UnderlyingType);
                }
            }

            // The value was null in the database, so return the default value for T; this will vary based on what T is (i.e. int has a default of 0)
            return default(T);
        }
    }
}
