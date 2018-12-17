using Nalka.Tools.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class Test : MonoBehaviour
{
    [Obsolete] public string Temp = "Je teste des components avec destroy immedaite";
    private void Start()
    {
        Destroyer.AllowsMultipleEqualHandler = false;
        Destroyer.Destroyed.AddHandler<GameObject>(OtherTestMethod);
        Destroyer.Destroyed.AddHandler<GameObject>(OtherTestMethod);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Destroyer.Destroy(gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            Instantiater.Test();
        }
    }
    private void OtherTestMethod(ObjectDestroyedEventArgs<GameObject> e) => Debug.Log($"{e.DestroyedObject.name}");
}