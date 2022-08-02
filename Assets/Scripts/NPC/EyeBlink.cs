using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBlink : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private AnimationCurve _animationCurve;

    private void Awake()
    {
        StartCoroutine(Blinking());
    }

    private IEnumerator Blinking()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 4));
            float elapsedTime = 0;
            float value = 0;
            float time = Random.Range(0.25f, 0.5f);

            while (elapsedTime< time)
            {
                value = 100 * _animationCurve.Evaluate(elapsedTime / time);
                _skinnedMeshRenderer.SetBlendShapeWeight(0, value);
                elapsedTime += Time.deltaTime;

                yield return null;
            }
 
        }
    }

}
