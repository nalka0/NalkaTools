using Nalka.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityObject = UnityEngine.Object;

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
        /// Determines if the handlers have to be added even if they have already been added
        /// </summary>
        public static bool AllowsMultipleEqualHandler { get; set; } = false;
        #endregion

        #region Methods
        /// <summary>
        /// Instantiates the given <see cref="UnityEngine.Object"/> and fires related events
        /// </summary>
        /// <param name="instantiatedObject"><see cref="UnityEngine.Object"/> to instantiate</param>
        /// <param name="msDelay">The number of milliseconds to wait before instantiating the given <see cref="UnityEngine.Object"/></param>
        /// <param name="instantiaterPath">This argument is automatically provided, please do not provide it</param>
        public static async Task Instantiate<InstantiatedT>(InstantiatedT instantiatedObject, int msDelay = 0, [CallerFilePath] string instantiaterPath = "") where InstantiatedT : UnityObject
        {
            string instantiaterName = instantiaterPath.Remove(instantiaterPath.Length - 3).Split('\\').Last();
            await Task.Delay(msDelay);
            InstantiatingObjectEventArgs<InstantiatedT> instantiatingArgs = new InstantiatingObjectEventArgs<InstantiatedT>(instantiatedObject, instantiaterName);
            ObjectInstiatedEventArgs<InstantiatedT> instantiatedArgs = new ObjectInstiatedEventArgs<InstantiatedT>(instantiatedObject, instantiaterName);
            InstantiatingObjectEventArgs<UnityObject> sentInstantiatingArgs = (InstantiatingObjectEventArgs<UnityObject>)instantiatingArgs;
            ObjectInstiatedEventArgs<UnityObject> sentInstantiatedArgs = (ObjectInstiatedEventArgs<UnityObject>)instantiatedArgs;
            _instantiatingObject?.Invoke(sentInstantiatingArgs);
            if (!sentInstantiatingArgs.Cancel)
            {
                UnityObject.Instantiate(instantiatedObject);
                _objectInstantiated?.Invoke(sentInstantiatedArgs);
            }
        }

        /// <summary>
        /// Instantiates the given <see cref="UnityEngine.Object"/>, sets <paramref name="parent"/> as parent and fires related events
        /// </summary>
        /// <param name="instantiatedObject"><see cref="UnityEngine.Object"/> to instantiate</param>
        /// <param name="parent"><see cref="Transform"/> to set as parent of the <paramref name="instantiatedObject"/></param>
        /// <param name="msDelay">The number of milliseconds to wait before instantiating the given <see cref="UnityEngine.Object"/></param>
        /// <param name="instantiaterPath">This argument is automatically provided, please do not provide it</param>
        public static async Task Instantiate<InstantiatedT>(InstantiatedT instantiatedObject, Transform parent, int msDelay = 0, [CallerFilePath] string instantiaterPath = "") where InstantiatedT : UnityObject
        {
            string instantiaterName = instantiaterPath.Remove(instantiaterPath.Length - 3).Split('\\').Last();
            await Task.Delay(msDelay);
            InstantiatingObjectEventArgs<InstantiatedT> instantiatingArgs = new InstantiatingObjectEventArgs<InstantiatedT>(instantiatedObject, instantiaterName);
            ObjectInstiatedEventArgs<InstantiatedT> instantiatedArgs = new ObjectInstiatedEventArgs<InstantiatedT>(instantiatedObject, instantiaterName);
            InstantiatingObjectEventArgs<UnityObject> sentInstantiatingArgs = (InstantiatingObjectEventArgs<UnityObject>)instantiatingArgs;
            ObjectInstiatedEventArgs<UnityObject> sentInstantiatedArgs = (ObjectInstiatedEventArgs<UnityObject>)instantiatedArgs;
            _instantiatingObject?.Invoke(sentInstantiatingArgs);
            if (!sentInstantiatingArgs.Cancel)
            {
                UnityObject.Instantiate(instantiatedObject, parent);
                _objectInstantiated?.Invoke(sentInstantiatedArgs);
            }
        }

        /// <summary>
        /// Instantiates the given <see cref="UnityEngine.Object"/>, sets <paramref name="parent"/> as parent and fires related events
        /// </summary>
        /// <param name="instantiatedObject"><see cref="UnityEngine.Object"/> to instantiate</param>
        /// <param name="parent"><see cref="Transform"/> to set as parent of the <paramref name="instantiatedObject"/></param>
        /// <param name="msDelay">The number of milliseconds to wait before instantiating the given <see cref="UnityEngine.Object"/></param>
        /// <param name="worldPositionStays">Determines if the world position has to be kept</param>
        /// <param name="instantiaterPath">This argument is automatically provided, please do not provide it</param>
        public static async Task Instantiate<InstantiatedT>(InstantiatedT instantiatedObject, Transform parent, bool worldPositionStays, int msDelay = 0, [CallerFilePath] string instantiaterPath = "") where InstantiatedT : UnityObject
        {
            string instantiaterName = instantiaterPath.Remove(instantiaterPath.Length - 3).Split('\\').Last();
            await Task.Delay(msDelay);
            InstantiatingObjectEventArgs<InstantiatedT> instantiatingArgs = new InstantiatingObjectEventArgs<InstantiatedT>(instantiatedObject, instantiaterName);
            ObjectInstiatedEventArgs<InstantiatedT> instantiatedArgs = new ObjectInstiatedEventArgs<InstantiatedT>(instantiatedObject, instantiaterName);
            InstantiatingObjectEventArgs<UnityObject> sentInstantiatingArgs = (InstantiatingObjectEventArgs<UnityObject>)instantiatingArgs;
            ObjectInstiatedEventArgs<UnityObject> sentInstantiatedArgs = (ObjectInstiatedEventArgs<UnityObject>)instantiatedArgs;
            _instantiatingObject?.Invoke(sentInstantiatingArgs);
            if (!sentInstantiatingArgs.Cancel)
            {
                UnityObject.Instantiate(instantiatedObject, parent, worldPositionStays);
                _objectInstantiated?.Invoke(sentInstantiatedArgs);
            }
        }

        /// <summary>
        /// Instantiates the given <see cref="UnityEngine.Object"/> with given world position and rotation and fires related events
        /// </summary>
        /// <param name="instantiatedObject"><see cref="UnityEngine.Object"/> to instantiate</param>
        /// <param name="position">The <see cref="Space.World"/> position of the <paramref name="instantiatedObject"/></param>
        /// <param name="rotation">The <see cref="Space.World"/> position of the <paramref name="instantiatedObject"/></param>
        /// <param name="msDelay">The number of milliseconds to wait before instantiating the given <see cref="UnityEngine.Object"/></param>
        /// <param name="instantiaterPath">This argument is automatically provided, please do not provide it</param>
        public static async Task Instantiate<InstantiatedT>(InstantiatedT instantiatedObject, Vector3 position, Quaternion rotation, int msDelay = 0, [CallerFilePath] string instantiaterPath = "") where InstantiatedT : UnityObject
        {
            string instantiaterName = instantiaterPath.Remove(instantiaterPath.Length - 3).Split('\\').Last();
            await Task.Delay(msDelay);
            InstantiatingObjectEventArgs<InstantiatedT> instantiatingArgs = new InstantiatingObjectEventArgs<InstantiatedT>(instantiatedObject, instantiaterName);
            ObjectInstiatedEventArgs<InstantiatedT> instantiatedArgs = new ObjectInstiatedEventArgs<InstantiatedT>(instantiatedObject, instantiaterName);
            InstantiatingObjectEventArgs<UnityObject> sentInstantiatingArgs = (InstantiatingObjectEventArgs<UnityObject>)instantiatingArgs;
            ObjectInstiatedEventArgs<UnityObject> sentInstantiatedArgs = (ObjectInstiatedEventArgs<UnityObject>)instantiatedArgs;
            _instantiatingObject?.Invoke(sentInstantiatingArgs);
            if (!sentInstantiatingArgs.Cancel)
            {
                UnityObject.Instantiate(instantiatedObject, position, rotation);
                _objectInstantiated?.Invoke(sentInstantiatedArgs);
            }
        }

        /// <summary>
        /// Instantiates the given <see cref="UnityEngine.Object"/> with given local position and rotation and fires related events
        /// </summary>
        /// <param name="instantiatedObject"><see cref="UnityEngine.Object"/> to instantiate</param>
        /// <param name="position">The <see cref="Space.World"/> position of the <paramref name="instantiatedObject"/></param>
        /// <param name="rotation">The <see cref="Space.World"/> position of the <paramref name="instantiatedObject"/></param>
        /// <param name="parent"><see cref="Transform"/> to set as parent of the <paramref name="instantiatedObject"/></param>
        /// <param name="msDelay">The number of milliseconds to wait before instantiating the given <see cref="UnityEngine.Object"/></param>
        /// <param name="instantiaterPath">This argument is automatically provided, please do not provide it</param>
        public static async Task Instantiate<InstantiatedT>(InstantiatedT instantiatedObject, Vector3 position, Quaternion rotation, Transform parent, int msDelay = 0, [CallerFilePath] string instantiaterPath = "") where InstantiatedT : UnityObject
        {
            string instantiaterName = instantiaterPath.Remove(instantiaterPath.Length - 3).Split('\\').Last();
            await Task.Delay(msDelay);
            InstantiatingObjectEventArgs<InstantiatedT> instantiatingArgs = new InstantiatingObjectEventArgs<InstantiatedT>(instantiatedObject, instantiaterName);
            ObjectInstiatedEventArgs<InstantiatedT> instantiatedArgs = new ObjectInstiatedEventArgs<InstantiatedT>(instantiatedObject, instantiaterName);
            InstantiatingObjectEventArgs<UnityObject> sentInstantiatingArgs = (InstantiatingObjectEventArgs<UnityObject>)instantiatingArgs;
            ObjectInstiatedEventArgs<UnityObject> sentInstantiatedArgs = (ObjectInstiatedEventArgs<UnityObject>)instantiatedArgs;
            _instantiatingObject?.Invoke(sentInstantiatingArgs);
            if (!sentInstantiatingArgs.Cancel)
            {
                UnityObject.Instantiate(instantiatedObject, position, rotation);
                _objectInstantiated?.Invoke(sentInstantiatedArgs);
            }
        }
        [Obsolete]
        public static void Test()
        {
            foreach (Delegate element in _instantiatingObject.GetInvocationList())
            {
                Debug.Log($"{_instantiatingObject.GetInvocationList().Any(e => element.Method == e.Method)}");
            }
        }
        #endregion

        #region Nested Classes (makes things clearer for users)
        /// <summary>
        /// Handlers added here will be invoked when before the <see cref="UnityEngine.Object"/>'s instantiation
        /// </summary>
        public static class Instantiating<InstantiatedType> where InstantiatedType : UnityObject
        {
            private static List<MethodInfo> AddedMethods = new List<MethodInfo>();
            public static void AddHandler(Action<InstantiatingObjectEventArgs<InstantiatedType>> method)
            {
                if (AllowsMultipleEqualHandler || !AddedMethods.Contains(method.Method))
                {
                    AddedMethods.Add(method.Method);
                    _instantiatingObject += (e) =>
                    {
                        if (e.InstantiatedObject is InstantiatedType && (typeof(InstantiatedType).Inherits(e.GetType().GenericTypeArguments[0]) || typeof(InstantiatedType) == e.GetType().GenericTypeArguments[0]))
                        {
                            method((InstantiatingObjectEventArgs<InstantiatedType>)e);
                        }
                    };
                }
            }

            [Obsolete("Not implemented", true)]
            public static void Clear()
            {
                _instantiatingObject = null;
                AddedMethods.Clear();
            }
        }

        /// <summary>
        /// Handlers added here will be invoked when after the <see cref="UnityEngine.Object"/>'s instantiation
        /// </summary>
        public static class Instantiated<InstantiatedType> where InstantiatedType : UnityObject
        {
            private static List<MethodInfo> AddedMethods = new List<MethodInfo>();
            public static void AddHandler(Action<ObjectInstiatedEventArgs<InstantiatedType>> method)
            {
                if (AllowsMultipleEqualHandler || !AddedMethods.Contains(method.Method))
                {
                    AddedMethods.Add(method.Method);
                    _objectInstantiated += (e) =>
                    {
                        if (e.InstantiatedObject is InstantiatedType && (typeof(InstantiatedType).Inherits(e.GetType().GenericTypeArguments[0]) || typeof(InstantiatedType) == e.GetType().GenericTypeArguments[0]))
                        {
                            method((ObjectInstiatedEventArgs<InstantiatedType>)e);
                        }
                    };
                }
            }

            [Obsolete("Not implemented", false)]
            public static void Clear()
            {
                _objectInstantiated = null;
                AddedMethods.Clear();
            }
        }
        #endregion
    }
}