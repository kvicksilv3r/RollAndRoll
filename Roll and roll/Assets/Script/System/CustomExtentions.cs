using System;
using System.Collections.Generic;
using UnityEngine;

public static class CustomExtentions
{
    public static bool IsEmpty<T>(this List<T> listToCheck)
    {
        return listToCheck.Count <= 0;
    }
}