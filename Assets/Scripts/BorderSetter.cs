using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderSetter : MonoBehaviour
{
    [SerializeField] Material borderMaterial;
    [SerializeField] GameObject epicBorder;
    [SerializeField] GameObject genesisBorder;
    [SerializeField] GameObject legendaryBorder;
    [SerializeField] GameObject platinumBorder;
    [SerializeField] GameObject rareBorder;
    [SerializeField] GameObject commonBorder;
    [SerializeField] GameObject parent;

    static BorderSetter instance;

    private void Awake()
    {
        instance = this;
    }

    public static void SetBorder(CubeRarityType cubeRarityType, string borderColor)
    {
        //instance.epicBorder.SetActive(cubeRarityType == CubeRarityType.epic);
        //instance.genesisBorder.SetActive(cubeRarityType == CubeRarityType.genesis);
        //instance.legendaryBorder.SetActive(cubeRarityType == CubeRarityType.legendary);
        //instance.platinumBorder.SetActive(cubeRarityType == CubeRarityType.platinum);
        //instance.rareBorder.SetActive(cubeRarityType == CubeRarityType.rare);
        //instance.commonBorder.SetActive(cubeRarityType == CubeRarityType.common);

        switch (cubeRarityType)
        {
            case CubeRarityType.epic:
                Instantiate(instance.epicBorder, instance.parent.transform);
                break;
            case CubeRarityType.genesis:
                Instantiate(instance.genesisBorder, instance.parent.transform);

                break;
            case CubeRarityType.legendary:
                Instantiate(instance.legendaryBorder, instance.parent.transform);

                break;
            case CubeRarityType.platinum:
                Instantiate(instance.platinumBorder, instance.parent.transform);

                break;
            case CubeRarityType.rare:
                Instantiate(instance.rareBorder, instance.parent.transform);

                break;
            case CubeRarityType.common:
                Instantiate(instance.commonBorder, instance.parent.transform);

                break;
        }

        if (ColorUtility.TryParseHtmlString(borderColor, out Color color))
        {
            instance.borderMaterial.color = color;
        }
    }
}
