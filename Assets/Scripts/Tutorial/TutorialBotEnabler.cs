using NodeCanvas.BehaviourTrees;
using UnityEngine;

public class TutorialBotEnabler : MonoBehaviour
{
    [SerializeField] private Building _building;
    [SerializeField] private BehaviourTreeOwner _botBehaviour;

    private void Awake()
    {
        _botBehaviour.enabled = false;    
    }

    private void OnEnable()
    {
        _building.CapturingSystem.TeamChanged += OnBuildingCaptured;
    }

    private void OnDisable()
    {
        _building.CapturingSystem.TeamChanged -= OnBuildingCaptured;
    }

    private void OnBuildingCaptured(Team team)
    {
        _botBehaviour.enabled = true;
    }

}
