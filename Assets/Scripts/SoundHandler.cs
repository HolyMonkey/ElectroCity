using System.Collections;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    [SerializeField] private AudioSource _win;
    [SerializeField] private AudioSource _set;
    [SerializeField] private AudioSource _lose;
    [SerializeField] private AudioSource _break;
    [SerializeField] private AudioSource _pickUp;
    [SerializeField] private AudioSource _capturing;
    [SerializeField] private AudioSource _background1;
    [SerializeField] private AudioSource _background2;

    private AudioSource _currentBackground;
    private int _levelCounter;

    public static SoundHandler Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }

       Destroy(gameObject);
    }

    private void Start()
    {
        _currentBackground = _background1;
    }

    public void PlayWinSound()
    {
        _win.Play();
        IncreaseLevelCounter();
    }

    public void StopWinSound()
    {
        _win.Stop();
    }

    public void PlayLoseSound()
    {
        StartCoroutine(StartingLoseSound(_currentBackground));
    }

    public void PlayPickUpSound()
    {
        RandomizePitch(_pickUp);
        _pickUp.Play();
    }

    public void PlaySetSound()
    {
        RandomizePitch(_set);
        _set.Play();
    }

    public void PlayBreakSound()
    {
        _break.Play();
    }

    public void PlayCapturingSound()
    {
        if (_capturing.isPlaying)
            return;

        RandomizePitch(_capturing);
        _capturing.Play();
    }

    public void TryChangeBackgroundMusic()
    {
        if(_levelCounter % 4 == 0)
        {
            if (_background1.isPlaying)
            {
                _background1.Stop();
                _background2.Play();
                _currentBackground = _background2;
                return;
            }

            _background2.Stop();
            _background1.Play();
            _currentBackground = _background1;
        }
    }

    private void IncreaseLevelCounter()
    {
        _levelCounter++;
    }

    private void RandomizePitch(AudioSource sound)
    {
        sound.pitch = Random.Range(0.8f, 1.2f);
    }

    private IEnumerator StartingLoseSound(AudioSource background)
    {
        background.Pause();
        _lose.Play();

        yield return new WaitUntil(() => _lose.isPlaying == false);

        background.UnPause();
    }
}
