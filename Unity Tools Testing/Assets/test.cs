using Nalka.Tools.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Debug;

public class Test : MonoBehaviour
{
    private void Start()
    {
        //Destroyer.AddHandler<GameObject>(Destroyer_ObjectDestroyed);
        Destroyer.AddHandler<Behaviour>(Destroyer_DestroyingObject);
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
            Destroyer.Destroy<Component>(this);
        }
    }

    private void Instantiater_ObjectCreated(ObjectCreatedEventArgs<GameObject> e)
    {
        Log($"{e.CreatingFileName} created {e.CreatedObject.name}");
    }

    private void Instantiater_CreatingObject(CreatingObjectEventArgs<GameObject> e)
    {
        Log($"{e.CreatingFileName} creating {e.CreatedObject.name}");
    }

    private void Destroyer_ObjectDestroyed(ObjectDestroyedEventArgs<GameObject> e)
    {
        Log($"{e.DestroyingFileName} destroyed {e.DestroyedObject.name}");
    }

    private void Destroyer_DestroyingObject(DestroyingObjectEventArgs<Behaviour> e)
    {
        Log($"{e.DestroyingFileName} destroying {e.DestroyedObject.name}");
    }
}