using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{

    [SerializeField] private Transform _squashAlert;
    [SerializeField] private GameObject _titlePanel;
    [SerializeField] private GameObject _promptPanel;
    [SerializeField] private TextMeshProUGUI _promptTxt;
    [SerializeField] private TextMeshProUGUI _horizontalVelocityUI;
    [SerializeField] private TextMeshProUGUI _attackTimeUI;
    [SerializeField] private TextMeshProUGUI _attackRangeUI;
    private Sequence _squashAlertSequence;
    private Vector3 _originalPosition;
    private Vector3 _targetPosition;
    private float _originalScale;
    private float _targetScale;

    public static UnityEvent Alert = new UnityEvent();
    public static UnityEvent Alerted = new UnityEvent();

    void Start()
    {
        ZombieLocomotion.InPosition.AddListener(SquashAlert);
        ZombieLocomotion.DisplayTitleUI.AddListener(DisplayTitle);
        SquashLocomotion.DisplayTitleUI.AddListener(DisplayTitle);
        SquashLocomotion.Prompt.AddListener(ShowPrompt);
        SquashLocomotion.HorizontalVelocity.AddListener(ShowHorizontalVelocity);
        SquashLocomotion.AttackTime.AddListener(ShowAttackTime);
        SquashLocomotion.AttackRange.AddListener(ShowAttackRange);
        SquashLocomotion.ResetUI.AddListener(ResetUI);

        _originalPosition = _squashAlert.localPosition;
        _targetPosition = Vector3.zero;
        _originalScale = _squashAlert.localScale.magnitude;
        _targetScale = 1f;
    }

    private void SquashAlert()
    {
        Alert.Invoke();
        _squashAlert.gameObject.SetActive(true);
        _squashAlertSequence = DOTween.Sequence();
        _squashAlertSequence.Append(_squashAlert.DOScale(_targetScale, 0.5f));
        _squashAlertSequence.Join(_squashAlert.DOLocalMove(_targetPosition, 0.5f));
        _squashAlertSequence.Append(_squashAlert.DOPunchPosition(new Vector3(0.5f, 0.5f, 0.5f), 1f));
        _squashAlertSequence.Append(_squashAlert.DOScale(_originalScale, 0.5f));
        _squashAlertSequence.Join(_squashAlert.DOLocalMove(_originalPosition, 0.5f));
        _squashAlertSequence.OnComplete(() =>
        {
            _squashAlert.gameObject.SetActive(false);
            Alerted.Invoke();
        });
    }

    private void DisplayTitle(bool display)
    {
        _titlePanel.SetActive(display);
    }

    private void ShowPrompt(float horizontalVelocity)
    {
        StartCoroutine(IShowPrompt(horizontalVelocity));
    }

    private IEnumerator IShowPrompt(float horizontalVelocity)
    {
        _promptTxt.text = $"Horizontal Velocity has been CHANGED. New Value = {horizontalVelocity} m/s";
        _promptPanel.SetActive(true);
        yield return new WaitForSeconds(2);
        _promptPanel.SetActive(false);
    }

    private void ShowHorizontalVelocity(float horizontalVelocity)
    {
        _horizontalVelocityUI.text = $"Horizontal Velocity:\n{horizontalVelocity} m/s";
    }

    private void ShowAttackTime(float attackTime)
    {
        _attackTimeUI.text = $"Attack Time:\n{attackTime} s";
    }

    private void ShowAttackRange(float attackRange)
    {
        _attackRangeUI.text = $"Attack Range:\n{attackRange} m";
    }

    private void ResetUI(float horizontalVelocity)
    {
        _horizontalVelocityUI.text = $"Horizontal Velocity:\n{horizontalVelocity} m/s";
        _attackTimeUI.text = $"Attack Time:\n0 s";
        _attackRangeUI.text = $"Attack Range:\n0 m";
    }

}