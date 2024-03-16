using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Spawner")]
public class OrbSpawner : ScriptableObject
{
    [SerializeField]
    private List<Orb> Orbs = new List<Orb>();
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
            orbspawnNum = Random.Range(0, Orbs.Count);
        }
        else
        {
        orbspawnNum = Random.Range(0, Orbs.Count -1);
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
        return Instantiate(Orbs[orbToSpawn]);
    }
}
