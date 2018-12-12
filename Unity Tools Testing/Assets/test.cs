using Nalka.Tools.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using static System.Diagnostics.Debug;

public class Test : MonoBehaviour
{
    public string Temp = "Je teste des components avec destroy immedaite";
    private void Start()
    {
        Instantiater.Instantiating<GameObject>.AddHandler(OtherTestMethod);
        //Destroyer.Destroyed<GameObject>.AddHandler(TestMethod);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Instantiater.Instantiate(gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {

        }
    }
    private void OtherTestMethod(InstiatingObjectEventArgs<GameObject> e)
    {
        Debug.Log($"salut");
    }
    private void TestMethod(ObjectDestroyedEventArgs<GameObject> e)
    {
        Debug.Log($"{e.DestroyedObject.name}");
    }
}