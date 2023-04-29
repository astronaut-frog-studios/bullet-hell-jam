using System;
using System.Collections.Generic;
using System.Linq;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public static class Helpers
{
    public static T FindInArrayByName<T>(T[] array, string name) where T : Object
    {
        return Array.Find(array, gameObject => gameObject.name == name);
    }
}