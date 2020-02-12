using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicGrass : MonoBehaviour
{

    public float grassDensity = 5f;


    public float grassAreaWidth = 8f;
    public float grassAreaLength = 6f;



    // 合批
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;


    //大面积草的材质球
    public Material grassMat;

    // 多人
    //public Transform[] obstacles;
    public List<Transform> obstacles = new List<Transform>();
    private Vector4[] obstaclePositions = new Vector4[100];


    public GameObject  pre_grass;
    // Start is called before the first frame update
    void Start()
    {

        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();

        meshRenderer.material = grassMat;

        //GameObject instance = Instantiate(pre_grass) as GameObject;
        InitializeGrass();
    }

    // Update is called once per frame
    void Update()
    {
        //obstacles.Count
        for(int n = 0;n < obstacles.Count; n++)
        {
            obstaclePositions[n] = obstacles[n].position;
        }

        //Debug.Log("人物数量。。。" + obstacles.Count);

        Shader.SetGlobalFloat("_PositionArray", obstacles.Count);
        Shader.SetGlobalVectorArray("_ObstaclePositions", obstaclePositions);
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


        ///
        


       for(int w =0;w < tempGrassNumW; w++)
        {
            for (int l = 0; l < tempGrassNumL; l++)
            {

                //位置
                tempGrassPosList[w + l * tempGrassNumW] = new Vector3(
                    tempPosStartX + w * tempPosInterval,   // x 
                    transform.position.y,                  // y
                    tempPosStartZ + l * tempPosInterval    // z
                    );
                //
                temgGrassObjList[w + l * tempGrassNumW] = Instantiate(pre_grass);

                temgGrassObjList[w + l * tempGrassNumW].transform.position = tempGrassPosList[w + l * tempGrassNumW];

                MeshFilter mf = temgGrassObjList[w + l * tempGrassNumW].GetComponent<MeshFilter>();

                combine[w + l * tempGrassNumW].mesh = mf.sharedMesh;
                combine[w + l * tempGrassNumW].transform = temgGrassObjList[w + l * tempGrassNumW].transform.localToWorldMatrix;


                temgGrassObjList[w + l * tempGrassNumW].SetActive(false);

            }
        }

        meshFilter.mesh = new Mesh();
        meshFilter.mesh.CombineMeshes(combine);
        //transform.gameObject.active = true;

        for(int n = 0;n< tempGrassPosList.Length; n++)
        {
            Destroy(temgGrassObjList[n]);
        }
       

    }
}
