﻿using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KimsTorchMeterScript : MonoBehaviour //Dont worry about this script, use the TileMovement Script and its component (in the Unity inspector) to manipulate the torch meter   
{
    private float fillAmount;

    [SerializeField]
    private float lerpSpeed;

    [SerializeField]
    private Image content;

    [SerializeField]
    private Image flameIcon;

    [SerializeField]
    private Text valueText;

    [SerializeField]
    private Color fullColor;

    [SerializeField]
    private Color lowColor;

    [SerializeField]
    private Color fullFlameColor;

    [SerializeField]
    private Color lowFlameColor;

    private AudioSource audioSource;

    public float MaxValue { get; set; }

    public float Value
    {
        set
        {
            //take everything behind the colon and place it as the tmp value...
            //string[] tmp = valueText.text.Split(':');   
            //valueText.text = tmp[0] + ": " + value;

            valueText.text = value.ToString();
            fillAmount = Map(value, 0, MaxValue, 0, 1);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();   
    }

    // Update is called once per frame
    void Update()
    {
        RadialBar();
    }

    private void RadialBar()
    {
        if(fillAmount != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);

            content.color = Color.Lerp(lowColor, fullColor, fillAmount);

            flameIcon.color = Color.Lerp(lowFlameColor, fullFlameColor, fillAmount);

            audioSource.volume = Mathf.Lerp(0f, 0.8f, fillAmount);
        }
    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }

}
