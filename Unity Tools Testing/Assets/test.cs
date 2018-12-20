using Nalka.Tools.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class Test : MonoBehaviour
{
    [HideInInspector]
    public string Temp = "Je teste des components";
    private void Start()
    {
        Destroyer.AllowsMultipleEqualHandler = false;
        Destroyer.Destroyed.AddHandler<Object>(e =>
        {
            Debug.Log($"Destroyed {e.DestroyedObject.GetType().Name} component from {e.DestroyedObject.name}");
        });
        Destroyer.Destroying.AddHandler<Object>(e =>
        {
            Debug.Log($"Destroying {e.DestroyedObject.GetType().Name} component from {e.DestroyedObject.name}");
            if (e.DestroyedObject.name == "Child cube")
            {
                Debug.LogWarning($"Cancelled");
                e.Cancel = true;
            }
        });
    }

    private async void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
#pragma warning disable CS0618 // Le type ou le membre est obsolète
            await Destroyer.Destroy(gameObject);
#pragma warning restore CS0618 // Le type ou le membre est obsolète
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            //Instantiater.Test();
        }
    }
}