using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject wolfPrefab;
    [SerializeField] private GameObject harePrefab;
    [SerializeField] private GameObject deerPrefab;
    [SerializeField] private GameObject deerGroupPrefab;
    // Start is called before the first frame update

    public GameObject SpawnHare(Vector3 position)
    {
        var newHare = Instantiate(harePrefab, position, Quaternion.identity, transform);
        return newHare;
    }

    public GameObject SpawnWolf(Vector3 position)
    {
        var newWolf = Instantiate(wolfPrefab, position, Quaternion.identity, transform);
        return newWolf;
    }

    public GameObject SpawnDeersGroup(Vector3 position)
    {
        var newGroup = Instantiate(deerGroupPrefab, position, Quaternion.identity, transform);
        return newGroup;
    }

    public GameObject SpawnDeerOnGroup(GameObject deersGroup, Vector3 positionOnGroup)
    {
        var newDeer = Instantiate(deerPrefab, deersGroup.transform.position + positionOnGroup, 
            Quaternion.identity, deersGroup.transform);
        return newDeer;
    }
}
