using UnityEngine;
using System.Collections;

public class DoubleClickDetector : MonoBehaviour
{

    private int m_NumberOfClicks = 0;
    private float m_Timer = 0.0f;
    public float m_DoubleClickTimeWindow = 0.3f;

    public bool IsDoubleClick()
    {
        var isDoubleClick = m_NumberOfClicks == 2;
        if ( isDoubleClick )
            m_NumberOfClicks = 0;
        return isDoubleClick;
    }

    public void Update()
    {
        m_Timer += Time.deltaTime;

        if ( m_Timer > m_DoubleClickTimeWindow )
        {
            m_NumberOfClicks = 0;
        }

        if ( Input.GetMouseButtonDown( 0 ) == true )
        {
            m_NumberOfClicks++;
            m_Timer = 0.0f;
        }
    }
}
