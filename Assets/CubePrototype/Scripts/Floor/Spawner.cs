using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool isRunning = false;
    public FloorModule floorModPrefab;
     public Transform spawnPoint;
   public References[] referenceScript;

    public void Starter()
    {
        if (isRunning == false)
            StartCoroutine(SpawnModules());
    }

    IEnumerator SpawnModules()
    {
        Transform origin;
        isRunning = true;
        //... create and set each floor and their references.
        for (int i = 0; i < referenceScript.Length; i++)
        {
            if (i == 0)
            {   // first loop is spawnwers Transform
                spawnPoint = spawnPoint.transform;
                referenceScript[i].ref_PointOfOrigin = spawnPoint;
            }
            else
            {   // depends on last LEFT or RIGHT tile
                referenceScript[i].ref_PointOfOrigin = GManager.Instance.nextSpawnPoint;
            }

            // Instantiating floor at Spawn Point
            referenceScript[i].instance = Instantiate(floorModPrefab, referenceScript[i].ref_PointOfOrigin);
            referenceScript[i].Setup();

            origin = referenceScript[i].instance.GetComponent<Transform>();

           yield return referenceScript[i].instance.FloorBulder(origin, GManager.Instance.changeXDirection);

            // after loop is finished add new Transform refernce for origin
             referenceScript[i].NewRefrences(referenceScript[i].ref_Width);

            isRunning = false;
        }
    }




}

/*
     public void SpawnModules()
    {
        //... create and set each floor and their references.
        for (int i = 0; i < loopingPrefab.Length; i++)
        {
            // 
            loopingPrefab[i].instance =
                Instantiate(floorBuldertPrefab, loopingPrefab[i].spawnPoint.position, loopingPrefab[i].spawnPoint.rotation) as GameObject;
            loopingPrefab[i].moduleNumber = i + 1;
            loopingPrefab[i].Setup();

        }
    }

 */