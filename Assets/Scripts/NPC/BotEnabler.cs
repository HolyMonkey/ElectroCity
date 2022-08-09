using System.Collections;
using UnityEngine;
using NodeCanvas.BehaviourTrees;

public class BotEnabler : MonoBehaviour
{
    [SerializeField] private BehaviourTreeOwner _bot;
    [SerializeField] private float _enableDelay;
    [SerializeField] private bool _enableBehaviour;

    private void Awake()
    {
        //StartCoroutine(EnableWithDelay());

        if (_enableBehaviour)
            _bot.enabled = false;
    }

    public void Activate()
    {
        StartCoroutine(EnableWithDelay());
    }

    private IEnumerator EnableWithDelay()
    {
        yield return new WaitForSeconds(_enableDelay);

        _bot.gameObject.SetActive(true);

        if (_enableBehaviour)
            _bot.enabled = true;
    }
}
