using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class WheelSpinner : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float m_RotationOffset;
    [SerializeField] private int m_LoopAmount = 10;
    [SerializeField] [Range(0.1f, 5f)] private float m_SpinSpeed = 1;
    [SerializeField] private int[] m_Rewards;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI[] m_WheelTexts;
    [SerializeField] private Transform m_WheelPlate;
    [SerializeField] private Button m_SpinButton;
    [SerializeField] private TextMeshProUGUI m_InfoText;

    private void Start()
    {
        InitializeWheel();

        m_SpinButton.onClick.AddListener(Spin);
    }

    private void InitializeWheel()
    {
        for (int i = 0; i < m_WheelTexts.Length; i++)
        {
            m_WheelTexts[i].text = "$" + m_Rewards[i].ToString();
        }
    }

    private void Spin()
    {
        m_InfoText.text = "";
        m_WheelPlate.transform.rotation = Quaternion.Euler(0, 0, 0);

        m_SpinButton.interactable = false;

        int randomAngle = Random.Range(0, 360);
        int earnedReward = ConvertAngleToReward(randomAngle);
        Vector3 endValue = new Vector3(0, 0, 360 * m_LoopAmount + randomAngle + m_RotationOffset);

        Tween rotationTween = m_WheelPlate.DORotate(endValue, 1f / m_SpinSpeed * m_LoopAmount, RotateMode.FastBeyond360);
        rotationTween.SetEase(Ease.OutCirc);
        rotationTween.onComplete = () =>
        {
            m_InfoText.text = "Won $" + earnedReward;
            m_SpinButton.interactable = true;
        };

    }

    private int ConvertAngleToReward(int angle)
    {
        int wheelAngle = 360 / m_Rewards.Length;
        int rewardIndex = angle / wheelAngle;

        return m_Rewards[rewardIndex];
    }
}
