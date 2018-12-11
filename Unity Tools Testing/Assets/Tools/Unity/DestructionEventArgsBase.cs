using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nalka.Tools.Unity
{
    /// <summary>
    /// Provides event data for all <see cref="Object"/> destructions
    /// </summary>
    /// <typeparam name="DestroyedT">Type of the <see cref="Object"/> that has been destroyed</typeparam>
    public abstract class DestrcutionEventArgsBase<DestroyedT> : System.EventArgs where DestroyedT : Object
    {
        /// <summary>
        /// The <see cref="Object"/> being destroyed
        /// </summary>
        public DestroyedT DestroyedObject { get; private set; }
        /// <summary>
        /// Name of the file that destroyed the <see cref="Object"/>
        /// </summary>
        public string DestroyingFileName { get; private set; }

        protected internal DestrcutionEventArgsBase(DestroyedT Obj, string destroyingFileName)
        {
            DestroyingFileName = destroyingFileName;
            DestroyedObject = Obj;
        }
    }
}