using System.Collections;
using UnityEngine;

public sealed class SpawnManager : MonoBehaviour, IDeathObserver
{
    [SerializeField] private Transform[] prefabs;
    private Coroutine coroutine;

    private const int startSec = 2;
    private const int perSec = 2;

    private void Start() => coroutine = StartCoroutine(SpawnWithDelay());

    private IEnumerator SpawnWithDelay()
    {
        yield return new WaitForSeconds(startSec);
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(perSec);
        }
    }

    private void Spawn()
    {
        Vector3 spawnPoint = new Vector3(30, Random.Range(0,16), 0);
        int randIndex = Random.Range(0, prefabs.Length);
        Instantiate(prefabs[randIndex], spawnPoint, prefabs[randIndex].rotation);
    }

    public void ExecuteDeath()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
    }
}