using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Plug : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private ParticleSystem _particleSystem;

    public MeshRenderer MeshRenderer => _meshRenderer;

    private Coroutine _socketFlyingCoroutine;
    private Coroutine _playerFlyingCoroutine;

    public void DESTRUCTION()
    {
        gameObject.SetActive(false);
    }

    public void SetHandRotation()
    {
        _meshRenderer.transform.localRotation = Quaternion.Euler(0f, 90f, 90f);
        _meshRenderer.transform.localPosition = Vector3.zero;
    }

    public void SetSocketRotatation()
    {
        _meshRenderer.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
        _meshRenderer.transform.localPosition = Vector3.up*0.2f;
        _particleSystem.Play();
    }

    public void Raise()
    {
        if (_socketFlyingCoroutine != null)
            StopCoroutine(_socketFlyingCoroutine);

        _socketFlyingCoroutine = StartCoroutine(Flying(Vector3.up * 0.5f));
    }

    public void Set()
    {
        if (_socketFlyingCoroutine != null)
            StopCoroutine(_socketFlyingCoroutine);

        _socketFlyingCoroutine = StartCoroutine(Flying(Vector3.zero));
    }

    public void FlyTo(Transform transform, Action onCoroutineEnd)
    {
        if (_playerFlyingCoroutine != null)
            StopCoroutine(_playerFlyingCoroutine);

        _playerFlyingCoroutine = StartCoroutine(Flying(transform, onCoroutineEnd));
    }

    private IEnumerator Flying(Vector3 endPosition)
    {
        float elapsedTime = 0;
        Vector3 startPosition = transform.localPosition;
        float time = 0.2f;

        while (elapsedTime< time)
        {
            transform.localPosition = Vector3.Lerp(startPosition, endPosition, elapsedTime / time);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    private IEnumerator Flying(Transform endPoint, Action onCoroutineEnd)
    {
        while (Vector3.Distance(transform.position, endPoint.position) > 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint.position, 10f* Time.deltaTime);
            _meshRenderer.transform.LookAt(endPoint,Vector3.up);
            _meshRenderer.transform.rotation *= Quaternion.Euler(-90f, 0f, 0f);

            yield return null;
        }

        _meshRenderer.transform.rotation = Quaternion.identity;
        onCoroutineEnd();
    }
}
