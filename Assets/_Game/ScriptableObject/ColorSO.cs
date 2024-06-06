using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorSO", menuName = "ScriptableObjects/ColorDataSO", order = 1)]
public class ColorSO : ScriptableObject
{
    [SerializeField] Material[] mats;
    public List<ColorType> selectedColors = new List<ColorType>();

    public Material GetMat(ColorType type)
    {
        return mats[(int)type];
    }

    public List<ColorType> GetListColor()
    {
        List<ColorType> colors = ((ColorType[])Enum.GetValues(typeof(ColorType))).ToList();
        selectedColors.Clear();
        // Remove the None element
        colors.Remove(ColorType.None);
        List<int> indices = new List<int>();
        for (int i = 0; i < colors.Count; i++)
        {
            indices.Add(i);
        }

        // Randomly select 4 indices
        for (int i = 0; i < 4; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, indices.Count);
            selectedColors.Add(colors[indices[randomIndex]]);
            indices.RemoveAt(randomIndex);
        }
        return selectedColors;
    }
}
