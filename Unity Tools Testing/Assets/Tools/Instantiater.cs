using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class Instantiater<CreatedT> where CreatedT : Object
{
    #region Champs
    private static event ObjectCreatedEventHandler<CreatedT> _objectCreated;
    private static event CreatingObjectEventHandler<CreatedT> _creatingObject;
    #endregion

    #region Propriétés
    public static event ObjectCreatedEventHandler<CreatedT> ObjectCreated
    {
        add
        {
            if (AllowsMultipleEqualHandler || _objectCreated == null || !_objectCreated.GetInvocationList().Any(element => element.Method == value.Method))
            {
                _objectCreated += value;
            }
        }
        remove
        {
            _objectCreated -= value;
        }
    }

    public static event CreatingObjectEventHandler<CreatedT> CreatingObject
    {
        add
        {
            if (AllowsMultipleEqualHandler || _creatingObject == null || !_creatingObject.GetInvocationList().Any(element => element.Method == value.Method))
            {
                _creatingObject += value;
            }
        }
        remove
        {
            _creatingObject -= value;
        }
    }

    public static bool AllowsMultipleEqualHandler { private get; set; } = false;
    #endregion

    #region Methodes
    public static CreatedT Instantiate(CreatedT CreatedObject, [CallerFilePath] string callerPath = "")
    {
        string callerName = callerPath.Remove(callerPath.Length - 3).Split('\\').Last();
        CreatingObjectEventArgs<CreatedT> CreatingArgs = new CreatingObjectEventArgs<CreatedT>(CreatedObject, callerName);
        ObjectCreatedEventArgs<CreatedT> CreatedArgs = new ObjectCreatedEventArgs<CreatedT>(CreatedObject, callerName);
        CreatedT ret = null;
        _creatingObject?.Invoke(CreatingArgs);
        if (!CreatingArgs.Cancel)
        {
            ret = Object.Instantiate(CreatedObject);
            _objectCreated?.Invoke(CreatedArgs);
        }
        return ret;
    }
    #endregion
}

public abstract class ObjectCreationEventArgsBase<CreatedT> : System.EventArgs where CreatedT : Object
{
    public CreatedT CreatedObject { get; private set; }
    public string CreatingFileName { get; private set; }

    internal ObjectCreationEventArgsBase(CreatedT createdObject, string creatorName)
    {
        CreatingFileName = creatorName;
        CreatedObject = createdObject;
    }

}

public sealed class ObjectCreatedEventArgs<CreatedT> : ObjectCreationEventArgsBase<CreatedT> where CreatedT : Object
{
    public ObjectCreatedEventArgs(CreatedT createdObject, string creatorName) : base(createdObject, creatorName)
    {

    }
}

public sealed class CreatingObjectEventArgs<CreatedT> : ObjectCreationEventArgsBase<CreatedT> where CreatedT : Object
{
    public bool Cancel { get; set; } = false;

    public CreatingObjectEventArgs(CreatedT createdObject, string creatorName) : base(createdObject, creatorName)
    {

    }
}

public delegate void ObjectCreatedEventHandler<CreatedT>(ObjectCreatedEventArgs<CreatedT> e) where CreatedT : Object;
public delegate void CreatingObjectEventHandler<CreatedT>(CreatingObjectEventArgs<CreatedT> e) where CreatedT : Object;