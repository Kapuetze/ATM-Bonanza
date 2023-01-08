using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MoneyIcon : MonoBehaviour
{
    [SerializeField]
    private TMP_Text amountText;
    [SerializeField]
    private TMP_Text denominationText;

    // Start is called before the first frame update
    public void SetMoneyValues(Denomination denomination, int amount)
    {
        // Get the color from enumeration description
        string hexColor = denomination.GetDescription();
        ColorUtility.TryParseHtmlString(hexColor, out Color color);
        GetComponent<Image>().color = color;

        int denominationValue = (int)denomination;

        // Change the text to the current amount
        amountText.text = amount.ToString();
        denominationText.text = denominationValue.ToString();
    }
}
