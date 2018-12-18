using Nalka.Tools.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class Test : MonoBehaviour
{
    public string Temp = "Je teste des components";
    private void Start()
    {
        Destroyer.AllowsMultipleEqualHandler = false;
        Destroyer.Destroying.AddHandler<Test>(Tester);
        Destroyer.Destroying.AddHandler<GameObject>(GameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Destroyer.Destroy(gameObject).GetAwaiter().GetResult();
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            //Instantiater.Test();
        }
    }
    private void Tester(DestroyingObjectEventArgs<Test> e) => Debug.Log($"Test");
    private void GameObject(DestroyingObjectEventArgs<GameObject> e) => Debug.Log($"GameObject");
}