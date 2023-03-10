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
    [Description("#c55e1b")]
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
    public float GROW_INTERVAL = 10f;
    public float timeUntilGrow = 0f;
    public bool isGrowing = true;

    private Rigidbody2D rb;
    private CapsuleCollider2D coll;
    private System.Array enumArray = Enum.GetValues(typeof(Denomination));

    private AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ApplyDenomination();

        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false;
        coll = GetComponent<CapsuleCollider2D>();
        coll.enabled = false;
    }

    public void Update()
    {
        if(isGrowing)
        {
            timeUntilGrow += Time.deltaTime;
            if (timeUntilGrow >= GROW_INTERVAL)
            {
                timeUntilGrow = 0f;
                Grow();
            }
        }
    }

    private void Grow()
    {
        int currIndex = Array.IndexOf(enumArray, denomination);
        denomination = (Denomination) enumArray.GetValue(currIndex + 1);
        ApplyDenomination();

        if(currIndex + 1 == enumArray.Length - 1)
        {
            isGrowing = false;
        }
    }

    public void ApplyDenomination()
    {
        // Get the color from enumeration description
        string hexColor = denomination.GetDescription();
        ColorUtility.TryParseHtmlString(hexColor, out Color color);
        GetComponent<SpriteRenderer>().color = color;

        //audio.Play();
    }

    public void StopGrowing()
    {
        coll.enabled = true;
        rb.simulated = true;
        isGrowing = false;
    }

    public void IncreaseTimer(float value)
    {
        timeUntilGrow += value;
    }
}
