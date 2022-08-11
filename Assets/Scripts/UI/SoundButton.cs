using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _soundOnSprite;
    [SerializeField] private Sprite _soundOffSprite;

    [SerializeField] private AudioListener _listener;
    private bool _isSoundEnabled = true;

    private void Start()
    {
        _listener = FindObjectOfType<AudioListener>();
    }

    public void OnButtonClick()
    {
        if (_isSoundEnabled)
        {
           DisableSound();
            return;
        }

        EnableSound();
    }

    private void DisableSound()
    {
        _listener.enabled = false;
        _isSoundEnabled = false;
        _image.sprite = _soundOffSprite;
        print(_listener.enabled);
    }

    private void EnableSound()
    {
        _listener.enabled = true;
        _isSoundEnabled = true;
        _image.sprite = _soundOnSprite;
        print(_listener.enabled);
    }
}
