using TMPro;
using UnityEngine;

public class GameEndController : MonoBehaviour
{
    [SerializeField] private TMP_Text _gameOverReasonText;
    [SerializeField] private TMP_Text _blocksPlacedText;
    [SerializeField] private TMP_Text _pollutionText;
    [SerializeField] private PollutionDisplay _pollutionDisplay;
    [SerializeField] private Board _board;
    [SerializeField] private string _pollutionWinText;
    [SerializeField] private string _pollutionLooseText;

    private void Awake()
    {
        _pollutionDisplay.NotifyPollutionChanged += OnPollutionChanged;
        gameObject.SetActive(false);
    }

    private void OnPollutionChanged(float pollution)
    {
        if (pollution >= _pollutionDisplay.MaxPollution)
        {
            OnGameEnded(_pollutionLooseText);
        }
        else if (pollution <= 0f)
        {
            OnGameEnded(_pollutionWinText);
        }
    }

    public void OnGameEnded(string reason)
    {
        gameObject.SetActive(true);
        _gameOverReasonText.text = reason;
        _blocksPlacedText.text = _board.BlocksPlaced.ToString();
        _pollutionText.text = _pollutionDisplay.CurrentPollution.ToString();
    }
}
