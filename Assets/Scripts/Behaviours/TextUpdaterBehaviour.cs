using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdaterBehaviour : MonoBehaviour
{
    private Text textDisplay;
    public string prefix;

    private void Start()
    {
        try
        {
            this.textDisplay = GetComponent<Text>();
        }
        catch
        {
            Debug.LogError("No 'Text' Component attached.");
        }
    }

    public void UpdateText(float input)
    {
        this.textDisplay.text = prefix + input;
    }
}
