using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nalka.Tools.Unity;

public class Destroy : MonoBehaviour
{
    private void Start()
    {
        Destroyer.Destroyed.AddHandler<Object>(e => Debug.Log($"{e.DestroyedObject.name} destroyed from {e.DestroyingFileName}"));
        Destroyer.Destroying.AddHandler<Object>(e => Debug.Log($"Destroying {e.DestroyedObject.name} from {e.DestroyingFileName}"));
        Instantiater.Instantiated.AddHandler<Object>(e => Debug.Log($"{e.InstantiatedObject.name} created from {e.InstantiatingFileName}"));
        Instantiater.Instantiating.AddHandler<Object>(e => Debug.Log($"Creating {e.InstantiatedObject.name} from {e.InstantiatingFileName}"));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Destroyer.Destroy(transform);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Instantiater.Instantiate(transform);
        }
    }
}
