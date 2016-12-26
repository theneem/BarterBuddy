using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarterBuddy.Common.IOC
{
    ///// <summary>
    ///// IOCHelper class
    ///// </summary>
    //public class IOCHelper
    //{
    //    /// <summary>
    //    /// infrastructure File name
    //    /// </summary>
    //    public static string UnityConfigurationFile = "UnityConfiguration.config";

    //    /// <summary>
    //    /// Lock Object
    //    /// </summary>
    //    private static readonly object lockObject = new object();

    //    /// <summary>
    //    /// Dictionary for registered instances
    //    /// </summary>
    //    private static readonly Dictionary<string, object> registeredInstances = new Dictionary<string, object>();

    //    /// <summary>
    //    /// Unity Container that will register and resolve the interfaces
    //    /// </summary>
    //    private static IUnityContainer container;

    //    /// <summary>
    //    /// _mappingsFiles
    //    /// </summary>
    //    private static HashSet<string> mappingsFiles;

    //    /// <summary>
    //    /// Loaded configuration file name.
    //    /// Exposed to ease configuring tests.
    //    /// </summary>
    //    public static HashSet<string> MappingsFiles
    //    {
    //        get
    //        {
    //            return mappingsFiles ?? (mappingsFiles = new HashSet<string>());
    //        }
    //    }

    //    /// <summary>
    //    /// Gets the container.
    //    /// </summary>
    //    /// <value>The container.</value>
    //    private static IUnityContainer Container
    //    {
    //        get
    //        {
    //            lock (lockObject)
    //            {
    //                if (container == null)
    //                {
    //                    Reconfigure();
    //                }
    //            }

    //            return container;
    //        }
    //    }

    //    /// <summary>
    //    /// Gets the assemblies location.
    //    /// </summary>
    //    /// <value>The assemblies location.</value>
    //    private static string AssembliesLocation
    //    {
    //        get
    //        {
    //            return string.IsNullOrEmpty(AppDomain.CurrentDomain.RelativeSearchPath)
    //                   ? AppDomain.CurrentDomain.BaseDirectory
    //                   : AppDomain.CurrentDomain.RelativeSearchPath;
    //        }
    //    }

    //    /// <summary>
    //    /// Reconfigures this instance.
    //    /// </summary>
    //    public static void Reconfigure()
    //    {
    //        MappingsFiles.Clear();
    //        container = new UnityContainer();

    //        AddConfiguration(UnityConfigurationFile);
    //    }

    //    /// <summary>
    //    /// Reconfigures the specified infrastructure file.
    //    /// </summary>
    //    /// <param name="infrastructureFile">The infrastructure file.</param>
    //    /// <param name="commonFile">The common file.</param>
    //    static void Reconfigure(string infrastructureFile, string commonFile)
    //    {
    //        MappingsFiles.Clear();
    //        container = new UnityContainer();
    //        AddConfiguration(infrastructureFile);
    //        AddConfiguration(commonFile);
    //    }

    //    /// <summary>
    //    /// Adds the infrastructure configuration.
    //    /// </summary>
    //    static void AddInfrastructureConfiguration()
    //    {
    //        AddConfiguration(UnityConfigurationFile);
    //    }

    //    /// <summary>
    //    /// Resolves the registered concrete class for interface T.
    //    /// </summary>
    //    /// <returns></returns>
    //    public static T Resolve<T>()
    //    {
    //        if (registeredInstances.ContainsKey(typeof(T).Name))
    //        {
    //            return (T)registeredInstances[typeof(T).Name];
    //        }

    //        return Container.Resolve<T>();
    //    }

    //    /// <summary>
    //    /// Resolves the registered concrete calss with specific "name" for interface T.
    //    /// </summary>
    //    /// <param name="name">The name.</param>
    //    /// <returns></returns>
    //    public static T Resolve<T>(string name)
    //    {
    //        if (registeredInstances.ContainsKey(name))
    //        {
    //            return (T)registeredInstances[name];
    //        }

    //        return Container.Resolve<T>(name);
    //    }

    //    /// <summary>
    //    /// Registers interface TFrom to concrete class TTo
    //    /// </summary>
    //    /// <typeparam name="TFrom">Interface to be registered</typeparam>
    //    /// <typeparam name="TTo">Concrete class to rgister to</typeparam>
    //    public static void RegisterType<TFrom, TTo>()
    //    {
    //        RegisterType(typeof(TFrom), typeof(TTo));
    //    }

    //    /// <summary>
    //    /// Registers interface TFrom to concrete class TTo with a name.
    //    /// Name will be used while resolving the instance
    //    /// </summary>
    //    /// <typeparam name="TFrom">Interface to be registered</typeparam>
    //    /// <typeparam name="TTo">Concrete class to rgister to</typeparam>
    //    /// <param name="name">Name of registered instance</param>
    //    public static void RegisterType<TFrom, TTo>(string name)
    //    {
    //        RegisterType(typeof(TFrom), typeof(TTo), name);
    //    }

    //    /// <summary>
    //    /// Registers interface typeof(from) to concrete class typeof(to)
    //    /// </summary>
    //    /// <param name="from">Interface to be registered</param>
    //    /// <param name="to">Concrete class to rgister to</param>
    //    public static void RegisterType(Type from, Type to)
    //    {
    //        RegisterType(from, to, string.Empty);
    //    }

    //    /// <summary>
    //    /// Registers interface typeof(from) to concrete class typeof(to) with a name
    //    /// Name will be used while resolving the instance
    //    /// </summary>
    //    /// <param name="from">Interface to be registered</param>
    //    /// <param name="to">Concrete class to rgister to</param>
    //    /// <param name="name">Name of registered instance</param>
    //    public static void RegisterType(Type from, Type to, string name)
    //    {
    //        if (string.IsNullOrEmpty(name))
    //            Container.RegisterType(from, to);
    //        else
    //            Container.RegisterType(from, to, name);
    //    }

    //    /// <summary>
    //    /// Registers instance for interface TInterface
    //    /// </summary>
    //    /// <param name="instance">The instance.</param>
    //    public static void RegisterInstance<TInteface>(TInteface instance)
    //    {
    //        if (!registeredInstances.ContainsKey(typeof(TInteface).Name))
    //        {
    //            registeredInstances.Add(typeof(TInteface).Name, instance);
    //        }
    //        else
    //        {
    //            registeredInstances[typeof(TInteface).Name] = instance;
    //        }
    //    }

    //    /// <summary>
    //    /// Registers instance for interface TInterface
    //    /// </summary>
    //    /// <param name="instance">The instance.</param>
    //    /// <param name="typeName">Name of the type.</param>
    //    public static void RegisterInstance<TInteface>(TInteface instance, string typeName)
    //    {
    //        if (!registeredInstances.ContainsKey(typeName))
    //        {
    //            registeredInstances.Add(typeName, instance);
    //        }
    //        else
    //        {
    //            registeredInstances[typeName] = instance;
    //        }
    //    }

    //    /// <summary>
    //    /// Unregisters all instances.
    //    /// </summary>
    //    public static void UnregisterAllInstances()
    //    {
    //        registeredInstances.Clear();
    //    }

    //    /// <summary>
    //    /// Determines whether [is instance registered].
    //    /// </summary>
    //    /// <returns></returns>
    //    public static bool IsInstanceRegistered<TInteface>()
    //    {
    //        return registeredInstances.ContainsKey(typeof(TInteface).Name);
    //    }

    //    /// <summary>
    //    /// Adds the configuration.
    //    /// </summary>
    //    /// <param name="configurationFileName">Name of the configuration file.</param>
    //    private static void AddConfiguration(string configurationFileName)
    //    {
    //        if (string.IsNullOrEmpty(configurationFileName))
    //        {
    //            throw new ConfigurationErrorsException("FileMissing");
    //        }

    //        MappingsFiles.Add(configurationFileName);
    //        registeredInstances.Clear();

    //        // Read unity configuration from config
    //        var map = new ExeConfigurationFileMap { ExeConfigFilename = Path.Combine(AssembliesLocation, configurationFileName) };

    //        System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
    //        var section = (UnityConfigurationSection)config.GetSection("unity");

    //        container = container ?? new UnityContainer();
    //        section.Configure(container, "default");
    //    }

    //    public static void LoadContainer()
    //    {
    //        container = container ?? new UnityContainer();
    //        container.LoadConfiguration("default");
    //    }
    //}
}
