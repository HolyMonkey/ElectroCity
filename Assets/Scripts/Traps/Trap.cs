using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trap : MonoBehaviour
{
	[SerializeField] protected Animator Animator;

    protected Player Player;
    protected Bot Bot;

    public abstract void InterractWithPlayer();
    public abstract void InterractWithBot();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            Player = player;
            InterractWithPlayer();
        }
        else if(other.gameObject.TryGetComponent(out Bot bot))
        {
            Bot = bot;
            InterractWithBot();
        }
    }
}
