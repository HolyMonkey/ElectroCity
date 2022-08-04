using UnityEngine;
using DG.Tweening;

public class Platform : Trap
{
    [SerializeField] private Transform _endValue;
    [SerializeField] private float _tossPower;
    [SerializeField] private float _duration;

    private const string Toss = "Toss";

    public override void Interract()
    {
        Player.Animator.StartFalling();
        Player.Mover.Disable();
        Player.EffectsHandler.StopTrail();
        Animator.Play(Toss);
        TossOutOfLevel();
    }

    private void TossOutOfLevel()
    {
        Player.transform.DOLocalJump(_endValue.position, _tossPower, 1, _duration).SetEase(Ease.Linear);
    }
}
