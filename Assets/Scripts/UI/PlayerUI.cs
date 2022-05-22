using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Slider _hpBar;
    [SerializeField] private Text _score;

    [Inject]
    public void Construct(PlayerLifeHandler playerLifeHandler, PlayerStatistics playerStatistics)
    {
        playerLifeHandler.onPlayerHPChanged += ChangeHPBar;
        playerStatistics.onChangedScore += ChangeScore;
    }

    private void ChangeHPBar(float percent)
    {
        _hpBar.value = _hpBar.maxValue * percent / 100;
    }

    private void ChangeScore(int score)
    {
        _score.text = score.ToString();
    }
}