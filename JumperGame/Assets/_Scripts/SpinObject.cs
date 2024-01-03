using System.Collections;
using UnityEngine;

public class SpinObject : MonoBehaviour, IDeathObserver
{
    private Coroutine coroutine;
    private const int spinSpeed = 5;

    private void Start() => coroutine = StartCoroutine(Spin());

    private IEnumerator Spin()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            transform.RotateAroundLocal(Vector3.up, spinSpeed * Time.fixedDeltaTime);
        }
    }

    public void ExecuteDeath() => StopCoroutine(coroutine);
}