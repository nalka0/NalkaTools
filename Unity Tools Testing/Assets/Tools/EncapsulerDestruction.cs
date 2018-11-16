using UnityEngine;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Nalka.Tools.Unity
{
    public static class EncapsulerDestruction<DestroyedT> where DestroyedT : Object
    {
        #region Fields
        private static event ObjectDestroyingEventHandler<DestroyedT> _destroyingObject;
        private static event ObjectDestroyedEventHandler<DestroyedT> _objectDestroyed;
        #endregion

        #region Properties
        /// <summary>
        /// Happens before an object is being destroyed, if <see cref="AllowsMultipleEqualHandler"/> is false a same method will be invoked once only
        /// </summary>
        public static event ObjectDestroyingEventHandler<DestroyedT> DestroyingObject
        {
            add
            {
                if (AllowsMultipleEqualHandler || _destroyingObject == null || !_destroyingObject.GetInvocationList().Any(element => element.Method == value.Method))
                {
                    _destroyingObject += value;
                }
            }
            remove
            {
                _destroyingObject -= value;
            }
        }

        /// <summary>
        /// Happens when an object is destroyed, if <see cref="AllowsMultipleEqualHandler"/> is false a same method will be invoked once only
        /// </summary>
        public static event ObjectDestroyedEventHandler<DestroyedT> ObjectDestroyed
        {
            add
            {
                if (AllowsMultipleEqualHandler || _objectDestroyed == null || !_objectDestroyed.GetInvocationList().Any(element => element.Method == value.Method))
                {
                    _objectDestroyed += value;
                }
            }
            remove
            {
                _objectDestroyed -= value;
            }
        }

        /// <summary>
        /// Determines if <see cref="ObjectDestroyed"/> and <see cref="DestroyingObject"/>
        /// <para/>can contain the same method in their invoation list
        /// </summary>
        public static bool AllowsMultipleEqualHandler { private get; set; } = false;
        #endregion

        #region Methods
        /// <summary>
        /// Destroys the given <see cref="Object"/> and fires related events
        /// </summary>
        /// <param name="DestroyedObject"><see cref="Object"/> to destroy</param>
        /// <param name="destroyerPath">This argument is automatically provided, please do not provide it</param>
        public static void DestroyObject(DestroyedT DestroyedObject, [CallerFilePath] string destroyerPath = "")
        {
            string destroyerName = destroyerPath.Remove(destroyerPath.Length - 3).Split('\\').Last();
            DestroyingObjectEventArgs <DestroyedT> DestroyingArgs = new DestroyingObjectEventArgs<DestroyedT>(DestroyedObject, destroyerName);
            ObjectDestroyedEventArgs<DestroyedT> DestroyedArgs = new ObjectDestroyedEventArgs<DestroyedT>(DestroyedObject, destroyerName);
            _destroyingObject?.Invoke(DestroyingArgs);
            if (!DestroyingArgs.Cancel)
            {
                Object.Destroy(DestroyedObject);
                _objectDestroyed?.Invoke(DestroyedArgs);
            }
        }
        #endregion
    }
    /// <summary>
    /// Provides event data for all <see cref="Object"/> destructions
    /// </summary>
    /// <typeparam name="DestroyedT">Type of the <see cref="Object"/> that has been destroyed</typeparam>
    public abstract class ObjectDestrcutionEventArgsBase<DestroyedT> : System.EventArgs where DestroyedT : Object
    {
        //Tout ce qui est commun à Destroyed et Destroying
        /// <summary>
        /// The <see cref="Object"/> being destroyed
        /// </summary>
        public DestroyedT DestroyedObject;
        /// <summary>
        /// Name of the file that destroyed the <see cref="Object"/>
        /// </summary>
        public string DestroyingFileName;
        
        protected internal ObjectDestrcutionEventArgsBase(DestroyedT Obj, string destroyingFileName)
        {
            DestroyedObject = Obj;
        }
    }

    /// <summary>
    /// Provides event data for <see cref="EncapsulerDestruction{DestroyedT}.DestroyingObject"/>
    /// </summary>
    /// <typeparam name="DestroyedT">Type of the destroyed <see cref="Object"/></typeparam>
    public class ObjectDestroyedEventArgs<DestroyedT> : ObjectDestrcutionEventArgsBase<DestroyedT> where DestroyedT : Object
    {
        //Tout ce qui est spécifique à Destroyed
        internal ObjectDestroyedEventArgs(DestroyedT destroyedObject, string destroyingFileName) : base(destroyedObject, destroyingFileName)
        {

        }
    }

    /// <summary>
    /// Provides event data for <see cref="EncapsulerDestruction{DestroyedT}.DestroyingObject"/>
    /// </summary>
    /// <typeparam name="DestroyingT">Type of the destroyed <see cref="Object"/></typeparam>
    public class DestroyingObjectEventArgs<DestroyingT> : ObjectDestrcutionEventArgsBase<DestroyingT> where DestroyingT : Object
    {
        //Tout ce qui est spécifique à Destroying se trouve ici

        /// <summary>
        /// Allows to cancel the <see cref="Object"/>'s destruction
        /// </summary>
        public bool Cancel = false;

        internal DestroyingObjectEventArgs(DestroyingT destroyedObject, string destroyingFileName) : base(destroyedObject, destroyingFileName)
        {

        }
    }

    public delegate void ObjectDestroyedEventHandler<DestroyedT>(ObjectDestroyedEventArgs<DestroyedT> e) where DestroyedT : Object;
    public delegate void ObjectDestroyingEventHandler<DestroyedT>(DestroyingObjectEventArgs<DestroyedT> e) where DestroyedT : Object;
}