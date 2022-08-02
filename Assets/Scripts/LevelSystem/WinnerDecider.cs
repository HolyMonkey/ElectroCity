using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerDecider : MonoBehaviour
{
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _loseScreen;

    private JoyStickCanvas _joyStickCanvas;
    private int _counter;
    private bool _isWinned;

    private void Awake()
    {
        if (_winScreen.activeInHierarchy)
            _winScreen.SetActive(false);

        if (_loseScreen.activeInHierarchy)
            _loseScreen.SetActive(false);
    }

    private void Start()
    {
        _joyStickCanvas = FindObjectOfType<JoyStickCanvas>();
    }

    public void EndGame(Team team)
    {
        _isWinned = true;

        Player player = FindObjectOfType<Player>();
        player.Mover.Disable();

        if (team.TeamId == TeamId.First)
        {
            player.Animator.StartDancing();
            player.EffectsHandler.EnableWinEffects();
            StartCoroutine(DelayedEnable());

        }
        else
        {
            SetLose();
        }
    }

    private void SetLose()
    {
        _isWinned = false;
        StartCoroutine(DelayedLose(1f));
    }

    private IEnumerator DelayedEnable()
    {
        yield return new WaitForSeconds(1f);

        ShowScreen(_winScreen);
    }

    private IEnumerator DelayedLose(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (_isWinned == false)
            ShowScreen(_loseScreen);
    }

    private void ShowScreen(GameObject screen)
    {
        screen.SetActive(true);

        _joyStickCanvas.Disable();
    }
}
