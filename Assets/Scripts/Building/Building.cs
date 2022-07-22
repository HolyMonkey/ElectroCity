using System;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private int _initialPoints;
    [SerializeField] private Team _initialTeam;
    [SerializeField] private List<Rope> _pickedRopes;
    [SerializeField] private List<Rope> _settedRopes;

    private int _maxPickUpedRopes = 3;
    private int _pickUpedRopes;
    private bool _areRopesDestroyed;
    private readonly int _lowEnergyLevel = 30;
    private readonly int _mediumEnergyLevel = 60;

    public int InitialPoints => _initialPoints;
    public bool IsBuildingNeutral => CapturingSystem.CurrentTeam.TeamId == TeamId.Netural;
    public TeamId TeamId => CapturingSystem.CurrentTeam.TeamId;
    public int PickUpedRopes => _pickUpedRopes;
    public int MaxPickUpedRopes => _maxPickUpedRopes;
    public bool AreRoesDestroyed => _areRopesDestroyed; 
    public CapturingSystem CapturingSystem { get; private set; } = new CapturingSystem();
    public int TotalPoints => CapturingSystem.TotalPoints;

    public event Action PickUpedRopesChanged;
    public event Action<int> EnergyChecked;

    private void Awake()
    {
        CapturingSystem.Init(_initialTeam, _initialPoints);
    }

    private void OnEnable()
    {
        CapturingSystem.TeamChanged += TryDestroyOthersTeamsRopes;

    }

    private void OnDisable()
    {
        CapturingSystem.TeamChanged -= TryDestroyOthersTeamsRopes;
    }

    private void Update()
    {
        CheckRopes();
        CheckEnergy();
    }

    public void AddSetedRope(Rope rope)
    {
        _settedRopes.Add(rope);

        float teamRopeAmount = 0;

        foreach (var settedRope in _settedRopes)
        {
            if (rope.TeamId == settedRope.TeamId)
                teamRopeAmount++;
        }

        rope.Connect(CapturingSystem, teamRopeAmount);
    }

    public void AddPickedRope(Rope rope)
    {
        _pickedRopes.Add(rope);
        _pickUpedRopes++;
        PickUpedRopesChanged?.Invoke();
    }

    private void CheckRopes()
    {
        foreach (var rope in _pickedRopes)
        {
            if (rope != null && rope.IsTorn)
            {
                _pickUpedRopes--;
                PickUpedRopesChanged?.Invoke();
                _pickedRopes.Remove(rope);
                break;
            }
        }
    }

    private void CheckEnergy()
    {
        if (TotalPoints > _mediumEnergyLevel)
        {
            _maxPickUpedRopes = 3;
        }

        if (TotalPoints > _lowEnergyLevel && TotalPoints <= _mediumEnergyLevel)
        {
            _maxPickUpedRopes = 2;
        }

        if (TotalPoints <= _lowEnergyLevel)
        {
            _maxPickUpedRopes = 1;
        }

        EnergyChecked?.Invoke(_maxPickUpedRopes);
    }

    private void TryDestroyOthersTeamsRopes(Team team)
    {
        foreach(var rope in _settedRopes)
        {
            if(rope.TeamId != team.TeamId && rope != null)
            {
                _areRopesDestroyed = true;
                rope.Disconnect();
                rope.Fall();
                //Destroy(rope.gameObject);
            }
        }
    }

    
}
