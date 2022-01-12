using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MaterialColourHandler : MonoBehaviour
{
    [SerializeField] Material floorMaterial;
    [Space]
    [SerializeField] TextMeshPro numberMaterial;
    [SerializeField] TextMeshPro number1Material;
    [Space]
    [SerializeField] Material jerseyBotTextMat;
    [SerializeField] Material jerseyTopTextMat;

    [Space]

    [SerializeField] Material edgesMaterial;
    [SerializeField] Material rareBorderMat;
    [SerializeField] Material epicBorderMat;
    [SerializeField] Material legendaryBorderMat;
    [SerializeField] Material genesisBorderMat;
    [SerializeField] Material platinumBorderMat;

    [Space]

    [SerializeField] Color greyFloor;
    [SerializeField] Color blackFloor;

    [Space]

    [SerializeField] [ColorUsage(true, true)] Color legendaryFaceColor;
    [SerializeField] [ColorUsage(true, true)] Color legendaryGlowColor;
    [SerializeField] [ColorUsage(true, true)] Color legendaryBGFaceColor;
    [SerializeField] [ColorUsage(true, true)] Color legendaryBGGlowColor;
    [SerializeField] Color legendaryEdgeColor;

    [Space]

    [SerializeField] [ColorUsage(true, true)] Color genesisFaceColor;
    [SerializeField] [ColorUsage(true, true)] Color genesisGlowColor;
    [SerializeField] [ColorUsage(true, true)] Color genesisBGFaceColor;
    [SerializeField] [ColorUsage(true, true)] Color genesisBGGlowColor;
    [SerializeField] Color genesisEdgeColor;

    [Space]

    [SerializeField] [ColorUsage(true, true)] Color platinumFaceColor;
    [SerializeField] [ColorUsage(true, true)] Color platinumGlowColor;
    [SerializeField] [ColorUsage(true, true)] Color platinumFaceBGColor;
    [SerializeField] [ColorUsage(true, true)] Color platinumGlowBGColor;

    [SerializeField] float defaultBGFontGlowIntensity = 0.2f; // intesnsity current -2

    static MaterialColourHandler instance;
    public Color32 c;
    private void Awake()
    {
        instance = this;
    }

    private Color ChangeFrontGlow(Color inputColor)
    {
        float h, s, v;
        Color.RGBToHSV(inputColor,out h,out s,out v);
        if(v<0.8)
        {
            v = 0.8f;
        }
        
        return Color.HSVToRGB(h, s, v);
    }

    private Color ChangeBGColorGlow(Color inputColor)
    {
        float h, s, v;
        Color.RGBToHSV(inputColor, out h, out s, out v);
        v = v/3;
        return Color.HSVToRGB(h, s, v);
    }

    private static Color DarkShadeColor(Color inputColor, float darkenBy = 0.50f)
    {
        float h, s, v;
        Color.RGBToHSV(inputColor, out h, out s, out v);
        v = v - darkenBy;
        //Debug.LogError(inputColor + "  Color set to :" + h + " ," + s + " ," + v);

        return Color.HSVToRGB(h, s, v);
    }
    public static void SetColor(string borderColor, CubeRarityType rarity, SpriteRenderer surface4Logo, SpriteRenderer surface4ShadowRender, SpriteRenderer surface4LogoGradiant
        , TextMeshPro tournamentName, TextMeshPro tournamentNameSadow)
    {
        if (ColorUtility.TryParseHtmlString(borderColor, out Color color))
        {
            instance.c = color;
            var factor = instance.defaultBGFontGlowIntensity;
            Debug.Log("<color=red>Set colors</color>");
            switch (rarity)
            {
                case CubeRarityType.common:
                    {

                        surface4Logo.color = color;
                        surface4ShadowRender.color = DarkShadeColor(color);
                        surface4LogoGradiant.gameObject.SetActive(true);
                        tournamentName.color = color;
                        tournamentNameSadow.color = DarkShadeColor(color);

                        instance.edgesMaterial.color = color;
                        instance.jerseyBotTextMat.SetColor("_FaceColor", color);
                        instance.jerseyBotTextMat.SetColor("_GlowColor", instance.ChangeBGColorGlow(color));
                        instance.jerseyTopTextMat.SetColor("_FaceColor", color);
                        instance.jerseyTopTextMat.SetColor("_GlowColor", instance.ChangeFrontGlow(color));
                        instance.floorMaterial.color = instance.greyFloor;
                    }
                    break;
                case CubeRarityType.rare:   
                    {
                        surface4Logo.color = color;
                        surface4ShadowRender.color = DarkShadeColor(color);
                        surface4LogoGradiant.gameObject.SetActive(true);
                        tournamentName.color = color;
                        tournamentNameSadow.color = DarkShadeColor(color);

                        instance.edgesMaterial.color = color;
                        instance.rareBorderMat.SetColor("EmissionColor", color);
                        instance.jerseyBotTextMat.SetColor("_FaceColor", color);
                        instance.jerseyBotTextMat.SetColor("_GlowColor", instance.ChangeBGColorGlow(color));
                        instance.jerseyTopTextMat.SetColor("_FaceColor", color);
                        instance.jerseyTopTextMat.SetColor("_GlowColor", instance.ChangeFrontGlow(color));
                        instance.floorMaterial.color = instance.greyFloor;
                    }
                    break;
                case CubeRarityType.epic:
                    {
                        surface4Logo.color = color;
                        surface4ShadowRender.color = DarkShadeColor(color);
                        surface4LogoGradiant.gameObject.SetActive(true);
                        tournamentName.color = color;
                        tournamentNameSadow.color = DarkShadeColor(color);
                        Debug.Log("Set epic colors");
                        instance.edgesMaterial.color = color;
                        instance.epicBorderMat.SetColor("EmissionColor", color);
                        instance.jerseyBotTextMat.SetColor("_FaceColor", color);
                        instance.jerseyBotTextMat.SetColor("_GlowColor", instance.ChangeBGColorGlow(color));
                        instance.jerseyTopTextMat.SetColor("_FaceColor", color);
                        instance.jerseyTopTextMat.SetColor("_GlowColor", instance.ChangeFrontGlow(color));
                        instance.floorMaterial.color = instance.greyFloor;

                    }
                    break;
                case CubeRarityType.legendary:
                    {
                        tournamentName.color = instance.legendaryEdgeColor;

                        surface4ShadowRender.color = DarkShadeColor(Color.white);
                        tournamentNameSadow.color = DarkShadeColor(instance.legendaryEdgeColor);
                        surface4Logo.color = Color.white;
                        instance.edgesMaterial.color = instance.legendaryEdgeColor;
                        instance.jerseyBotTextMat.SetColor("_FaceColor", instance.legendaryBGFaceColor);
                        instance.jerseyBotTextMat.SetColor("_GlowColor", instance.legendaryBGGlowColor);
                        instance.jerseyTopTextMat.SetColor("_FaceColor", instance.legendaryFaceColor);
                        instance.jerseyTopTextMat.SetColor("_GlowColor", instance.legendaryGlowColor);
                        instance.floorMaterial.color = instance.greyFloor;
                    }
                    break;
                case CubeRarityType.genesis:
                    {
                        tournamentName.color = instance.genesisEdgeColor;

                        instance.edgesMaterial.color = instance.genesisEdgeColor;
                        surface4ShadowRender.color = DarkShadeColor(Color.white);
                        tournamentNameSadow.color = DarkShadeColor(instance.genesisEdgeColor);
                        surface4Logo.color = Color.white;

                        instance.edgesMaterial.color = instance.genesisEdgeColor;
                        instance.jerseyBotTextMat.SetColor("_FaceColor", instance.genesisBGFaceColor);
                        instance.jerseyBotTextMat.SetColor("_GlowColor", instance.genesisBGGlowColor);
                        instance.jerseyTopTextMat.SetColor("_FaceColor", instance.genesisFaceColor);
                        instance.jerseyTopTextMat.SetColor("_GlowColor", instance.genesisGlowColor);
                        instance.floorMaterial.color = instance.blackFloor;
                    }
                    break;
                case CubeRarityType.platinum:
                    {
                        Debug.Log("Set plat colors");
                        tournamentName.color = instance.platinumFaceColor;
                        instance.edgesMaterial.color = instance.platinumFaceColor;
                        surface4ShadowRender.color = DarkShadeColor(Color.white);
                        tournamentNameSadow.color = DarkShadeColor(instance.platinumFaceColor);
                        surface4Logo.color = Color.white;

                        instance.edgesMaterial.color = instance.platinumFaceColor;
                        instance.jerseyBotTextMat.SetColor("_FaceColor", instance.platinumFaceBGColor);
                        instance.jerseyBotTextMat.SetColor("_GlowColor", instance.platinumGlowBGColor);
                        instance.jerseyTopTextMat.SetColor("_FaceColor", instance.platinumFaceColor);
                        instance.jerseyTopTextMat.SetColor("_GlowColor", instance.platinumGlowColor);
                        instance.floorMaterial.color = instance.blackFloor;
                    }
                    break;
            }
        }
    }
}
