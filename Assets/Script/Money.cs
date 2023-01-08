using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum Denomination 
{
    [Description("#767F8B")]
    Five = 5, 
    [Description("#FF4A37")]
    Ten = 10,
    [Description("#3643FF")]
    Twenty = 20,
    [Description("#56FF74")]
    Fifty = 50,
    /*[Description("#37e302")]
    Onehundred = 100,
    [Description("#e1eb34")]
    Twohundred = 200,
    [Description("#eb34d5")]
    Fivehundred = 500*/
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
    public int GROW_INTERVAL = 10;

    private Rigidbody2D rigidbody;
    private CapsuleCollider2D collider;
    private System.Array enumArray = Enum.GetValues(typeof(Denomination));

    // Start is called before the first frame update
    void Start()
    {
        ApplyDenomination();

        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.simulated = false;
        collider = GetComponent<CapsuleCollider2D>();
        collider.enabled = false;

        InvokeRepeating("Grow", GROW_INTERVAL, GROW_INTERVAL);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Grow()
    {
        int currIndex = Array.IndexOf(enumArray, denomination);
        denomination = (Denomination) enumArray.GetValue(currIndex + 1);
        ApplyDenomination();

        if(currIndex + 1 == enumArray.Length - 1)
        {
            CancelInvoke();
        }
    }

    private void ApplyDenomination()
    {
        // Get the color from enumeration description
        string hexColor = denomination.GetDescription();
        ColorUtility.TryParseHtmlString(hexColor, out Color color);
        GetComponent<SpriteRenderer>().color = color;
    }

    public void StopGrowing()
    {
        collider.enabled = true;
        rigidbody.simulated = true;
        CancelInvoke();
    }
}
