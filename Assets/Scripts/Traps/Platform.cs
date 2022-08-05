using UnityEngine;
using DG.Tweening;

public class Platform : Trap
{
    [SerializeField] private Transform _endValue;
    [SerializeField] private float _tossPower;
    [SerializeField] private float _duration;

    private const string Toss = "Toss";

    public override void InterractWithPlayer()
    {
        Player.Animator.StartFalling();
        Player.Mover.Disable();
        Player.EffectsHandler.StopTrail();
        Animator.Play(Toss);
        TossOutOfLevel(Player.transform);
    }

    public override void InterractWithBot()
    {
        Animator.Play(Toss);
        //Bot.DisableBihaviour();
        TossOutOfLevel(Bot.transform);
    }

    private void TossOutOfLevel(Transform body)
    {
        body.DOLocalJump(_endValue.position, _tossPower, 1, _duration).SetEase(Ease.Linear);
    }
}
