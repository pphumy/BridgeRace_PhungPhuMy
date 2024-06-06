using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : GameUnit
{
    [SerializeField] private ColorSO colors;
    [SerializeField] private Renderer rd;

    public ColorType colorType;

    public void ChangeColor(ColorType colorType)
    {
        rd.enabled = true;
        this.colorType = colorType;
        rd.material = colors.GetMat(colorType);
    }
}
