using UnityEngine;

public sealed class DeathManager : MonoBehaviour
{
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private BackgroundRepeat bgRepeat;
    [SerializeField] private MoveLeft[] moveLeft;
    [SerializeField] private SpinObject spinObject;
    private Death death;

    private void Awake() => death = Death.Instance;

    private void OnEnable() => AddObservers();

    private void OnDestroy() => death.Clear();

    private void AddObservers()
    {
        death.Add(spawnManager);
        death.Add(bgRepeat);
        death.Add(spinObject);
        foreach (var move in moveLeft)
            death.Add(move);
    }
}