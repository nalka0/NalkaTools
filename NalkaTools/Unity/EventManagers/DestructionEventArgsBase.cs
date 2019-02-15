namespace NalkaTools.Unity.EventManagers
{
    using UnityEngine;

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
        
        internal DestrcutionEventArgsBase(DestroyedT Obj, string destroyingFileName)
        {
            DestroyingFileName = destroyingFileName;
            DestroyedObject = Obj;
        }
    }
}