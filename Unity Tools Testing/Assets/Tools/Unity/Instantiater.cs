using Nalka.Tools.Extensions;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections.Generic;

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

        #region Properties
        /// <summary>
        /// Determines if the handlers have to be invoked even if they have already been invoked by the same destruction
        /// </summary>
        public static bool AllowsMultipleEqualHandler { get; set; } = false;
        #endregion

        #region Methods
        /// <summary>
        /// Instantiates the given <see cref="Object"/> and fires related events
        /// </summary>
        /// <param name="instantiatedObject"><see cref="Object"/> to instantiate</param>
        /// <param name="msDelay">The number of milliseconds to wait before instantiating the given <see cref="Object"/></param>
        /// <param name="instantiaterPath">This argument is automatically provided, please do not provide it</param>
        public async static void Instantiate<InstantiatedT>(InstantiatedT instantiatedObject, int msDelay = 0, [CallerFilePath] string instantiaterPath = "") where InstantiatedT : Object
        {
            string instantiaterName = instantiaterPath.Remove(instantiaterPath.Length - 3).Split('\\').Last();
            await Task.Delay(msDelay);
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

        /// <summary>
        /// Instantiates the given <see cref="Object"/>, sets <paramref name="parent"/> as parent and fires related events
        /// </summary>
        /// <param name="instantiatedObject"><see cref="Object"/> to instantiate</param>
        /// <param name="parent"><see cref="Transform"/> to set as parent of the <paramref name="instantiatedObject"/></param>
        /// <param name="msDelay">The number of milliseconds to wait before instantiating the given <see cref="Object"/></param>
        /// <param name="instantiaterPath">This argument is automatically provided, please do not provide it</param>
        public async static void Instantiate<InstantiatedT>(InstantiatedT instantiatedObject, Transform parent, int msDelay = 0, [CallerFilePath] string instantiaterPath = "") where InstantiatedT : Object
        {
            string instantiaterName = instantiaterPath.Remove(instantiaterPath.Length - 3).Split('\\').Last();
            await Task.Delay(msDelay);
            InstiatingObjectEventArgs<InstantiatedT> instantiatingArgs = new InstiatingObjectEventArgs<InstantiatedT>(instantiatedObject, instantiaterName);
            ObjectInstiatedEventArgs<InstantiatedT> instantiatedArgs = new ObjectInstiatedEventArgs<InstantiatedT>(instantiatedObject, instantiaterName);
            InstiatingObjectEventArgs<Object> sentInstantiatingArgs = (InstiatingObjectEventArgs<Object>)instantiatingArgs;
            ObjectInstiatedEventArgs<Object> sentInstantiatedArgs = (ObjectInstiatedEventArgs<Object>)instantiatedArgs;
            _instantiatingObject?.Invoke(sentInstantiatingArgs);
            if (!sentInstantiatingArgs.Cancel)
            {
                Object.Instantiate(instantiatedObject, parent);
                _objectInstantiated?.Invoke(sentInstantiatedArgs);
            }
        }

        /// <summary>
        /// Instantiates the given <see cref="Object"/>, sets <paramref name="parent"/> as parent and fires related events
        /// </summary>
        /// <param name="instantiatedObject"><see cref="Object"/> to instantiate</param>
        /// <param name="parent"><see cref="Transform"/> to set as parent of the <paramref name="instantiatedObject"/></param>
        /// <param name="msDelay">The number of milliseconds to wait before instantiating the given <see cref="Object"/></param>
        /// <param name="worldPositionStays">Determines if the world position has to be kept</param>
        /// <param name="instantiaterPath">This argument is automatically provided, please do not provide it</param>
        public async static void Instantiate<InstantiatedT>(InstantiatedT instantiatedObject, Transform parent, bool worldPositionStays, int msDelay = 0, [CallerFilePath] string instantiaterPath = "") where InstantiatedT : Object
        {
            string instantiaterName = instantiaterPath.Remove(instantiaterPath.Length - 3).Split('\\').Last();
            await Task.Delay(msDelay);
            InstiatingObjectEventArgs<InstantiatedT> instantiatingArgs = new InstiatingObjectEventArgs<InstantiatedT>(instantiatedObject, instantiaterName);
            ObjectInstiatedEventArgs<InstantiatedT> instantiatedArgs = new ObjectInstiatedEventArgs<InstantiatedT>(instantiatedObject, instantiaterName);
            InstiatingObjectEventArgs<Object> sentInstantiatingArgs = (InstiatingObjectEventArgs<Object>)instantiatingArgs;
            ObjectInstiatedEventArgs<Object> sentInstantiatedArgs = (ObjectInstiatedEventArgs<Object>)instantiatedArgs;
            _instantiatingObject?.Invoke(sentInstantiatingArgs);
            if (!sentInstantiatingArgs.Cancel)
            {
                Object.Instantiate(instantiatedObject, parent, worldPositionStays);
                _objectInstantiated?.Invoke(sentInstantiatedArgs);
            }
        }

        /// <summary>
        /// Instantiates the given <see cref="Object"/> with given world position and rotation and fires related events
        /// </summary>
        /// <param name="instantiatedObject"><see cref="Object"/> to instantiate</param>
        /// <param name="position">The <see cref="Space.World"/> position of the <paramref name="instantiatedObject"/></param>
        /// <param name="rotation">The <see cref="Space.World"/> position of the <paramref name="instantiatedObject"/></param>
        /// <param name="msDelay">The number of milliseconds to wait before instantiating the given <see cref="Object"/></param>
        /// <param name="instantiaterPath">This argument is automatically provided, please do not provide it</param>
        public async static void Instantiate<InstantiatedT>(InstantiatedT instantiatedObject, Vector3 position, Quaternion rotation, int msDelay = 0, [CallerFilePath] string instantiaterPath = "") where InstantiatedT : Object
        {
            string instantiaterName = instantiaterPath.Remove(instantiaterPath.Length - 3).Split('\\').Last();
            await Task.Delay(msDelay);
            InstiatingObjectEventArgs<InstantiatedT> instantiatingArgs = new InstiatingObjectEventArgs<InstantiatedT>(instantiatedObject, instantiaterName);
            ObjectInstiatedEventArgs<InstantiatedT> instantiatedArgs = new ObjectInstiatedEventArgs<InstantiatedT>(instantiatedObject, instantiaterName);
            InstiatingObjectEventArgs<Object> sentInstantiatingArgs = (InstiatingObjectEventArgs<Object>)instantiatingArgs;
            ObjectInstiatedEventArgs<Object> sentInstantiatedArgs = (ObjectInstiatedEventArgs<Object>)instantiatedArgs;
            _instantiatingObject?.Invoke(sentInstantiatingArgs);
            if (!sentInstantiatingArgs.Cancel)
            {
                Object.Instantiate(instantiatedObject, position, rotation);
                _objectInstantiated?.Invoke(sentInstantiatedArgs);
            }
        }

        /// <summary>
        /// Instantiates the given <see cref="Object"/> with given local position and rotation and fires related events
        /// </summary>
        /// <param name="instantiatedObject"><see cref="Object"/> to instantiate</param>
        /// <param name="position">The <see cref="Space.World"/> position of the <paramref name="instantiatedObject"/></param>
        /// <param name="rotation">The <see cref="Space.World"/> position of the <paramref name="instantiatedObject"/></param>
        /// <param name="parent"><see cref="Transform"/> to set as parent of the <paramref name="instantiatedObject"/></param>
        /// <param name="msDelay">The number of milliseconds to wait before instantiating the given <see cref="Object"/></param>
        /// <param name="instantiaterPath">This argument is automatically provided, please do not provide it</param>
        public async static void Instantiate<InstantiatedT>(InstantiatedT instantiatedObject, Vector3 position, Quaternion rotation, Transform parent, int msDelay = 0, [CallerFilePath] string instantiaterPath = "") where InstantiatedT : Object
        {
            string instantiaterName = instantiaterPath.Remove(instantiaterPath.Length - 3).Split('\\').Last();
            await Task.Delay(msDelay);
            InstiatingObjectEventArgs<InstantiatedT> instantiatingArgs = new InstiatingObjectEventArgs<InstantiatedT>(instantiatedObject, instantiaterName);
            ObjectInstiatedEventArgs<InstantiatedT> instantiatedArgs = new ObjectInstiatedEventArgs<InstantiatedT>(instantiatedObject, instantiaterName);
            InstiatingObjectEventArgs<Object> sentInstantiatingArgs = (InstiatingObjectEventArgs<Object>)instantiatingArgs;
            ObjectInstiatedEventArgs<Object> sentInstantiatedArgs = (ObjectInstiatedEventArgs<Object>)instantiatedArgs;
            _instantiatingObject?.Invoke(sentInstantiatingArgs);
            if (!sentInstantiatingArgs.Cancel)
            {
                Object.Instantiate(instantiatedObject, position, rotation);
                _objectInstantiated?.Invoke(sentInstantiatedArgs);
            }
        }
        #endregion

        #region Nested Classes (makes things clearer for users)
        /// <summary>
        /// Handlers added here will be invoked when before the <see cref="Object"/>'s instantiation
        /// </summary>
        public static class Instantiating<InstantiatedType> where InstantiatedType : Object
        {
            private static List<System.Action<InstiatingObjectEventArgs<InstantiatedType>>> AddedMethods = new List<System.Action<InstiatingObjectEventArgs<InstantiatedType>>>();
            public static void AddHandler(System.Action<InstiatingObjectEventArgs<InstantiatedType>> method)
            {
                if (AllowsMultipleEqualHandler || !AddedMethods.Contains(method))
                {
                    _instantiatingObject += (e) =>
                    {
                        if (typeof(InstantiatedType).Inherits(e.GetType().GenericTypeArguments[0]) || typeof(InstantiatedType) == e.GetType().GenericTypeArguments[0])
                        {
                            method((InstiatingObjectEventArgs<InstantiatedType>)e);
                        }
                    };
                }
            }

            [System.Obsolete("Not implemented", true)]
            public static void Clear()
            {
                AddedMethods.Clear();
                _instantiatingObject = null;
            }
        }
        
        /// <summary>
        /// Handlers added here will be invoked when after the <see cref="Object"/>'s instantiation
        /// </summary>
        public static class Instantiated<InstantiatedType> where InstantiatedType : Object
        {

            private static List<System.Action<ObjectInstiatedEventArgs<InstantiatedType>>> AddedMethods = new List<System.Action<ObjectInstiatedEventArgs<InstantiatedType>>>();
            public static void AddHandler(System.Action<ObjectInstiatedEventArgs<InstantiatedType>> method) 
            {
                if (AllowsMultipleEqualHandler || !AddedMethods.Contains(method))
                {
                    AddedMethods.Add(method);
                    _objectInstantiated += (e) =>
                    {
                        if (typeof(InstantiatedType).Inherits(e.GetType().GenericTypeArguments[0]) || typeof(InstantiatedType) == e.GetType().GenericTypeArguments[0])
                        {
                            method((ObjectInstiatedEventArgs<InstantiatedType>)e);
                        }
                    };
                }
            }

            [System.Obsolete("Not implemented", true)]
            public static void Clear()
            {

            }
        }
        #endregion
    }
}