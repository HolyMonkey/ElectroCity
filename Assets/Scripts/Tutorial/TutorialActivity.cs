using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialActivity : MonoBehaviour
{
    [SerializeField] private Building _mainBuilding;
    [SerializeField] private Building _secondBuilding;
    [SerializeField] private Arrow _arrow1;
    [SerializeField] private Arrow _arrow2;
    [SerializeField] private TMP_Text _value;
    [SerializeField] private Image _image;

    private const string SetRope = "Set the Cable!";

    private void OnEnable()
    {
        _mainBuilding.PickUpedRopesChanged += SwitchToSet;
        _secondBuilding.SettedRopesChanged += Disable;
    }

    private void OnDisable()
    {
        _mainBuilding.PickUpedRopesChanged -= SwitchToSet;
        _secondBuilding.SettedRopesChanged -= Disable;
    }

    private void SwitchToSet()
    {
        _value.text = SetRope;
        _arrow1.gameObject.SetActive(false);
        _arrow2.gameObject.SetActive(true);
    }

    private void Disable()
    {
        _arrow2.gameObject.SetActive(false);
        _value.gameObject.SetActive(false);
        _image.gameObject.SetActive(false);
        enabled = false;
    }
}
