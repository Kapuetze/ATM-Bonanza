using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class RainbowText : MonoBehaviour
{
    public float speed = 0.1f;
    public float colorValue = 0f;

    private TextMeshProUGUI tmp;
    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        colorValue += Time.deltaTime * speed;

        if (colorValue > 1) colorValue = 0;
        Color newColor = Color.HSVToRGB(colorValue, 1, 1);
        tmp.color = newColor;
    }
}
