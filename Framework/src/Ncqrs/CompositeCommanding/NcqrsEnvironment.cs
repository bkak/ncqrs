using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using Castle.Model;
using Ncqrs.Config;
using Ncqrs.Domain;

namespace Ncqrs
{
    public static partial class NcqrsEnvironment
    {
        /// <summary>
        /// Gets or create the requested instance from Castle Windsor, specified by the parameter <typeparamref name="T"/>.
        /// </summary>
        /// <remarks>This</remarks>
        /// <typeparam name="T">The type of the instance that is requested.
        /// </typeparam>
        /// <returns>If the type specified by <typeparamref name="T"/> is
        /// registered, it returns an instance that is, is a super class of, or
        /// implements the type specified by <typeparamref name="T"/>. Otherwise
        /// a <see cref="InstanceNotFoundInEnvironmentConfigurationException"/>
        /// occurs.
        /// </returns>
        public static T GetFromWindsor<T>() where T : class
        {
            Contract.Ensures(Contract.Result<T>() != null, "The result cannot be null.");

            Log.DebugFormat("Requesting instance {0} from the environment.", typeof(T).FullName);

            T result = (T)Rhino.Commons.IoC.Resolve(typeof(T));
           
            if (result == null)
                throw new InstanceNotFoundInEnvironmentConfigurationException(typeof(T));

            return result;
        }
    }
}
