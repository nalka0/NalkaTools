using Nalka.Tools.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Threading.Tasks;
using static System.Diagnostics.Debug;

public class Test : MonoBehaviour
{
    public string Temp = "Je teste des components avec destroy immedaite";
    private void Start()
    {
        Destroyer.Destroyed.AddHandler<GameObject>(e => { Debug.Log($"salut"); Task.Delay(5000).GetAwaiter().GetResult(); Debug.Log($"{e.DestroyedObject.GetComponent<Test>().Temp}"); });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //exception
            Destroyer.Destroy(gameObject);
        }
    }
}