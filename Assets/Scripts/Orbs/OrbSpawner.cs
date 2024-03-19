using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Spawner")]
public class OrbSpawner : ScriptableObject
{
    [SerializeField]
    private List<OrbData> orbDatas = new List<OrbData>();
    [SerializeField]
    private Orb spawningOrb;
    private int orbToSpawn;
    private int orbLastSpawn;
    private uint spawnCount;
    bool isEndGame = false;

    private void OnEnable()
    {
        spawnCount = 0;
        orbToSpawn = -1;
        orbLastSpawn = 3;
        isEndGame = false;
        EvolutionHandler.EndGameEvent += HandleEndGame;
        //Decide what Orb to spawn
    }

    private void HandleEndGame()
    {
        isEndGame = true;
    }

    public int PickOrb()
    {
        int orbspawnNum;
        int spawnlastOrb = Random.Range(1, 101);
        if (spawnlastOrb <= 5)
        {
            orbToSpawn = orbLastSpawn;
            Debug.Log("Picked: " + orbToSpawn);
            return orbToSpawn;
        }
        if (isEndGame)
        {
            orbspawnNum = Random.Range(0, orbDatas.Count);
        }
        else
        {
        orbspawnNum = Random.Range(0, orbDatas.Count -1);
        }
        orbToSpawn = orbspawnNum;
        Debug.Log("Picked: " + orbToSpawn);
        return orbToSpawn;
    }
    public Orb Spawn()
    {
        orbLastSpawn = orbToSpawn;
        spawnCount++;
        Debug.Log("Spawned: " + orbToSpawn);
        Orb spawnedOrb = Instantiate(spawningOrb);
        spawnedOrb.SetData(orbDatas[orbToSpawn]);
        return spawnedOrb;
    }
    public Orb SpawnSpecificOrb(OrbData orb, Vector3 pos, Quaternion rot)
    {
        Debug.Log("Spawned: " + orb);
        Orb spawnedOrb = Instantiate(spawningOrb, pos, rot);
        spawnedOrb.SetData(orb);
        return spawnedOrb;
    }
}
