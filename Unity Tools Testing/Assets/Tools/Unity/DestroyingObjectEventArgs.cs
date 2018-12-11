using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nalka.Tools.Unity
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="DestroyingT">Type of the destroyed <see cref="Object"/></typeparam>
    public sealed class DestroyingObjectEventArgs<DestroyingT> : DestrcutionEventArgsBase<DestroyingT> where DestroyingT : Object
    {
        private RefNeeded trick;

        /// <summary>
        /// Determines if the <see cref="Object"/>'s destruction has to be cancelled
        /// </summary>
        public bool Cancel
        {
            get { return trick.RefCancel; }
            set { trick.RefCancel = value; }
        }

        private DestroyingObjectEventArgs(DestroyingT destroyedObject, string destroyingFileName, RefNeeded refNeeded) : this(destroyedObject, destroyingFileName)
        {
            trick = refNeeded;
        }

        internal DestroyingObjectEventArgs(DestroyingT destroyedObject, string destroyingFileName) : base(destroyedObject, destroyingFileName)
        {
            trick = new RefNeeded();
        }

        public static explicit operator DestroyingObjectEventArgs<DestroyingT>(DestroyingObjectEventArgs<Object> e) => new DestroyingObjectEventArgs<DestroyingT>((DestroyingT)e.DestroyedObject, e.DestroyingFileName, e.trick);

        public static explicit operator DestroyingObjectEventArgs<Object>(DestroyingObjectEventArgs<DestroyingT> e) => new DestroyingObjectEventArgs<Object>(e.DestroyedObject, e.DestroyingFileName, e.trick);
    }

    public delegate void DestroyingObjectEventHandler(DestroyingObjectEventArgs<Object> e);
}