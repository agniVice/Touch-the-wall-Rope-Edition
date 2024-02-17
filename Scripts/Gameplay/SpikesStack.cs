using DG.Tweening;
using UnityEngine;

public class SpikesStack : MonoBehaviour
{
    private Vector2 _startPosition;

    private void Start()
    {
        Spawn();
    }
    private void Spawn()
    {
        _startPosition = transform.position;

        if(transform.localScale.x < 0)
            transform.position = new Vector2((transform.right).x -4f, _startPosition.y);
        else
            transform.position = new Vector2((transform.right).x + 2f, _startPosition.y);

        transform.DOMove(_startPosition, 0.5f).SetLink(gameObject).SetEase(Ease.OutBack);
    }
}
