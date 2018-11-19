using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nalka.Tools.Extensions;
using System.Linq;

public class ReflectionTest : MonoBehaviour
{
    private void Start()
    {

    }

    private void Update()
    {
        new REGIS().MemberwiseEquals(new REGIS());
    }
}

public class REGIS
{
    public int TestInt = 7;

    public void TestMethod()
    {

    }

    static REGIS()
    {
    }

    public REGIS()
    {
    }
}