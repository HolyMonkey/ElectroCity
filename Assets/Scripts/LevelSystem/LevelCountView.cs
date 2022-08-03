using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelCountView : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelView;

    private void OnEnable()
    {
        _levelView.text = $"Level {SaveSystem.LoadLevelNumber()}";
    }
}
