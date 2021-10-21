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

    [SerializeField] private Animator endAnimator;

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
            TriggerAnimation(2);
        }
        else if (pollution <= 0f)
        {
            OnGameEnded(_pollutionWinText);
            TriggerAnimation(1);
        }
    }

    public void TriggerAnimation(int animationType)
    {
        if (animationType == 0)
        {
            endAnimator.transform.parent.gameObject.SetActive(true);
            endAnimator.SetTrigger("Good");
        }
        if (animationType == 1)
        {
            endAnimator.transform.parent.gameObject.SetActive(true);
            endAnimator.SetTrigger("Meh");
        }
        if (animationType == 2)
        {
            endAnimator.transform.parent.gameObject.SetActive(true);
            endAnimator.SetTrigger("Bad");
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
