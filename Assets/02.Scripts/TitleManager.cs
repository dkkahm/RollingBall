using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class TitleManager : MonoBehaviour {
#if UNITY_IOS
	private string gameId = "1597025";
#elif UNITY_ANDROID
    private string gameId = "1597024";
#endif

    public string placementId = "video";

    GameObject m_start_game_button_obj;
    GameObject m_view_ads_button_obj;
    Text m_title_text;
    Text m_best_score_text;

    // Use this for initialization
    void Start()
    {
        m_start_game_button_obj = GameObject.Find("StartGameButton");
        m_start_game_button_obj.GetComponent<Button>().onClick.AddListener(OnStartGame);
        m_view_ads_button_obj = GameObject.Find("ViewAds");
        m_view_ads_button_obj.GetComponent<Button>().onClick.AddListener(OnShowAd);
        m_title_text = GameObject.Find("TitleText").GetComponent<Text>();

        m_best_score_text = GameObject.Find("BestScoreText").GetComponent<Text>();
        m_best_score_text.text = string.Format("Best Score: {0:F2}", PlayerPrefs.GetFloat("BestScore", 0f));

        Log("m_best_score_text is " + (m_best_score_text == null ? "null" : "not null"));

        if (Advertisement.isSupported)
        {
            Log("supported");
            Advertisement.Initialize(gameId, true);
        }
    }

    // Update is called once per frame
    void Update () {
        m_view_ads_button_obj.GetComponent<Button>().interactable = Advertisement.IsReady(placementId);
        // m_view_ads_button_obj.SetActive(Advertisement.IsReady(placementId));

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }

    void OnStartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    void OnShowAd()
    {
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowAdResult;

        Advertisement.Show(placementId, options);
    }

    void HandleShowAdResult(ShowResult result)
    {
        // OnStartGame();
    }

    private void Log(string text)
    {
        // m_title_text.text = text;
    }
}
