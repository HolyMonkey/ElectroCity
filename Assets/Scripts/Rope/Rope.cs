using UnityEngine;
using Obi;
using static Obi.ObiRope;
using System.Collections;
using System;

public class Rope : MonoBehaviour
{
    [SerializeField] private ObiRope _obiRope;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private ObiParticleAttachment _endAttachment;
    [SerializeField] private ObiParticleAttachment _startAttachment;
    [SerializeField] private Plug _plug;

    private readonly float _movingDownSpeed = 0.5f;
    private readonly float _movingDownTime = 2f;
    private bool _isTorn;
    private bool _isConnected;
    private Action _onRopeConnected;

    public Team Team { get; private set; }
    public int Multiplier { get; private set; } = 1;
    public MeshRenderer Renderer => _meshRenderer;
    public ObiRope ObiRope => _obiRope;
    public Transform StartPoint => _startPoint;
    public Transform EndPoint => _plug.transform;
    public bool IsTorn => _isTorn;
    public bool IsConnected => _isConnected;
    public TeamId TeamId => Team.TeamId;
    public Plug Plug => _plug;

    public event Action<Rope> Torned;

    private void OnEnable()
    {
        _obiRope.OnRopeTorn += Disappear;
    }

    private void OnDisable()
    {
        _obiRope.OnRopeTorn -= Disappear;
    }

    public void SetTeamId(Team team)
    {
        Team = team;
    }

    public void LaunchOnRopeConnected(Action onRopeConnected)
    {
        _onRopeConnected = onRopeConnected;
    }

    public void Connect(CapturingSystem capturingSystem)
    {
        _isConnected = true;
        StartCoroutine(GivingEnergy(capturingSystem));
        StartCoroutine(MovingTexture());
        _onRopeConnected?.Invoke();
    }

    public void Disconnect(bool destroyRope = true)
    {
        _isConnected = false;

        if(destroyRope)
            Fall();
    }

    public void Disable()
    {
        Destroy(gameObject);
    }

    private void Fall()
    {
        StartCoroutine(Disappearing());
    }

    private void Disappear(ObiRope obiRope, ObiRopeTornEventArgs tearInfo)
    {
        Torned?.Invoke(this);
        _isTorn = true;
        _plug.DESTRUCTION();
        StartCoroutine(Disappearing());
    }

    private IEnumerator Disappearing()
    {
        yield return new WaitForSeconds(0.5f);

        _endAttachment.enabled = false;
        _startAttachment.enabled = false;
        //Destroy(_plug.gameObject);

        yield return new WaitForSeconds(2f);

        transform.SetParent(null);
        float elaspedTime = 0;

        while (elaspedTime <= _movingDownTime)
        {
            elaspedTime += Time.deltaTime;
            transform.Translate(_movingDownSpeed * Time.deltaTime * Vector3.down);
           
            yield return null;
        }

        Destroy(gameObject);
    }

    private IEnumerator GivingEnergy(CapturingSystem capturingSystem)
    {
        while (IsConnected)
        {
            capturingSystem.ApplyEnergy(Multiplier, Team);
            float frequency = 0.5f;

            if (capturingSystem.TotalPoints > capturingSystem.MaxPoints)
                frequency = 0.25f;

            yield return new WaitForSeconds(frequency);
        }
    }

    private IEnumerator MovingTexture()
    {
        float value = 0f;
        while (IsConnected)
        {
            value += Time.deltaTime;
            _meshRenderer.material.SetTextureOffset("_MainTex", Vector2.up * value);

            yield return null;
        }
    }
}
