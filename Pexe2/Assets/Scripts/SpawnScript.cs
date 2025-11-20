using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [SerializeField] GameObject inimigo;
    [SerializeField] GameObject outimigo;

    [SerializeField] private float inimigoTimer;
    [SerializeField] private float outmigoTimer;

    private void Start()
    {
        StartCoroutine(spawnCreator(inimigoTimer, inimigo));
        StartCoroutine(spawnCreator(outmigoTimer, outimigo));
    }

    private IEnumerator spawnCreator(float interval, GameObject creature)
    {
        yield return new WaitForSeconds(interval);
        GameObject newCreature = Instantiate(creature, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        StartCoroutine(spawnCreator(interval, creature));
    }
}
