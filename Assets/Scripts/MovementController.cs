using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum FingerSide
{
    Left,
    Right
};

[System.Serializable]
public class DebugTextInfo
{
    public TMP_Text text;
    public string prefix;

    public void Init()
    {
        prefix = text.text;
    }
}

[System.Serializable]
public struct Bool3
{
    public bool x;
    public bool y;
    public bool z;
}

public class MovementController : MonoBehaviour
{
    [Range(0.0f, 10.0f)][SerializeField] float m_Speed;
    [Range(0,1)][SerializeField] float m_ScreenPortion;
    [SerializeField] FingerSide m_Side;
    [SerializeField] DebugTextInfo m_PositionDebug;
    [SerializeField] DebugTextInfo m_DeltaDebug;
    [SerializeField] Bool3 lockAxis;

    // Start is called before the first frame update
    void Start()
    {
        m_PositionDebug.Init();
        m_DeltaDebug.Init();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                Vector2 relativePosition = GetScreenRelativePosition(touch.position);

                // Debug
                // Debug.Log(touch.position);
                // Debug.Log(touch.deltaPosition);
                m_PositionDebug.text.text = m_PositionDebug.prefix + relativePosition;

                // Position
                switch(m_Side)
                {
                    case FingerSide.Left:
                        if (relativePosition.x < m_ScreenPortion) {
                            m_PositionDebug.text.color = Color.green;
                            m_DeltaDebug.text.text = m_DeltaDebug.prefix + touch.deltaPosition;
                            Move(touch.deltaPosition);
                            
                        } else {
                            m_PositionDebug.text.color = Color.red;
                            m_DeltaDebug.text.text = "NOT IN AREA";
                        }
                        break;    
                    case FingerSide.Right:
                        if (relativePosition.x > 1 - m_ScreenPortion) {
                            m_PositionDebug.text.color = Color.green;
                            m_DeltaDebug.text.text = m_DeltaDebug.prefix + touch.deltaPosition;
                            Move(touch.deltaPosition);                            
                        } else {
                            m_PositionDebug.text.color = Color.red;
                            m_DeltaDebug.text.text = "NOT IN AREA";
                        }
                        break;
                    default:
                        break;
                }                
            }
            
        }    
    }

    private void Move(Vector2 delta)
    {
        float x, y, z;
        Vector2 d = m_Speed * delta * 0.01f;
        if (!lockAxis.x)
            x = this.transform.position.x + d.x;
        else
            x = this.transform.position.x;
        if (!lockAxis.y) {
            y = this.transform.position.y + d.y;
            if (y < -10) y = -10;
            if (y > 10) y = 10;
        }
        else
            y = this.transform.position.y;
        z = this.transform.position.z;
        // if (!lockAxis.z)
        //     z = this.transform.position.z + d.z;
        // else
        //     x = this.transform.position.z;
                
        Vector3 newPosition = new Vector3(x, y, z);
        this.transform.position = newPosition;
    }
    
    private Vector2 GetScreenRelativePosition(Vector2 position)
    {
        Vector2 relativePosition;
        relativePosition.x = position.x/Screen.width;
        relativePosition.y = position.y/Screen.height;
        return relativePosition;
    }
}
