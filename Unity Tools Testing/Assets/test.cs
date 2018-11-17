using Nalka.Tools.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        Destroyer<GameObject>.ObjectDestroyed += EncapsulerDestruction_ObjectDestroyed;
        Destroyer<GameObject>.DestroyingObject += EncapsulerDestruction_DestroyingObject;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Destroyer<GameObject>.Destroy(gameObject);
        }
    }

    private void EncapsulerDestruction_ObjectDestroyed(ObjectDestroyedEventArgs<GameObject> e)
    {
        Debug.Log($"{e.DestroyingFileName} destroyed {e.DestroyedObject.name}");
    }

    private void EncapsulerDestruction_DestroyingObject(DestroyingObjectEventArgs<GameObject> e)
    {
        Debug.Log($"{e.DestroyingFileName} destroying {e.DestroyedObject.name}");
    }
}