using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameObject m_score_text_obj;
    GameObject m_log_text_obj;

    float m_score = 0f;

    Color m_off_score_color = new Color(50, 50, 50);
    Color m_on_score_color = new Color(0, 255, 0);

    bool m_ball_in_hot_zone = false;

    // Use this for initialization
    void Start()
    {
        m_score_text_obj = GameObject.Find("ScoreText");
        m_log_text_obj = GameObject.Find("LogText");

        UpdateObjectsForDebug();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 acceleration = Input.acceleration;

        Physics.gravity = new Vector3(acceleration.x, acceleration.z, acceleration.y) * 20f;

        UpdateScore();

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }

    public void OnBallDropped()
    {
        float best_score = PlayerPrefs.GetFloat("BestScore");
        if (m_score > best_score)
        {
            PlayerPrefs.SetFloat("BestScore", m_score);
        }
        SceneManager.LoadScene("TitleScene");
    }

    public void OnBallOn()
    {
        Debug.Log("Ball On");

        m_ball_in_hot_zone = true;
    }

    public void OnBallOff()
    {
        Debug.Log("Ball Off");

        m_ball_in_hot_zone = false;
    }

    private void UpdateScore()
    {
        if(m_ball_in_hot_zone)
        {
            m_score += Time.deltaTime;
            m_score_text_obj.GetComponent<Text>().color = m_on_score_color;
        }
        else
        {
            m_score_text_obj.GetComponent<Text>().color = m_off_score_color;
        }

        m_score_text_obj.GetComponent<Text>().text = string.Format("Score: {0:F2}", m_score);
    }

    private void UpdateObjectsForDebug()
    {
        if (Application.platform != RuntimePlatform.WindowsPlayer)
        {
            m_log_text_obj.SetActive(false);
        }
    }

    private void Log(string text)
    {
        m_log_text_obj.GetComponent<Text>().text = text;
    }
}
