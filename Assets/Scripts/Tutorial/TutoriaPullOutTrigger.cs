using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoriaPullOutTrigger : MonoBehaviour
{
    [SerializeField] private Arrow _arrow;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Player _player;
    private void OnEnable()
    {
        _player.RopeHandler.EnemyRopeBreaked += OnSettedRopesChanged;
    }

    private void OnDisable()
    {
        _player.RopeHandler.EnemyRopeBreaked -= OnSettedRopesChanged;
    }

    private void OnSettedRopesChanged()
    {
        _arrow.gameObject.SetActive(false);
        _canvas.gameObject.SetActive(false);
    }
}
