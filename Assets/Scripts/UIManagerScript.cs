using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class UIManagerScript : MonoBehaviour
{
    public static UIManagerScript instance;

    GameManagerScript gms;

    [Header("Dynamic")]
    public Button mainButton;
    public GameObject levelProgressGo;
    public Image levelProgressFill;
    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI nextLevelText;
    public TextMeshProUGUI positionText;

    [Space(10)]
    [Header("Static")]
    public TextMeshProUGUI HeaderText;
    public TextMeshProUGUI FooterText;
    public GameObject gameNameGo;
    public GameObject raceCompletePanelParent;
    public GameObject raceCompletePanelHolder;
    public List<GameCompletePanel> gameCompletePanelList = new List<GameCompletePanel>();

  public  Statistics mainPlayerStats;
    [System.Serializable]
    public class GameCompletePanel
    {
        public TextMeshProUGUI posText;
        public TextMeshProUGUI playerNameTxt;
        public Image panelBarImage;
      //  public TextMeshProUGUI totalTimeTxt;
    }
    int currentLevel = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {

       

        FooterText.enabled = true;
        FooterText.text = "Tap To Start";
        raceCompletePanelParent.SetActive(false);
        levelProgressGo.SetActive(false);
        mainButton.enabled = true;
        gameNameGo.SetActive(true);
        gms = GameManagerScript.instance;
       

        currentLevel = gms.level;

        currentLevelText.text = currentLevel.ToString();
        nextLevelText.text = (currentLevel + 1).ToString();

        levelProgressFill.fillAmount = 0;
        positionText.text = ("6th").ToString();

        int i = 0;
        foreach (Transform item in raceCompletePanelHolder.transform)
        {
            gameCompletePanelList[i].panelBarImage = item.GetComponent<Image>();

            gameCompletePanelList[i].posText = item.GetChild(0).GetComponent<TextMeshProUGUI>();
            gameCompletePanelList[i].playerNameTxt =  item.GetChild(1).GetComponent<TextMeshProUGUI>();
            i++;
        }

        //for (int i = 0; i < gameCompletePanelList.Count; i++)
        //{
        //    GetComponentInChildren
        //}
      
    }
    private void Update()
    {
        if (gms.isGameStart)
        {
            levelProgressFill.fillAmount = mainPlayerStats.completion/100f;//  ((characterTransform.position.z - startZ) / divZ);
            positionText.text =( mainPlayerStats.rank+1).ToString();
        }
    }

    void GameStart()
    {
        gameNameGo.SetActive(false);

        mainButton.enabled = false;
        FooterText.enabled = false;
        levelProgressGo.SetActive(true);
        gms.GameStart();
    }

    void GameOver()
    {
        HeaderText.text = "Race Lost!";
        FooterText.text = "Tap To Restart";
    }

    void GameComplete()
    {
        HeaderText.text = "Race Won!";
        FooterText.text = "Tap To Continue";
    }
    public void SetUI(Statistics s,int i)
    {
        if(s.isPlayer)
            gameCompletePanelList[i - 1].panelBarImage.color = Color.yellow;
        gameCompletePanelList[i-1].posText.text = i.ToString(); 
        gameCompletePanelList[i-1].playerNameTxt.text = s.playerName;
    }
    public void ShowUI()
    {
        FooterText.enabled = true;
        raceCompletePanelParent.SetActive(true);
        mainButton.enabled = true;
        levelProgressGo.SetActive(false);

        if (gms.isGameComplete)
        {
            GameComplete();
        }
        else if (gms.isGameOver)
        {
            GameOver();
        }
        else
        {
            Debug.Log("No condition defined");
        }
    }

    public void _GameButton()
    {
        if (gms.isGameComplete)
        {
            //  SceneManager.LoadScene("Level"+(currentLevel+1).ToString());
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            if (Application.CanStreamedLevelBeLoaded("Level" + (currentLevel + 1).ToString()))
            {
                SceneManager.LoadScene("Level" + (currentLevel + 1).ToString());
            }
            else
            {
                if (Application.CanStreamedLevelBeLoaded("FinalLevel"))
                    SceneManager.LoadScene("FinalLevel");
                else
                {
                    PlayerPrefs.DeleteAll();
                    SceneManager.LoadScene("Level1");
                }
            }
        }
        else if (gms.isGameOver)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (!gms.isGameStart && !gms.isGameOver && !gms.isGameComplete)
        {
            GameStart();
        }
        else
        {
            Debug.Log("No condition defined");
        }
    }

}
