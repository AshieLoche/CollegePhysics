using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject _promptPanel;
    [SerializeField] private TextMeshProUGUI _promptTxt;
    [SerializeField] private TextMeshProUGUI _horizontalVelocityUI;
    [SerializeField] private TextMeshProUGUI _attackTimeUI;
    [SerializeField] private TextMeshProUGUI _attackRangeUI;

    void Start()
    {
        SquashLocomotion.Prompt.AddListener(ShowPrompt);
        SquashLocomotion.HorizontalVelocity.AddListener(ShowHorizontalVelocity);
        SquashLocomotion.AttackTime.AddListener(ShowAttackTime);
        SquashLocomotion.AttackRange.AddListener(ShowAttackRange);
        SquashLocomotion.ResetUI.AddListener(ResetUI);
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
