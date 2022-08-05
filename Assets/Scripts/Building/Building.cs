using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private int _initialPoints;
    [SerializeField] private bool _canGiveRopes = true;
    [SerializeField] private Team _initialTeam;
    [SerializeField] private List<Rope> _pickedRopes;
    [SerializeField] private List<Rope> _settedRopes;
    [SerializeField] private int _maxPickUpedRopes = 3;

    private bool _areRopesDestroyed;
    private readonly int _lowEnergyLevel = 20;
    private readonly int _mediumEnergyLevel = 40;
    private Coroutine _produceEnergyCoroutine;

    public IReadOnlyList<Rope> SettedRopes => _settedRopes;
    public int InitialPoints => _initialPoints;
    public bool IsBuildingNeutral => CapturingSystem.CurrentTeam.TeamId == TeamId.Netural;
    public TeamId TeamId => CapturingSystem.CurrentTeam.TeamId;
    public int PickUpedRopesCount => _pickedRopes.Count;
    public int MaxPickUpedRopes => _maxPickUpedRopes;
    public bool AreRoesDestroyed => _areRopesDestroyed; 
    public int TotalPoints => CapturingSystem.TotalPoints;
    public CapturingSystem CapturingSystem { get; private set; } = new CapturingSystem();


    public event Action PickUpedRopesChanged;
    public event Action SettedRopesChanged;
    public event Action<int> EnergyChecked;

    private void Awake()
    {
        CapturingSystem.Init(_initialTeam, _initialPoints);

        if (CapturingSystem.CurrentTeam.TeamId != TeamId.Netural)
            ProduceEnergy();

        CheckEnergy(CapturingSystem.TotalPoints);
    }

    private void OnEnable()
    {
        CapturingSystem.TeamChanged += TryDestroyOthersTeamsRopes;
        CapturingSystem.TeamChanged += ResetProduction;
        CapturingSystem.PointsChanged += CheckEnergy;
    }

    private void OnDisable()
    {
        CapturingSystem.TeamChanged -= TryDestroyOthersTeamsRopes;
        CapturingSystem.TeamChanged -= ResetProduction;
        CapturingSystem.PointsChanged -= CheckEnergy;
    }

    public bool GetTeamRopeCount()
    {
        bool isEqual = true;

        return isEqual;
    }

    public void AddSetedRope(Rope rope)
    {
        if(rope != null)
        {
            rope.Torned += OnRopeRemoved;
            _settedRopes.Add(rope);
            rope.Connect(CapturingSystem);
            SettedRopesChanged?.Invoke();
        }
    }

    public void AddPickedRope(Rope rope)
    {
        if (rope == null)
            return;

        rope.Torned += OnRopeRemoved;
        _pickedRopes.Add(rope);
        //rope.LaunchOnRopeConnected(ProduceHadouken);

        PickUpedRopesChanged?.Invoke();
    }

    private void CheckEnergy(int totalPoints)
    {
        if (_canGiveRopes)
        {
            //if (totalPoints > _mediumEnergyLevel)
            //{
            //    _maxPickUpedRopes = 3;
            //}

            //if (totalPoints > _lowEnergyLevel && TotalPoints <= _mediumEnergyLevel)
            //{
            //    _maxPickUpedRopes = 2;
            //}

            //if (totalPoints <= _lowEnergyLevel)
            //{
            //    _maxPickUpedRopes = 1;
            //}

            DestroyExcessivePickedRopes();
            EnergyChecked?.Invoke(_maxPickUpedRopes);
        }
    }

    //private void ProduceHadouken()
    //{
    //    if (_spawningCoroutine == null)
    //        _spawningCoroutine = StartCoroutine(GivingEnergy());
    //}

    public void OnRopeRemoved(Rope rope)
    {
        rope.Torned -= OnRopeRemoved;

        if(_settedRopes.Contains(rope))
            _settedRopes.Remove(rope);

        if (_pickedRopes.Contains(rope))
            _pickedRopes.Remove(rope);

        SettedRopesChanged?.Invoke();
        PickUpedRopesChanged?.Invoke();
    }

    private void TryDestroyOthersTeamsRopes(Team team)
    {

        foreach (var rope in _pickedRopes)
        {
            if (rope != null)
            {
                _areRopesDestroyed = true;
                rope.Disconnect();
            }
        }

        ProduceEnergy();
    }

    private void DestroyExcessivePickedRopes()
    {
        for (int i = _pickedRopes.Count-1; i >= _maxPickUpedRopes; i--)
        {
            if (_pickedRopes[i].IsConnected)
                _pickedRopes[i].Disconnect();
        }
    }

    //private IEnumerator GivingEnergy()
    //{
    //    while (_pickedRopes.Count > 0)
    //    {
    //        foreach (var rope in _pickedRopes)
    //        {
    //            if (rope.IsConnected)
    //                _ryu.LaunchHadouken(rope, this);
    //        }

    //        yield return new WaitForSeconds(1f);
    //    }

    //    _spawningCoroutine = null;
    //}

    private void ResetProduction(Team team)
    {
        StopCoroutine(_produceEnergyCoroutine);

        ProduceEnergy();
    }

    private void ProduceEnergy()
    {
        _produceEnergyCoroutine = StartCoroutine(ProducingEnergy());
    }

    private bool HasEnemyRope()
    {
        bool hasEnemyRope = false;

        foreach (var rope in _settedRopes)
        {
            if (rope.TeamId != CapturingSystem.CurrentTeam.TeamId)
                hasEnemyRope = true;
        }

        return hasEnemyRope;
    }

    private IEnumerator ProducingEnergy()
    {
        var delay = new WaitForSeconds(3f);

        while (true)
        {
            yield return delay;

            if (_pickedRopes.Count <= 0 && CapturingSystem.TotalPoints>3 && HasEnemyRope() == false)
            {
                CapturingSystem.IncreseEnergy(_maxPickUpedRopes);
            }
        }
    }
}
