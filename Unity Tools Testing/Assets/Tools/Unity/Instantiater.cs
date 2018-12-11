using Nalka.Tools.Extensions;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;


namespace Nalka.Tools.Unity
{
    /// <summary>
    /// Provides tools for <see cref="Object"/>'s instantiation
    /// </summary>
    public static class Instantiater
    {
        #region Fields
        private static event InstantiatingObjectEventHandler _instantiatingObject;
        private static event ObjectInstantiatedEventHandler _objectInstantiated;
        #endregion

        #region Events
        private static event InstantiatingObjectEventHandler InstantiatingObject
        {
            add
            {
                if (AllowsMultipleEqualHandler || _instantiatingObject == null || !_instantiatingObject.GetInvocationList().Any(element => element.Method == value.Method))
                {
                    _instantiatingObject += value;
                }
            }
            remove { _instantiatingObject -= value; }
        }
        private static event ObjectInstantiatedEventHandler ObjectInstantiated
        {
            add
            {
                if (AllowsMultipleEqualHandler || _objectInstantiated == null || !_objectInstantiated.GetInvocationList().Any(element => element.Method == value.Method))
                {
                    _objectInstantiated += value;
                }
            }
            remove { _objectInstantiated -= value; }
        }
        #endregion

        #region Properties
        public static bool AllowsMultipleEqualHandler { get; set; } = false;
        #endregion

        #region Methods
        /// <summary>
        /// Instantiates the given <see cref="Object"/> and fires related events
        /// </summary>
        /// <param name="instantiatedObject"><see cref="Object"/> to instantiate</param>
        /// <param name="instantiaterPath">This argument is automatically provided, please do not provide it</param>
        public static void Instantiate<InstantiatedT>(InstantiatedT instantiatedObject, [CallerFilePath] string instantiaterPath = "") where InstantiatedT : Object
        {
            string instantiaterName = instantiaterPath.Remove(instantiaterPath.Length - 3).Split('\\').Last();
            InstiatingObjectEventArgs<InstantiatedT> instantiatingArgs = new InstiatingObjectEventArgs<InstantiatedT>(instantiatedObject, instantiaterName);
            ObjectInstiatedEventArgs<InstantiatedT> instantiatedArgs = new ObjectInstiatedEventArgs<InstantiatedT>(instantiatedObject, instantiaterName);
            InstiatingObjectEventArgs<Object> sentInstantiatingArgs = (InstiatingObjectEventArgs<Object>)instantiatingArgs;
            ObjectInstiatedEventArgs<Object> sentInstantiatedArgs = (ObjectInstiatedEventArgs<Object>)instantiatedArgs;
            _instantiatingObject?.Invoke(sentInstantiatingArgs);
            if (!sentInstantiatingArgs.Cancel)
            {
                Object.Instantiate(instantiatedObject);
                _objectInstantiated?.Invoke(sentInstantiatedArgs);
            }
        }
        #endregion

        #region Nested Classes (makes things clearer for users)
        public static class Instantiating
        {
            public static void AddHandler<U>(System.Action<InstiatingObjectEventArgs<U>> handler) where U : Object
            {
                InstantiatingObject += (e) =>
                {
                    if (typeof(U).Inherits(e.GetType().GenericTypeArguments[0]) || typeof(U) == e.GetType().GenericTypeArguments[0])
                    {
                        handler((InstiatingObjectEventArgs<U>)e);
                    }
                };
            }
        }

        public static class Instantiated
        {
            public static void AddHandler<U>(System.Action<ObjectInstiatedEventArgs<U>> handler) where U : Object
            {
                ObjectInstantiated += (e) =>
                {
                    if (typeof(U).Inherits(e.GetType().GenericTypeArguments[0]) || typeof(U) == e.GetType().GenericTypeArguments[0])
                    {
                        handler((ObjectInstiatedEventArgs<U>)e);
                    }
                };
            }
        }
        #endregion
    }
}