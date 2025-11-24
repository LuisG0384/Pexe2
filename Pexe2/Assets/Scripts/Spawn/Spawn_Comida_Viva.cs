using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [SerializeField] GameObject inimigo;
    [SerializeField] GameObject outimigo;

    [SerializeField] private float inimigoTimer;
    [SerializeField] private float outmigoTimer;

    SpawnManagerScript manager;

    private void Awake()
    {
        manager = transform.parent.GetComponent<SpawnManagerScript>();
    }
    private void Start()
    {
        StartCoroutine(spawnCreator(inimigoTimer, inimigo));
        StartCoroutine(spawnCreator(outmigoTimer, outimigo));
    }

    private IEnumerator spawnCreator(float interval, GameObject creature)
    {
        yield return new WaitForSeconds(interval);
        if (manager.pode)
        {
            GameObject newCreature = Instantiate(creature, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            manager.Aumenta();
        }
        StartCoroutine(spawnCreator(interval, creature));
    }
}
