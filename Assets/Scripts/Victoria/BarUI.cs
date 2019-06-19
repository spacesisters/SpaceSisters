using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarUI : MonoBehaviour
{
    private ArturPlayerOneController player1;
    private ArturPlayerTwoController player2;
    private ArturMetaInf metaInf;
    //private SplitScreenController splitScreenController;
    private ArturSplitScreenCamera splitScreenController;

    private GameObject[] scoretexts;
    private GameObject[] livetexts;

    private Transform barContainerRight;
    private Transform barContainerLeft;
    private Transform barTemplate;

    private List<barItem> barList_playerOne = new List<barItem>();
    private List<barItem> barList_playerTwo = new List<barItem>();
    private float templateHeight = 100f;
    private Vector3 barPositionLeft;
    private Vector3 barPositionRight;

    private GameObject scorebarleft;
    private GameObject scorebarright;
    private GameObject livesleft;
    private GameObject livesright;
    private GameObject middlecorner;

    private float speedBonusTimePlayer1;
    private float speedBonusTimePlayer2;
    void Start()
    {


        player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<ArturPlayerOneController>();
        player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<ArturPlayerTwoController>();
        metaInf = GetComponent<ArturMetaInf>();

        speedBonusTimePlayer1 = metaInf.speedBonusTime;
        speedBonusTimePlayer2 = metaInf.speedBonusTime;

        //splitScreenController = GameObject.Find("SplitScreen").GetComponent<SplitScreenController>();
        splitScreenController = GameObject.FindGameObjectWithTag("SplitScreenController").GetComponent<ArturSplitScreenCamera>();

        barContainerRight = GameObject.Find("Right Corner").GetComponent<Transform>();
        barTemplate = GameObject.Find("BarTemplate").GetComponent<Transform>();
        barTemplate.gameObject.SetActive(false); // Template is not visible
        barContainerLeft = GameObject.Find("Left Corner").GetComponent<Transform>();
        barPositionRight = new Vector3(1650, 1000, 0);
        barPositionLeft = new Vector3(-1650, 1000, 0);

        scorebarleft = GameObject.Find("ScoreBarLeft");
        scorebarright = GameObject.Find("ScoreBarRight");
        livesleft = GameObject.Find("LivesLeft");
        livesright = GameObject.Find("LivesRight");
        middlecorner = GameObject.Find("Middle Corner");

        AddNewBar(1, "energy");
        AddNewBar(2, "energy");

        scoretexts = GameObject.FindGameObjectsWithTag("HighScoreText");
        foreach (GameObject g in scoretexts)
        {
               g.GetComponent<Text>().text = "0";
        }

        livetexts = GameObject.FindGameObjectsWithTag("LiveText");
        foreach (GameObject g in livetexts)
        {
            g.GetComponent<Text>().text = metaInf.playerLives.ToString();
        }
        livesleft.transform.Find("Health").GetComponent<Image>().fillAmount = 1;
        livesright.transform.Find("Health").GetComponent<Image>().fillAmount = 1;
        middlecorner.transform.Find("LivesMiddle").Find("Health").GetComponent<Image>().fillAmount = 1;
    }
    private void AddNewBar(int player, string type)
    {
        Transform entryTransform = null;
        RectTransform entryRectTransform = null;
        barItem baritem = null;
        if (player == 1)
        {
            entryTransform = Instantiate(barTemplate, barContainerLeft);
            entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = barPositionLeft;
            entryTransform.gameObject.SetActive(true);
            baritem = new barItem { itemTransform = entryTransform, barType = type };
            barList_playerOne.Add(baritem);
            baritem.itemTransform.Find("OuterBar").Find("InnerBar").GetComponent<Image>().fillAmount = 1;
            barPositionLeft = new Vector3(barPositionLeft.x, barPositionLeft.y - templateHeight, barPositionLeft.z);

        }
        else if (player == 2)
        {
            entryTransform = Instantiate(barTemplate, barContainerRight);
            entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = barPositionRight;
            entryTransform.gameObject.SetActive(true);
            baritem = new barItem { itemTransform = entryTransform, barType = type };
            barList_playerTwo.Add(baritem);
            baritem.itemTransform.Find("OuterBar").Find("InnerBar").GetComponent<Image>().fillAmount = 1;

            barPositionRight = new Vector3(barPositionRight.x, barPositionRight.y - templateHeight, barPositionRight.z);
        }


        if (type.Equals("energy"))
        {
            baritem.itemTransform.Find("EnergySymbol").gameObject.SetActive(true);
            baritem.itemTransform.Find("SpeedSymbol").gameObject.SetActive(false);
        }
        else if (type.Equals("speedBonus"))
        {
            baritem.itemTransform.Find("EnergySymbol").gameObject.SetActive(false);
            baritem.itemTransform.Find("SpeedSymbol").gameObject.SetActive(true);

            baritem.itemTransform.Find("OuterBar").GetComponent<Image>().sprite = Resources.Load<Sprite>("menuScoreBar_GREEN");
            baritem.itemTransform.Find("OuterBar").Find("InnerBar").GetComponent<Image>().sprite = Resources.Load<Sprite>("menuBarFill_GREEN");
        }

    }
    void Update()
    {
        if (splitScreenController.isSplitted)
        {
            scorebarleft.SetActive(true);
            scorebarright.SetActive(true);

            livesleft.SetActive(true);
            livesright.SetActive(true);

            middlecorner.SetActive(false);
        }
        else
        {
            scorebarleft.SetActive(false);
            scorebarright.SetActive(false);

            livesleft.SetActive(false);
            livesright.SetActive(false);

            middlecorner.SetActive(true);
        }

        if (metaInf.playerLives > 0){
            foreach(GameObject score in scoretexts)
            {
                score.GetComponent<Text>().text = metaInf.score.ToString();
            }
            foreach (GameObject live in livetexts)
            {
                live.GetComponent<Text>().text = metaInf.playerLives.ToString();
            }
            livesleft.transform.Find("Health").GetComponent<Image>().fillAmount = (float)metaInf.playerHealth / (float)metaInf.initialPlayerHealth;
            livesright.transform.Find("Health").GetComponent<Image>().fillAmount = (float)metaInf.playerHealth / (float)metaInf.initialPlayerHealth;
            middlecorner.transform.Find("LivesMiddle").Find("Health").GetComponent<Image>().fillAmount = (float)metaInf.playerHealth / (float)metaInf.initialPlayerHealth;


            if (player1.speedBonus && !barList_playerOne.Contains(new barItem { itemTransform = null, barType = "speedBonus" }))
            {
                AddNewBar(1, "speedBonus");
            }
            else if ((!player1.speedBonus || speedBonusTimePlayer1 == 0f) && barList_playerOne.Contains(new barItem { itemTransform = null, barType = "speedBonus" }))
            {
                barItem bar = barList_playerOne.Find(x => x.barType == "speedBonus");
                bar.itemTransform.gameObject.SetActive(false);
                barList_playerOne.Remove(new barItem { itemTransform = null, barType = "speedBonus" });
                barPositionLeft = new Vector3(barPositionLeft.x, barPositionLeft.y + templateHeight, barPositionLeft.z);
                speedBonusTimePlayer1 = metaInf.speedBonusTime;
            }

            if (player2.speedBonus && !barList_playerTwo.Contains(new barItem { itemTransform = null, barType = "speedBonus" }))
            {
                AddNewBar(2, "speedBonus");
            }
            else if ((!player2.speedBonus||speedBonusTimePlayer2==0f) && barList_playerTwo.Contains(new barItem { itemTransform = null, barType = "speedBonus" }))
            {
                barItem bar = barList_playerTwo.Find(x => x.barType == "speedBonus");
                bar.itemTransform.gameObject.SetActive(false);
                barList_playerTwo.Remove(new barItem { itemTransform = null, barType = "speedBonus" });
                barPositionRight = new Vector3(barPositionRight.x, barPositionRight.y + templateHeight, barPositionRight.z);
                speedBonusTimePlayer2 = metaInf.speedBonusTime;
            }


            foreach (barItem bar in barList_playerOne)
            {
                switch (bar.barType)
                {
                    case "energy":
                        bar.itemTransform.Find("OuterBar").Find("InnerBar").GetComponent<Image>().fillAmount = player1.energy;
                        break;
                    //case "improvedAmmo":
                    //    bar.itemTransform.Find("Mask").GetComponent<Image>().fillAmount = player1.improvedAmmo; break;
                    case "speedBonus":
                        bar.itemTransform.Find("OuterBar").Find("InnerBar").GetComponent<Image>().fillAmount = (float)speedBonusTimePlayer1 / (float)metaInf.speedBonusTime;
                        if (speedBonusTimePlayer1 - Time.deltaTime > 0)
                        {
                            speedBonusTimePlayer1 -= Time.deltaTime;
                        }
                        else speedBonusTimePlayer1 = 0;
                        break;
                }

            }

            foreach (barItem bar in barList_playerTwo)
            {
                switch (bar.barType)
                {
                    case "energy":
                        bar.itemTransform.Find("OuterBar").Find("InnerBar").GetComponent<Image>().fillAmount = player2.energy;
                        break;
                    //case "improvedAmmo":
                    //    bar.itemTransform.Find("Mask").GetComponent<Image>().fillAmount = player2.improvedAmmo; break;
                    case "speedBonus":
                        bar.itemTransform.Find("OuterBar").Find("InnerBar").GetComponent<Image>().fillAmount = (float)speedBonusTimePlayer2 / (float)metaInf.speedBonusTime;
                        if (speedBonusTimePlayer2 - Time.deltaTime > 0)
                        {
                            speedBonusTimePlayer2 -= Time.deltaTime;
                        }
                        else speedBonusTimePlayer2 = 0;

                        break;
                }

            }
        }


    }

    private class barItem
    {
        public Transform itemTransform;
        public string barType;

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if ((obj as barItem) == null) return false;
            if (Object.ReferenceEquals(this, obj)) return true;

            barItem toCompare = obj as barItem;
            return toCompare.barType.Equals(this.barType);
        }
    }
}
