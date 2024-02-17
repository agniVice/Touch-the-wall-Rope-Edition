using UnityEngine;

public class HeroSpawner : MonoBehaviour, ILevelSpawner
{
    [SerializeField] private GameObject _bubblePrefab;

    private GameObject _bubble;

    public void Build()
    {
        _bubble = Instantiate(_bubblePrefab);
    }
    public void Clear()
    {
        Destroy(_bubble);
    }
}