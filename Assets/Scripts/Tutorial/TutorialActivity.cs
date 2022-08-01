using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialActivity : MonoBehaviour
{
    [SerializeField] private Building _mainBuilding;
    [SerializeField] private Building _secondBuilding;
    [SerializeField] private Arrow _arrow1;
    [SerializeField] private Arrow _arrow2;
    [SerializeField] private Arrow _arrow3;
    [SerializeField] private TMP_Text _value;
    [SerializeField] private Image _image;
    [SerializeField] private TextAnimator _animator;

    private const string SetCable = "Set the Cable!";
    private const string PullOut = "Pull Out the Cable!";

    private void OnEnable()
    {
        _mainBuilding.PickUpedRopesChanged += SwitchToSet;
        _secondBuilding.SettedRopesChanged += DisableSet;
        _secondBuilding.CapturingSystem.TeamChanged += SwitchToPull;
    }

    private void OnDisable()
    {
        _mainBuilding.PickUpedRopesChanged -= SwitchToSet;
        _secondBuilding.CapturingSystem.TeamChanged -= SwitchToPull;
        _secondBuilding.SettedRopesChanged -= DisablePull;

    }

    private void SwitchToSet()
    {
        _animator.DoRotation();
        _value.text = SetCable;
        _arrow1.gameObject.SetActive(false);
        _arrow2.gameObject.SetActive(true);
    }

    private void DisableSet()
    {
        _arrow2.gameObject.SetActive(false);
        _value.gameObject.SetActive(false);
        _image.gameObject.SetActive(false);
    }

    private void SwitchToPull(Team team)
    {
        _arrow3.gameObject.SetActive(true);
        _value.text = PullOut;
        _value.gameObject.SetActive(true);
        _image.gameObject.SetActive(true);
        _animator.DoRotation();
        _secondBuilding.SettedRopesChanged -= DisableSet;
        _secondBuilding.SettedRopesChanged += DisablePull;

    }

    private void DisablePull()
    {
        _arrow3.gameObject.SetActive(false);
        _value.gameObject.SetActive(false);
        _image.gameObject.SetActive(false);
        enabled = false;
    }
}
