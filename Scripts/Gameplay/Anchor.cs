using UnityEngine;

public class Anchor : MonoBehaviour
{
    public static Anchor Instance;

    [SerializeField] private Transform _hook;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }
    public Transform GetHook()
    {
        return _hook;
    }
}
