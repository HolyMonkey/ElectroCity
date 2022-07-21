using System;
using System.Collections;
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

    public bool IsBuildingNeutral => CapturingSystem.CurrentTeam.TeamId == TeamId.Netural;
    public TeamId TeamId => CapturingSystem.CurrentTeam.TeamId;
    public int PickUpedRopes => _pickUpedRopes;
    public int MaxPickUpedRopes => _maxPickUpedRopes;
    public bool AreRoesDestroyed => _areRopesDestroyed; 
    public CapturingSystem CapturingSystem { get; private set; } = new CapturingSystem();

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
    }

    private void CheckRopes()
    {
        foreach (var rope in _pickedRopes)
        {
            if (rope != null && rope.IsTorn)
            {
                _pickUpedRopes--;
                _pickedRopes.Remove(rope);
                break;
            }
        }
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
