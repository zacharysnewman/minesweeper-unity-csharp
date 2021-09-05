using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombSliderUpdaterBehaviour : MonoBehaviour
{
    private Slider slider;
    public Text maxValueText;

    void Start()
    {
        this.slider = GetComponent<Slider>();
    }

    private int width = 10;
    private int height = 10;
    public void UpdateWidth(float width)
    {
        this.width = (int)width;
        UpdateBombSlider();
    }
    public void UpdateHeight(float height)
    {
        this.height = (int)height;
        UpdateBombSlider();
    }

    public void UpdateBombSlider()
    {
        var maxBombs = this.width * this.height - 1;
        this.slider.maxValue = maxBombs;
        this.maxValueText.text = maxBombs.ToString();
        this.slider.value = this.slider.value <= this.slider.maxValue ? this.slider.value : this.slider.maxValue;
    }
}
