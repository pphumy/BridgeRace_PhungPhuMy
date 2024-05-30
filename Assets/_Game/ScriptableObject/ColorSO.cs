using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSO : MonoBehaviour
{
    [CreateAssetMenu(fileName = "ColorDataSO", menuName = "ScriptableObjects/ColorDataSO", order = 1)]
    public class ColorDataSO : ScriptableObject
    {
        [SerializeField] Material[] mats;

        public Material GetMat(ColorType type)
        {
            return mats[(int)type];
        }
    }
}
