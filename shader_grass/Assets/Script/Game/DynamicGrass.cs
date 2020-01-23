using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicGrass : MonoBehaviour
{

    public float grassDensity = 5f;


    public float grassAreaWidth = 8f;
    public float grassAreaLength = 6f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void InitializeGrass()
    {
        int tempGrassNumW = Mathf.FloorToInt(grassAreaWidth * grassDensity);
        int tempGrassNumL = Mathf.FloorToInt(grassAreaLength * grassDensity);

        //
        float tempPosInterval = 1f / grassDensity;
        float tempPosStartX = grassAreaWidth * -0.5f;
        float tempPosStartZ = grassAreaLength * -0.5f;


        //
        Vector3[] tempGrassPosList = new Vector3[tempGrassNumW * tempGrassNumL];
        GameObject[] temgGrassObjList = new GameObject[tempGrassNumW * tempGrassNumL];

        CombineInstance[] combine = new CombineInstance[tempGrassNumW * tempGrassNumL];


       for(int w =0;w < tempGrassNumW; w++)
        {
            for (int l = 0; l < tempGrassNumL; l++)
            {

                //位置
                tempGrassPosList[w + l * tempGrassNumW] = new Vector3(
                    tempPosStartX + w*tempPosInterval,
                    transform.position.y,
                    tempPosStartZ + l * tempPosInterval
                    ) + new Vector3(
                        0,
                        0,
                        0
                        );
                //
                //tempGrassPosList[w + l*tempGrassNumW] = 
            }
        }

    }
}
