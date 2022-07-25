using System.Collections;
using System.Collections.Generic;
using Obi;
using UnityEngine;

public class Hadouken : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private Rope _rope;
    private Building _buildingFrom;
    private bool _isDisabled;

    public TeamId TeamId => _rope.TeamId;

    private void OnDisable()
    {
        if (_rope != null)
            _rope.Torned -= Disable;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isDisabled)
            return;

        if (other.TryGetComponent(out Building building) && building != _buildingFrom)
            GiveEnergy(building);

        if (other.TryGetComponent(out Hadouken otherHadouken) && TeamId != otherHadouken.TeamId)
            Disable();
    }

    public void Throw(Rope rope, Building buildingFrom)
    {
        _rope = rope;
        _buildingFrom = buildingFrom;
        _particleSystem.startColor = _rope.Team.Color;
        StartCoroutine(FlyingAlongRope());
        _rope.Torned += Disable;
    }

    private void GiveEnergy(Building building)
    {
        building.CapturingSystem.ApplyEnergy(_rope.Multiplier, _rope.Team);
        Disable();
    }

    private void Disable()
    {
        _rope.Torned -= Disable;
        _isDisabled = true;
        Destroy(gameObject);
    }

    private IEnumerator FlyingAlongRope()
    {
        for (int i = _rope.ObiRope.activeParticleCount-1 ; i >0 ; i--)
        {
            Vector3 startPos = _rope.ObiRope.GetParticlePosition(i +1);
            Vector3 endPos = _rope.ObiRope.GetParticlePosition(i);

            float elapsedTime = 0;
            float time = 0.01f;

            while (elapsedTime < time && _rope.IsConnected)
            {
                transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / time);

                elapsedTime += Time.deltaTime;

                yield return null;
            }
        }
    }
}
