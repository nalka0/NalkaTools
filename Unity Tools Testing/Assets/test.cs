using Nalka.Tools.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        Destroyer.AddHandler<Object>(Destroyer_ObjectDestroyed);
        Destroyer.AddHandler<Object>(Destroyer_DestroyingObject);
        Destroyer.AddHandler<Object>(Destroyer_DestroyingObject);
        Instantiater<GameObject>.CreatingObject += Instantiater_CreatingObject;
        Instantiater<GameObject>.ObjectCreated += Instantiater_ObjectCreated;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Instantiater<GameObject>.Instantiate(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Destroyer.Destroy(gameObject);
        }
    }

    private void Instantiater_ObjectCreated(ObjectCreatedEventArgs<GameObject> e)
    {
        Debug.Log($"{e.CreatingFileName} created {e.CreatedObject.name}");
    }

    private void Instantiater_CreatingObject(CreatingObjectEventArgs<GameObject> e)
    {
        Debug.Log($"{e.CreatingFileName} creating {e.CreatedObject.name}");
    }

    private void Destroyer_ObjectDestroyed(ObjectDestroyedEventArgs<Object> e)
    {
        Debug.Log($"{e.DestroyingFileName} destroyed {e.DestroyedObject.name}");
    }

    private void Destroyer_DestroyingObject(DestroyingObjectEventArgs<Object> e)
    {
        Debug.Log($"{e.DestroyingFileName} destroying {e.DestroyedObject.name}");
    }
}