using UnityEngine;

public sealed class OutOfBoundDisable : MonoBehaviour
{
    private const float bound = -15;

    private void FixedUpdate() => CheckOrDestroy();

    private void CheckOrDestroy()
    {
        float currentXPos = transform.position.x;
        if (currentXPos <= bound)
            gameObject.SetActive(false);
    }
}
