using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassFlow : MonoBehaviour
{

    Vector3 playerPos;

    private DynamicGrass dynamicGrass;
    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("grassMgr");
        //GameObject.FindGameObjectWithTag
        dynamicGrass = go.GetComponent<DynamicGrass>();

        //int leng = dynamicGrass.obstacles.Length;
        //dynamicGrass.obstacles[leng + 1] = transform;
        if (dynamicGrass.obstacles.Contains(transform) == false)
        {
            dynamicGrass.obstacles.Add(transform);
        }

    }

    // Update is called once per frame
    void Update()
    {
        playerPos = transform.position;
        //dynamicGrass.obstacles[]
        
        Shader.SetGlobalVector("_PlayerPos", playerPos);

    }
}
