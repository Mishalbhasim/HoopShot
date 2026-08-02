using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MainMenu : MonoBehaviour
{
    [Header("UI Refs")]
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private TMP_Dropdown _modeDropdown;
    [SerializeField] private Button _playButton;
    [SerializeField] private GameObject _timerDisplay;

    [Header("Scene Refs")]
    [SerializeField] private HoopPlacer _hoopPlacer;

    [Header("ArSession Refs")]
    [SerializeField] private GameObject _arSession;


    private const int timedIndex = 0;
    private const int practiceIndex = 1;

    

    private void Awake()
    {
        _playButton.onClick.AddListener(HandlePlayPressed);
    }

    private void OnDestroy()
    {
        _playButton.onClick.RemoveListener(HandlePlayPressed);
    }

    private void OnEnable()
    {

        if (_modeDropdown.value != timedIndex && _modeDropdown.value != practiceIndex)
        {
            _modeDropdown.value = timedIndex;
        }
    }

    private void HandlePlayPressed()
    {
        //_arSession.SetActive(true);

        GameSession.CurrentMode = _modeDropdown.value == practiceIndex
            ? GameMode.Practice
            : GameMode.Timed;

        _timerDisplay.SetActive(GameSession.CurrentMode == GameMode.Timed);

        _menuPanel.SetActive(false);
        _hoopPlacer.EnablePlacement();
    }


    public void ShowMenu()
    {
        _menuPanel.SetActive(true);
        _hoopPlacer.DisablePlacement();
    }
}