using DG.Tweening;
using TMPro;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _particlePrefab;
    [SerializeField] private GameObject _anchorParticlePrefab;

    [Space]
    [Header("HeroSettings")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private LineRenderer _lineRenderer;

    private Rigidbody2D _rigidbody;
    private Camera _camera;

    private bool _isMoving;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _camera = Camera.main;

        _rigidbody.simulated = false;

        _lineRenderer.transform.parent = null;
        _lineRenderer.transform.position = Vector3.zero;
        _lineRenderer.transform.localScale = Vector3.one;
    }
    private void OnEnable()
    {
        PlayerInput.Instance.PlayerMouseDown += OnPlayerMouseDown;
        PlayerInput.Instance.PlayerMouseUp += OnPlayerMouseUp;
    }
    private void OnDisable()
    {
        PlayerInput.Instance.PlayerMouseDown -= OnPlayerMouseDown;
        PlayerInput.Instance.PlayerMouseUp -= OnPlayerMouseUp;
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        if (_isMoving)
        {
            Vector2 direction = (Anchor.Instance.GetHook().position - transform.position).normalized;
            _rigidbody.velocity += direction * _speed * Time.fixedDeltaTime;

            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, Anchor.Instance.GetHook().transform.position);
        }
        else
        {
            _lineRenderer.SetPosition(0, Anchor.Instance.GetHook().transform.position);
            _lineRenderer.SetPosition(1, Anchor.Instance.GetHook().transform.position);
        }
    }
    private void SpawnParticle()
    {
        var particle = Instantiate(_particlePrefab).GetComponent<ParticleSystem>();

        particle.transform.position = new Vector2(transform.position.x, transform.position.y);
        particle.Play();

        Destroy(particle.gameObject, 2f);
    }
    private void SpawnAnchorParticle()
    {
        var particle = Instantiate(_anchorParticlePrefab).GetComponent<ParticleSystem>();

        particle.transform.position = Anchor.Instance.GetHook().position;
        particle.Play();

        Destroy(particle.gameObject, 2f);
    }
    private void OnPlayerMouseDown()
    {
        if (_isMoving == false)
        {
            _isMoving = true;
            _rigidbody.simulated = true;
            AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.Line, Random.Range(0.9f, 1.1f));
            SpawnAnchorParticle();
        }
    }
    private void OnPlayerMouseUp()
    {
        if (_isMoving == true)
        {
            _isMoving = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            PlayerScore.Instance.AddScore();
            WallManager.Instance.ChangeWall();
            AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.ScoreAdd, 1);
            AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.PopUp, Random.Range(0.9f, 1.1f));
            _camera.DOShakePosition(0.2f, 0.1f, fadeOut: true).SetUpdate(true);
            _camera.DOShakeRotation(0.2f, 0.1f, fadeOut: true).SetUpdate(true);
            SpawnParticle();
        }
        if (collision.gameObject.CompareTag("Spike"))
        {
            SpawnParticle();
            _camera.DOShakePosition(0.4f, 0.2f, fadeOut: true).SetUpdate(true);
            _camera.DOShakeRotation(0.4f, 0.2f, fadeOut: true).SetUpdate(true);
            GameState.Instance.FinishGame();
        }
    }
}
