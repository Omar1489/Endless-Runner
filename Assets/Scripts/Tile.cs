using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    GroundSpawn groundSpawn;
    public GameObject IronBall;
    public GameObject Bomb;
    public GameObject Coin;
    public GameObject BlueBall;
    // Start is called before the first frame update
    void Start()
    {
        groundSpawn = GameObject.FindObjectOfType<GroundSpawn>();
        SpawnObstacles();
        SpawnCoin();
        SpawnBlueBall();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            groundSpawn.SpawnTile();
            Destroy(gameObject, 2);
        }
    }


    void SpawnObstacles()
    {
        int ironBallIndex = 0;
        int bombIndex = 0;
        int thirdObstIndex = Random.Range(2, 5);
        while(ironBallIndex == bombIndex)
        {
            ironBallIndex = Random.Range(2, 5);
            bombIndex = Random.Range(2,5);
        }
        Transform ironBallSpawnPoint = transform.GetChild(ironBallIndex).transform;
        Instantiate(IronBall, ironBallSpawnPoint.position, Quaternion.identity, transform);
        Transform bombSpawnPoint = transform.GetChild(bombIndex).transform;
        Instantiate(Bomb, bombSpawnPoint.position, Quaternion.identity, transform);
        while ((thirdObstIndex == ironBallIndex) || (thirdObstIndex == bombIndex))
        {
            thirdObstIndex = Random.Range(2,5);
        }
        int thirdObst = Random.Range(0,4);
        if (thirdObst == 1)
        {
            Transform ironBallSpawnPoint2 = transform.GetChild(thirdObstIndex).transform;
            Instantiate(IronBall, ironBallSpawnPoint2.position, Quaternion.identity, transform);
        }
        else if (thirdObst == 2)
        {
            Transform bombSpawnPoint2 = transform.GetChild(thirdObstIndex).transform;
            Instantiate(Bomb, bombSpawnPoint2.position, Quaternion.identity, transform);
        }

    }

    void SpawnCoin()
    {
        int coinsToSpawn = 2;
        for (int i = 0; i< coinsToSpawn;i++)
        {
            GameObject tmp = Instantiate(Coin,transform);
            tmp.transform.position = GetRandomColliderPoint(GetComponent<Collider>());
        }
    }

    void SpawnBlueBall()
    {
        int ballsToSpawn = 1;
        for (int i = 0; i < ballsToSpawn; i++)
        {
            GameObject tmp = Instantiate(BlueBall, transform);
            tmp.transform.position = GetRandomColliderPoint(GetComponent<Collider>());
        }
    }

    Vector3 GetRandomColliderPoint(Collider collider)
    {
        Vector3 point = new Vector3(Random.Range(collider.bounds.min.x,collider.bounds.max.x), Random.Range(collider.bounds.min.y, collider.bounds.max.y), Random.Range(collider.bounds.min.z,collider.bounds.max.z));
        if(point != collider.ClosestPoint(point))
        {
            point = GetRandomColliderPoint(collider);
        }

        point.y = 0.5f;
        return point;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
