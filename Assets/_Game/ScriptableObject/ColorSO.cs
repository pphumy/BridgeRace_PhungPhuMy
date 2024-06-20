using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorSO", menuName = "ScriptableObjects/ColorDataSO", order = 1)]
public class ColorSO : ScriptableObject
{
    [SerializeField] Material[] mats;
    public Material GetMat(ColorType type)
    {
        return mats[(int)type];
    }

}
