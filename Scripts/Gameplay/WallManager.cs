using UnityEngine;

public class WallManager : MonoBehaviour
{
    public static WallManager Instance;

    [SerializeField] private GameObject _leftWall;
    [SerializeField] private GameObject _rightWall;

    private bool _isLeftWall = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        _leftWall.tag = "Untagged";
        _rightWall.tag = "Wall";
    }
    public void ChangeWall()
    {
        if (_isLeftWall)
        {
            _leftWall.tag = "Untagged";
            _rightWall.tag = "Wall";
        }
        else
        {
            _leftWall.tag = "Wall";
            _rightWall.tag = "Untagged";
        }
        _isLeftWall = !_isLeftWall;
    }
}
