using Nalka.Tools.Extensions;
using Nalka.Tools.Internal;
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
    /// Provides tools for <see cref="UnityEngine.Object"/>'s instantiation
    /// </summary>
    public static class Instantiater
    {
        #region Fields
        private static event InstantiatingObjectEventHandler _instantiatingObject;
        private static event ObjectInstantiatedEventHandler _objectInstantiated;
        #endregion

        #region Properties
        /// <summary>
        /// Determines if the handlers have to be added even if they have already been added.
        /// </summary>
        /// <value><see langword="false"/></value>
        public static bool AllowsMultipleEqualHandler { get; set; } = false;
        #endregion

        #region Methods

        #region Instantiate(UnityEngine.Object original)
        /// <summary>
        /// Instantiates the given <see cref="UnityEngine.Object"/> and fires related events
        /// </summary>
        /// <param name="instantiatedObject"><see cref="UnityEngine.Object"/> to instantiate</param>
        /// <param name="msDelay">The number of milliseconds to wait before instantiating the given <see cref="UnityEngine.Object"/></param>
        /// <param name="instantiaterPath">This argument is automatically provided, please do not provide it</param>
        public static async Task Instantiate<InstantiatedT>(InstantiatedT instantiatedObject, int msDelay = 0, [CallerFilePath, DontProvide] string instantiaterPath = "") where InstantiatedT : UnityObject
        {
            bool raiseInstantiated = true;
            if (instantiatedObject is Transform transform)
            {
                await Instantiate(transform.gameObject, msDelay, instantiaterPath);
            }
            else
            {
                string instantiaterName = instantiaterPath.Remove(instantiaterPath.Length - 3).Split('\\').Last();
                await Task.Delay(msDelay);
                if (instantiatedObject is GameObject InstantiatedGameObject)
                {
                    foreach (Component element in InstantiatedGameObject.GetComponents<Component>())
                    {
                        if (element is Transform transfo)
                        {
                            foreach (Transform item in transfo)
                            {
                                if (item.gameObject != InstantiatedGameObject)
                                {
                                    if (!InstantiatingMethod(item.gameObject, instantiaterName))
                                    {
                                        raiseInstantiated = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (!InstantiatingMethod(element, instantiaterName))
                                raiseInstantiated = false;
                        }
                    }
                    if (raiseInstantiated)
                    {
                        foreach (Component element in InstantiatedGameObject.GetComponents<Component>())
                        {
                            if (element is Transform transfo)
                            {
                                foreach (Transform item in transfo)
                                {
                                    if (item.gameObject != InstantiatedGameObject)
                                    {
                                        InstantiatedMethod(item.gameObject, instantiaterName);
                                    }
                                }
                            }
                            else
                            {
                                InstantiatedMethod(element, instantiaterName);
                            }
                        }
                    }
                }
            }
        }

        private static bool InstantiatingMethod<InstantiatedT>(InstantiatedT InstantiatedObject, string instantiaterName) where InstantiatedT : UnityObject
        {
            InstantiatingObjectEventArgs<InstantiatedT> InstantiatingArgs = new InstantiatingObjectEventArgs<InstantiatedT>(InstantiatedObject, instantiaterName);
            InstantiatingObjectEventArgs<UnityObject> SentInstantiatingArgs = (InstantiatingObjectEventArgs<UnityObject>)InstantiatingArgs;
            _instantiatingObject?.Invoke(SentInstantiatingArgs);
            return !SentInstantiatingArgs.Cancel;
        }

        private static void InstantiatedMethod<InstantiatedT>(InstantiatedT InstantiatedObject, string instantiaterName) where InstantiatedT : UnityObject
        {
            ObjectInstiatedEventArgs<InstantiatedT> InstantiatedArgs = new ObjectInstiatedEventArgs<InstantiatedT>(InstantiatedObject, instantiaterName);
            ObjectInstiatedEventArgs<UnityObject> SentInstantiatedArgs = (ObjectInstiatedEventArgs<UnityObject>)InstantiatedArgs;
            UnityObject.Instantiate(InstantiatedObject);
            _objectInstantiated?.Invoke(SentInstantiatedArgs);
        }
        #endregion

        #region Instantiate(UnityEngine.Object original, Vector3 position, Quaternion rotation)
        /// <summary>
        /// Instantiates the given <see cref="UnityEngine.Object"/> with given world position and rotation and fires related events
        /// </summary>
        /// <param name="instantiatedObject"><see cref="UnityEngine.Object"/> to instantiate</param>
        /// <param name="position">The <see cref="Space.World"/> position of the <paramref name="instantiatedObject"/></param>
        /// <param name="rotation">The <see cref="Space.World"/> position of the <paramref name="instantiatedObject"/></param>
        /// <param name="msDelay">The number of milliseconds to wait before instantiating the given <see cref="UnityEngine.Object"/></param>
        /// <param name="instantiaterPath">This argument is automatically provided, please do not provide it</param>
        public static async Task Instantiate<InstantiatedT>(InstantiatedT instantiatedObject, Vector3 position, Quaternion rotation, int msDelay = 0, [CallerFilePath, DontProvide] string instantiaterPath = "") where InstantiatedT : UnityObject
        {
            bool raiseInstantiated = true;
            if (instantiatedObject is Transform transform)
            {
                await Instantiate(transform.gameObject, position, rotation, msDelay, instantiaterPath);
            }
            else
            {
                string instantiaterName = instantiaterPath.Remove(instantiaterPath.Length - 3).Split('\\').Last();
                await Task.Delay(msDelay);
                if (instantiatedObject is GameObject InstantiatedGameObject)
                {
                    foreach (Component element in InstantiatedGameObject.GetComponents<Component>())
                    {
                        if (element is Transform transfo)
                        {
                            foreach (Transform item in transfo)
                            {
                                if (item.gameObject != InstantiatedGameObject)
                                {
                                    if (!InstantiatingMethod(item.gameObject, position, rotation, instantiaterName))
                                    {
                                        raiseInstantiated = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (!InstantiatingMethod(element, position, rotation, instantiaterName))
                                raiseInstantiated = false;
                        }
                    }
                    if (raiseInstantiated)
                    {
                        foreach (Component element in InstantiatedGameObject.GetComponents<Component>())
                        {
                            if (element is Transform transfo)
                            {
                                foreach (Transform item in transfo)
                                {
                                    if (item.gameObject != InstantiatedGameObject)
                                    {
                                        InstantiatedMethod(item.gameObject, position, rotation, instantiaterName);
                                    }
                                }
                            }
                            else
                            {
                                InstantiatedMethod(element, position, rotation, instantiaterName);
                            }
                        }
                    }
                }
            }
        }

        private static bool InstantiatingMethod<InstantiatedT>(InstantiatedT InstantiatedObject, Vector3 position, Quaternion rotation, string instantiaterName) where InstantiatedT : UnityObject
        {
            InstantiatingObjectEventArgs<InstantiatedT> InstantiatingArgs = new InstantiatingObjectEventArgs<InstantiatedT>(InstantiatedObject, instantiaterName);
            InstantiatingObjectEventArgs<UnityObject> SentInstantiatingArgs = (InstantiatingObjectEventArgs<UnityObject>)InstantiatingArgs;
            _instantiatingObject?.Invoke(SentInstantiatingArgs);
            return !SentInstantiatingArgs.Cancel;
        }

        private static void InstantiatedMethod<InstantiatedT>(InstantiatedT InstantiatedObject, Vector3 position, Quaternion rotation, string instantiaterName) where InstantiatedT : UnityObject
        {
            ObjectInstiatedEventArgs<InstantiatedT> InstantiatedArgs = new ObjectInstiatedEventArgs<InstantiatedT>(InstantiatedObject, instantiaterName);
            ObjectInstiatedEventArgs<UnityObject> SentInstantiatedArgs = (ObjectInstiatedEventArgs<UnityObject>)InstantiatedArgs;
            UnityObject.Instantiate(InstantiatedObject, position, rotation);
            _objectInstantiated?.Invoke(SentInstantiatedArgs);
        }
        #endregion

        #region Instantiate(UnityEngine.Object original, Vector3 position, Quaternion rotation, Transform parent)
        /// <summary>
        /// Instantiates the given <see cref="UnityEngine.Object"/> with given local position and rotation and fires related events
        /// </summary>
        /// <param name="instantiatedObject"><see cref="UnityEngine.Object"/> to instantiate</param>
        /// <param name="position">The <see cref="Space.World"/> position of the <paramref name="instantiatedObject"/></param>
        /// <param name="rotation">The <see cref="Space.World"/> position of the <paramref name="instantiatedObject"/></param>
        /// <param name="parent"><see cref="Transform"/> to set as parent of the <paramref name="instantiatedObject"/></param>
        /// <param name="msDelay">The number of milliseconds to wait before instantiating the given <see cref="UnityEngine.Object"/></param>
        /// <param name="instantiaterPath">This argument is automatically provided, please do not provide it</param>
        public static async Task Instantiate<InstantiatedT>(InstantiatedT instantiatedObject, Vector3 position, Quaternion rotation, Transform parent, int msDelay = 0, [CallerFilePath, DontProvide] string instantiaterPath = "") where InstantiatedT : UnityObject
        {
            bool raiseInstantiated = true;
            if (instantiatedObject is Transform transform)
            {
                await Instantiate(transform.gameObject, position, rotation, parent, msDelay, instantiaterPath);
            }
            else
            {
                string instantiaterName = instantiaterPath.Remove(instantiaterPath.Length - 3).Split('\\').Last();
                await Task.Delay(msDelay);
                if (instantiatedObject is GameObject InstantiatedGameObject)
                {
                    foreach (Component element in InstantiatedGameObject.GetComponents<Component>())
                    {
                        if (element is Transform transfo)
                        {
                            foreach (Transform item in transfo)
                            {
                                if (item.gameObject != InstantiatedGameObject)
                                {
                                    if (!InstantiatingMethod(item.gameObject, position, rotation, parent, instantiaterName))
                                    {
                                        raiseInstantiated = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (!InstantiatingMethod(element, position, rotation, parent, instantiaterName))
                                raiseInstantiated = false;
                        }
                    }
                    if (raiseInstantiated)
                    {
                        foreach (Component element in InstantiatedGameObject.GetComponents<Component>())
                        {
                            if (element is Transform transfo)
                            {
                                foreach (Transform item in transfo)
                                {
                                    if (item.gameObject != InstantiatedGameObject)
                                    {
                                        InstantiatedMethod(item.gameObject, position, rotation, parent, instantiaterName);
                                    }
                                }
                            }
                            else
                            {
                                InstantiatedMethod(element, position, rotation, parent, instantiaterName);
                            }
                        }
                    }
                }
            }
        }

        private static bool InstantiatingMethod<InstantiatedT>(InstantiatedT InstantiatedObject, Vector3 position, Quaternion rotation, Transform parent, string instantiaterName) where InstantiatedT : UnityObject
        {
            InstantiatingObjectEventArgs<InstantiatedT> InstantiatingArgs = new InstantiatingObjectEventArgs<InstantiatedT>(InstantiatedObject, instantiaterName);
            InstantiatingObjectEventArgs<UnityObject> SentInstantiatingArgs = (InstantiatingObjectEventArgs<UnityObject>)InstantiatingArgs;
            _instantiatingObject?.Invoke(SentInstantiatingArgs);
            return !SentInstantiatingArgs.Cancel;
        }

        private static void InstantiatedMethod<InstantiatedT>(InstantiatedT InstantiatedObject, Vector3 position, Quaternion rotation, Transform parent, string instantiaterName) where InstantiatedT : UnityObject
        {
            ObjectInstiatedEventArgs<InstantiatedT> InstantiatedArgs = new ObjectInstiatedEventArgs<InstantiatedT>(InstantiatedObject, instantiaterName);
            ObjectInstiatedEventArgs<UnityObject> SentInstantiatedArgs = (ObjectInstiatedEventArgs<UnityObject>)InstantiatedArgs;
            UnityObject.Instantiate(InstantiatedObject, position, rotation, parent);
            _objectInstantiated?.Invoke(SentInstantiatedArgs);
        }
        #endregion

        #region Instantiate(UnityEngine.Object original, Transform parent)
        /// <summary>
        /// Instantiates the given <see cref="UnityEngine.Object"/>, sets <paramref name="parent"/> as parent and fires related events
        /// </summary>
        /// <param name="instantiatedObject"><see cref="UnityEngine.Object"/> to instantiate</param>
        /// <param name="parent"><see cref="Transform"/> to set as parent of the <paramref name="instantiatedObject"/></param>
        /// <param name="msDelay">The number of milliseconds to wait before instantiating the given <see cref="UnityEngine.Object"/></param>
        /// <param name="instantiaterPath">This argument is automatically provided, please do not provide it</param>
        public static async Task Instantiate<InstantiatedT>(InstantiatedT instantiatedObject, Transform parent, int msDelay = 0, [CallerFilePath, DontProvide] string instantiaterPath = "") where InstantiatedT : UnityObject
        {
            bool raiseInstantiated = true;
            if (instantiatedObject is Transform transform)
            {
                await Instantiate(transform.gameObject, transform, msDelay, instantiaterPath);
            }
            else
            {
                string instantiaterName = instantiaterPath.Remove(instantiaterPath.Length - 3).Split('\\').Last();
                await Task.Delay(msDelay);
                if (instantiatedObject is GameObject InstantiatedGameObject)
                {
                    foreach (Component element in InstantiatedGameObject.GetComponents<Component>())
                    {
                        if (element is Transform transfo)
                        {
                            foreach (Transform item in transfo)
                            {
                                if (item.gameObject != InstantiatedGameObject)
                                {
                                    if (!InstantiatingMethod(item.gameObject, parent, instantiaterName))
                                    {
                                        raiseInstantiated = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (!InstantiatingMethod(element, parent, instantiaterName))
                                raiseInstantiated = false;
                        }
                    }
                    if (raiseInstantiated)
                    {
                        foreach (Component element in InstantiatedGameObject.GetComponents<Component>())
                        {
                            if (element is Transform transfo)
                            {
                                foreach (Transform item in transfo)
                                {
                                    if (item.gameObject != InstantiatedGameObject)
                                    {
                                        InstantiatedMethod(item.gameObject, parent, instantiaterName);
                                    }
                                }
                            }
                            else
                            {
                                InstantiatedMethod(element, parent, instantiaterName);
                            }
                        }
                    }
                }
            }
        }

        private static bool InstantiatingMethod<InstantiatedT>(InstantiatedT InstantiatedObject, Transform parent, string instantiaterName) where InstantiatedT : UnityObject
        {
            InstantiatingObjectEventArgs<InstantiatedT> InstantiatingArgs = new InstantiatingObjectEventArgs<InstantiatedT>(InstantiatedObject, instantiaterName);
            InstantiatingObjectEventArgs<UnityObject> SentInstantiatingArgs = (InstantiatingObjectEventArgs<UnityObject>)InstantiatingArgs;
            _instantiatingObject?.Invoke(SentInstantiatingArgs);
            return !SentInstantiatingArgs.Cancel;
        }

        private static void InstantiatedMethod<InstantiatedT>(InstantiatedT InstantiatedObject, Transform parent, string instantiaterName) where InstantiatedT : UnityObject
        {
            ObjectInstiatedEventArgs<InstantiatedT> InstantiatedArgs = new ObjectInstiatedEventArgs<InstantiatedT>(InstantiatedObject, instantiaterName);
            ObjectInstiatedEventArgs<UnityObject> SentInstantiatedArgs = (ObjectInstiatedEventArgs<UnityObject>)InstantiatedArgs;
            UnityObject.Instantiate(InstantiatedObject, parent);
            _objectInstantiated?.Invoke(SentInstantiatedArgs);
        }
        #endregion

        #region Instantiate(UnityEngine.Object original, Transform parent, bool worldPositionStays)
        /// <summary>
        /// Instantiates the given <see cref="UnityEngine.Object"/>, sets <paramref name="parent"/> as parent and fires related events
        /// </summary>
        /// <param name="instantiatedObject"><see cref="UnityEngine.Object"/> to instantiate</param>
        /// <param name="parent"><see cref="Transform"/> to set as parent of the <paramref name="instantiatedObject"/></param>
        /// <param name="msDelay">The number of milliseconds to wait before instantiating the given <see cref="UnityEngine.Object"/></param>
        /// <param name="worldPositionStays">Determines if the world position has to be kept</param>
        /// <param name="instantiaterPath">This argument is automatically provided, please do not provide it</param>
        public static async Task Instantiate<InstantiatedT>(InstantiatedT instantiatedObject, Transform parent, bool worldPositionStays, int msDelay = 0, [CallerFilePath, DontProvide] string instantiaterPath = "") where InstantiatedT : UnityObject
        {
            bool raiseInstantiated = true;
            if (instantiatedObject is Transform transform)
            {
                await Instantiate(transform.gameObject, transform, worldPositionStays, msDelay, instantiaterPath);
            }
            else
            {
                string instantiaterName = instantiaterPath.Remove(instantiaterPath.Length - 3).Split('\\').Last();
                await Task.Delay(msDelay);
                if (instantiatedObject is GameObject InstantiatedGameObject)
                {
                    foreach (Component element in InstantiatedGameObject.GetComponents<Component>())
                    {
                        if (element is Transform transfo)
                        {
                            foreach (Transform item in transfo)
                            {
                                if (item.gameObject != InstantiatedGameObject)
                                {
                                    if (!InstantiatingMethod(item.gameObject, parent, worldPositionStays, instantiaterName))
                                    {
                                        raiseInstantiated = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (!InstantiatingMethod(element, parent, worldPositionStays, instantiaterName))
                                raiseInstantiated = false;
                        }
                    }
                    if (raiseInstantiated)
                    {
                        foreach (Component element in InstantiatedGameObject.GetComponents<Component>())
                        {
                            if (element is Transform transfo)
                            {
                                foreach (Transform item in transfo)
                                {
                                    if (item.gameObject != InstantiatedGameObject)
                                    {
                                        InstantiatedMethod(item.gameObject, parent, worldPositionStays, instantiaterName);
                                    }
                                }
                            }
                            else
                            {
                                InstantiatedMethod(element, parent, worldPositionStays, instantiaterName);
                            }
                        }
                    }
                }
            }
        }

        private static bool InstantiatingMethod<InstantiatedT>(InstantiatedT InstantiatedObject, Transform parent, bool worldPositionStays, string instantiaterName) where InstantiatedT : UnityObject
        {
            InstantiatingObjectEventArgs<InstantiatedT> InstantiatingArgs = new InstantiatingObjectEventArgs<InstantiatedT>(InstantiatedObject, instantiaterName);
            InstantiatingObjectEventArgs<UnityObject> SentInstantiatingArgs = (InstantiatingObjectEventArgs<UnityObject>)InstantiatingArgs;
            _instantiatingObject?.Invoke(SentInstantiatingArgs);
            return !SentInstantiatingArgs.Cancel;
        }

        private static void InstantiatedMethod<InstantiatedT>(InstantiatedT InstantiatedObject, Transform parent, bool worldPositionStays, string instantiaterName) where InstantiatedT : UnityObject
        {
            ObjectInstiatedEventArgs<InstantiatedT> InstantiatedArgs = new ObjectInstiatedEventArgs<InstantiatedT>(InstantiatedObject, instantiaterName);
            ObjectInstiatedEventArgs<UnityObject> SentInstantiatedArgs = (ObjectInstiatedEventArgs<UnityObject>)InstantiatedArgs;
            UnityObject.Instantiate(InstantiatedObject, parent, worldPositionStays);
            _objectInstantiated?.Invoke(SentInstantiatedArgs);
        }
        #endregion
        
        #endregion

        #region Nested Classes (makes things clearer for users and may save memory)
        /// <summary>
        /// Handlers added here will be invoked when before the <see cref="UnityEngine.Object"/>'s instantiation
        /// </summary>
        public static class Instantiating
        {
            private static List<MethodInfo> AddedMethods = new List<MethodInfo>();
            /// <summary>
            /// Adds a method to execute before an object of type <typeparamref name="InstantiatedType"/> was instantiated by the <see cref="Instantiater"/>
            /// </summary>
            /// <typeparam name="InstantiatedType">Type of the object's instantiation to listen</typeparam>
            /// <param name="method">Event listener to be added</param>
            public static void AddHandler<InstantiatedType>(Action<InstantiatingObjectEventArgs<InstantiatedType>> method) where InstantiatedType : UnityObject
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
        public static class Instantiated
        {
            private static List<MethodInfo> AddedMethods = new List<MethodInfo>();
            /// <summary>
            /// Adds a method to execute after an object of type <typeparamref name="InstantiatedType"/> was instantiated by the <see cref="Instantiater"/>
            /// </summary>
            /// <typeparam name="InstantiatedType">Type of the object's instantiation to listen</typeparam>
            /// <param name="method">Event listener to be added</param>
            public static void AddHandler<InstantiatedType>(Action<ObjectInstiatedEventArgs<InstantiatedType>> method) where InstantiatedType : UnityObject
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