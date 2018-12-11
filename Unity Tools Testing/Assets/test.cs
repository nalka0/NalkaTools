using Nalka.Tools.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Instantiater.Instantiate(gameObject);
        }
    }
}