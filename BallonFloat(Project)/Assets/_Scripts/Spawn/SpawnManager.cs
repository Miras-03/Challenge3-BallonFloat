using System.Collections;
using UnityEngine;

public sealed class SpawnManager : MonoBehaviour, IDeathObserver
{
    private ObjectPooler pooler;

    private const int startSec = 2;
    private const int perSec = 2;

    private void Awake() => pooler = GetComponent<ObjectPooler>();

    private void Start()
    {
        pooler.CreateObjects(transform);
        StartCoroutine(SpawnWithDelay());
    }

    public void ExecuteDeath() => StopAllCoroutines();

    private IEnumerator SpawnWithDelay()
    {
        yield return new WaitForSeconds(startSec);
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(perSec);
        }
    }

    private void Spawn() => pooler.ReleaseObject(GenerateRandTag(), SpawnPosition(), Quaternion.identity);

    private string GenerateRandTag()
    {
        ObjectsType[] types = { ObjectsType.Bomb, ObjectsType.Money };
        int randIndex = Random.Range(0, types.Length);
        return types[randIndex].ToString();
    }

    private Vector3 SpawnPosition() => new Vector3(30, Random.Range(0, 16), 0);

    private enum ObjectsType
    {
        Bomb,
        Money
    }
}