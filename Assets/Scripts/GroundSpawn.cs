using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawn : MonoBehaviour
{
    public GameObject Tile;
    Vector3 Spawn;

    public void SpawnTile()
    {
        GameObject temp = Instantiate(Tile, Spawn, Quaternion.identity);
        Spawn = temp.transform.GetChild(1).transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 17; i++)
        {
            SpawnTile();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
