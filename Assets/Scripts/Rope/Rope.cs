using UnityEngine;
using Obi;
using static Obi.ObiRope;
using System.Collections;
using System;

public class Rope : MonoBehaviour
{
    [SerializeField] private ObiRope _obiRope;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private ObiParticleAttachment _endAttachment;
    [SerializeField] private ObiParticleAttachment _startAttachment;

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
    public Transform EndPoint => _endPoint;
    public bool IsTorn => _isTorn;
    public bool IsConnected => _isConnected;
    public TeamId TeamId => Team.TeamId;

    public event Action Torned;

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
        //StartCoroutine(GivingEnergy(capturingSystem));
        _onRopeConnected?.Invoke();
    }

    public void Disconnect()
    {
        _isConnected = false;
        Fall();
        Torned?.Invoke();
    }

    public void Fall()
    {
        StartCoroutine(Disappearing());
    }

    private void Disappear(ObiRope obiRope, ObiRopeTornEventArgs tearInfo)
    {
        _isTorn = true;
        StartCoroutine(Disappearing());
    }

    private IEnumerator Disappearing()
    {
        yield return new WaitForSeconds(0.5f);

        _endAttachment.enabled = false;
        _startAttachment.enabled = false;

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

    //private IEnumerator GivingEnergy(CapturingSystem capturingSystem)
    //{
    //    while (IsConnected)
    //    {
    //        capturingSystem.ApplyEnergy(Multiplier, Team);
    //        float frequency = Random.Range(0.2f, 0.23f);

    //        yield return new WaitForSeconds(frequency);
    //    }
    //}
}
