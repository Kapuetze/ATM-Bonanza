using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum Denomination 
{
    [Description("#56FF74")]
    Five = 5, 
    [Description("#FF4A37")]
    Ten = 10,
    [Description("#3643FF")]
    Twenty = 20,
    [Description("#56FF74")]
    Fifty = 50,
    Onehundred = 100, 
    Twohundred = 200, 
    Fivehundred = 500
}

public static class EnumHelper
{
    public static string GetDescription<T>(this T enumValue)
        where T : struct, IConvertible
    {
        if (!typeof(T).IsEnum)
            return null;

        var description = enumValue.ToString();
        var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

        if (fieldInfo != null)
        {
            var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
            if (attrs != null && attrs.Length > 0)
            {
                description = ((DescriptionAttribute)attrs[0]).Description;
            }
        }

        return description;
    }
}

public class Money : MonoBehaviour
{
    public Denomination denomination = Denomination.Five;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
