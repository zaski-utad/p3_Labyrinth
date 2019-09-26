using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickBehaviour : MonoBehaviour
{
    private MainComponent m_mainComponent;

    float m_counterButtonTime = 0;

    private void Start()
    {
        m_mainComponent = GetComponent<MainComponent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_counterButtonTime = Time.time;
        }

        if ( Input.GetMouseButtonUp (0) && (Time.time - m_counterButtonTime < 0.25f))
        {
            m_mainComponent.PlayerAttack();
        }

        if ( Input.GetMouseButtonUp (0) && (Time.time - m_counterButtonTime >= 0.25f))
        {
            m_mainComponent.PlayerStrongAttack();
        }
    }
}
