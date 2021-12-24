using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField] GameObject epicBall;
    [SerializeField] GameObject genesisBall;
    [SerializeField] GameObject legendaryBall;
    [SerializeField] GameObject platinumBall;
    [SerializeField] GameObject rareBall;
    [SerializeField] GameObject commonBall;
    [SerializeField] GameObject parent;

    static BallManager instance;

    private void Start()
    {
        instance = this;
    }

    public static void SetBall(CubeRarityType cubeRarityType)
    {
        //instance.epicBall.SetActive(cubeRarityType == CubeRarityType.epic);
        //instance.genesisBall.SetActive(cubeRarityType == CubeRarityType.genesis);
        //instance.legendaryBall.SetActive(cubeRarityType == CubeRarityType.legendary);
        //instance.platinumBall.SetActive(cubeRarityType == CubeRarityType.platinum);
        //instance.rareBall.SetActive(cubeRarityType == CubeRarityType.rare);
        //instance.commonBall.SetActive(cubeRarityType == CubeRarityType.common);

        GameObject ball;
        switch (cubeRarityType)
        {
            case CubeRarityType.epic:
                 ball=Instantiate(instance.epicBall, instance.parent.transform);
                ball.name= "EpicBall";
                break;
            case CubeRarityType.genesis:
                ball = Instantiate(instance.genesisBall, instance.parent.transform);
                ball.name = "GenesisBall";

                break;
            case CubeRarityType.legendary:
                ball = Instantiate(instance.legendaryBall, instance.parent.transform);
                ball.name = "LegendaryBall";

                break;
            case CubeRarityType.platinum:
                ball = Instantiate(instance.platinumBall, instance.parent.transform);
                ball.name = "PlatinumBall";

                break;
            case CubeRarityType.rare:
                ball = Instantiate(instance.rareBall, instance.parent.transform);
                ball.name = "RareBall";

                break;
            case CubeRarityType.common:
                ball = Instantiate(instance.commonBall, instance.parent.transform);
                ball.name = "CommonBall";

                break;
        }
    }
}
