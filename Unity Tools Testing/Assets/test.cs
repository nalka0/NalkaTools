using Nalka.Tools.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private void Start()
    {
        EncapsulerDestruction<GameObject>.ObjectDestroyed += EncapsulerDestruction_ObjectDestroyed;
        EncapsulerDestruction<GameObject>.DestroyingObject += EncapsulerDestruction_ObjectDestroying;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            EncapsulerDestruction<GameObject>.DestroyObject(gameObject);
        }
    }

    private void EncapsulerDestruction_ObjectDestroyed(ObjectDestroyedEventArgs<GameObject> e)
    {
        Debug.Log($"Destroyed {e.DestroyedObject.name}");
    }

    private void EncapsulerDestruction_ObjectDestroying(DestroyingObjectEventArgs<GameObject> e)
    {
        Debug.Log($"Destroyed {e.DestroyedObject.name}");
    }
}