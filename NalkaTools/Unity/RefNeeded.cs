using System;

namespace NalkaTools.Unity
{
    [Obsolete("Unity now supports ref properties", false)]
    internal class RefNeeded
    {
        internal bool RefCancel { get; set; }
    }
}
