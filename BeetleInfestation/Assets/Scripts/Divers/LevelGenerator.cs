using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Vector2 minDistanceBetweenPlatforms;
    [SerializeField] private Vector2[] maxDistanceBetweenPlatforms;

    [SerializeField] private int minPlatformsCount = 20;
    [SerializeField] private int maxPlatformsCount = 40;
    private int platformsToSpawn;

    [SerializeField] private Transform firstGround;

    List<Transform> groundsSpawned = new List<Transform>();
    List<bool> hasGroundInLeft = new List<bool>();
    List<bool> hasGroundInRight = new List<bool>();

    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject groundKillerPrefab;
    private Transform moreDownGround;

    private void Awake()
    {
        platformsToSpawn = Random.Range(minPlatformsCount, maxPlatformsCount);
        groundsSpawned.Add(firstGround);
        hasGroundInLeft.Add(false);
        hasGroundInRight.Add(false);
        moreDownGround = firstGround;
    }

    public void GenerateLevel()
    {
        if (groundsSpawned.Count >= platformsToSpawn)
        {
            GenerateBounds();
            return;
        }
        Vector2 maxDistance = maxDistanceBetweenPlatforms[Random.Range(0, maxDistanceBetweenPlatforms.Length)];
        Vector2 distance = new Vector2(Random.Range(minDistanceBetweenPlatforms.x, maxDistance.x),
                                        Random.Range(minDistanceBetweenPlatforms.y, maxDistance.y));
        int groundToSpawnID;
        do
        {
            groundToSpawnID = Random.Range(0, groundsSpawned.Count);
        }
        while (hasGroundInLeft[groundToSpawnID] && hasGroundInRight[groundToSpawnID]);
        int direction;
        if (!hasGroundInLeft[groundToSpawnID] && !hasGroundInRight[groundToSpawnID])
        {
            do
            {
                direction = Random.Range(-1, 2);
            }
            while (direction == 0);
        }
        else
        {
            direction = (!hasGroundInLeft[groundToSpawnID] ? -1 : 1);
        }
        Transform groundToSpawnAround = groundsSpawned[groundToSpawnID];
        distance += new Vector2(groundToSpawnAround.GetComponent<SpriteRenderer>().size.x / 2, 0);
        Vector2 newGroundPos = groundToSpawnAround.position + new Vector3(distance.x * direction, distance.y * direction);
        Transform newGround = GroundGenerator.gg.GenerateGround(newGroundPos);
        if (newGroundPos.y < moreDownGround.position.y)
        {
            moreDownGround = newGround;
        }
        newGround.transform.position += new Vector3(newGround.GetComponent<SpriteRenderer>().size.x / 2, 0) * direction;
        groundsSpawned.Add(newGround);
        hasGroundInLeft.Add(direction == 1);
        hasGroundInRight.Add(direction == -1);
        if (direction == 1)
        {
            hasGroundInRight[groundToSpawnID] = true;
        }
        else
        {
            hasGroundInLeft[groundToSpawnID] = true;
        }
        GenerateLevel();
    }

    void GenerateBounds()
    {
        GameObject wallL = null;
        GameObject wallR = null;
        foreach (bool b in hasGroundInLeft)
        {
            if (!b)
            {
                Transform ground = groundsSpawned[hasGroundInLeft.IndexOf(b)];
                Vector2 position = new Vector2(ground.transform.position.x - ground.GetComponent<SpriteRenderer>().size.x / 2 - 1,
                    ground.position.y - ground.GetComponent<SpriteRenderer>().size.y / 2 + 1);
                wallL = Instantiate(wallPrefab, position, transform.rotation);
            }
        }

        foreach (bool b in hasGroundInRight)
        {
            if (!b)
            {
                Transform ground = groundsSpawned[hasGroundInRight.IndexOf(b)];
                Vector2 position = new Vector2(ground.transform.position.x + ground.GetComponent<SpriteRenderer>().size.x / 2 + 1,
                    ground.position.y - ground.GetComponent<SpriteRenderer>().size.y / 2 + 1);
                wallR = Instantiate(wallPrefab, position, transform.rotation);
            }
        }

        GameObject groundKiller = Instantiate(groundKillerPrefab);
        float distanceBetweenBounds = Mathf.Abs(wallL.transform.position.x - wallR.transform.position.x);
        groundKiller.transform.localScale = new Vector2(distanceBetweenBounds, 1);
        groundKiller.transform.position = new Vector2(wallL.transform.position.x + distanceBetweenBounds / 2, moreDownGround.position.y-10);

        GameController.gc.finishedGrounds = true;
    }
}
