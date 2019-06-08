using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarUI : MonoBehaviour
{
    private ArturPlayerOneController player1;
    private ArturPlayerTwoController player2;
    private ArturMetaInf metaInf;

    private GameObject[] scoretexts;

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

    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<ArturPlayerOneController>();
        player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<ArturPlayerTwoController>();
        metaInf = GetComponent<ArturMetaInf>();

        barContainerRight = GameObject.Find("Right Corner").GetComponent<Transform>();
        barTemplate = GameObject.Find("TemplateBar").GetComponent<Transform>();
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

        scoretexts = GameObject.FindGameObjectsWithTag("HighScoreText"); // TODO auh für livetext
        foreach (GameObject g in scoretexts)
        {
               g.GetComponent<Text>().text = "0";
        }

        // speedBonus.SetActive(false); // TODO hier nicht hin
    }
    private void AddNewBar(int player, string type)
    {
        Transform entryTransform = null;
        RectTransform entryRectTransform = null; 
        if (player == 1)
        {
            entryTransform = Instantiate(barTemplate, barContainerLeft);
            entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = barPositionLeft;
            entryTransform.gameObject.SetActive(true);
            barList_playerOne.Add(new barItem { itemTransform = entryTransform, barType = type });
            barList_playerOne[0].itemTransform.Find("Mask").GetComponent<Image>().fillAmount = 1;

            barPositionLeft = new Vector3(barPositionLeft.x, barPositionLeft.y - templateHeight, barPositionLeft.z);
        }
        else if (player == 2)
        {
            entryTransform = Instantiate(barTemplate, barContainerRight);
            entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = barPositionRight;
            entryTransform.gameObject.SetActive(true);
            barList_playerTwo.Add(new barItem { itemTransform = entryTransform, barType = type });
            barList_playerTwo[0].itemTransform.Find("Mask").GetComponent<Image>().fillAmount = 1;

            barPositionRight = new Vector3(barPositionRight.x, barPositionRight.y - templateHeight, barPositionRight.z);
        }

        if (type.Equals("improvedAmmo"))
        {
            entryTransform.GetComponent<Image>().sprite = Resources.Load<Sprite>("menuScoreBar_GREEN");
            entryTransform.Find("Mask").GetComponent<Image>().sprite = Resources.Load<Sprite>("menuBarFill_GREEN");

            //speedBonus.SetActive(true); // should activated when collectable is collected, then activated after X miliseconds TODO
        }

    }
    void Update()
    {
        if (metaInf.screenIsSplitted)
        {
            scorebarleft.SetActive(true);
            scorebarright.SetActive(true);

            livesleft.SetActive(true);
            livesright.SetActive(true);

            middlecorner.SetActive(false);
        }
        else if(!metaInf.screenIsSplitted)
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
            

            if (player1.improvedAmmo > 0 && !barList_playerOne.Contains(new barItem { itemTransform = null, barType = "improvedAmmo" }))
            {
                AddNewBar(1, "improvedAmmo");
            }
            else if (player1.improvedAmmo <= 0 && barList_playerOne.Contains(new barItem { itemTransform = null, barType = "improvedAmmo" }))
            {
                barItem bar = barList_playerOne.Find(x => x.barType == "improvedAmmo");
                bar.itemTransform.gameObject.SetActive(false);
                barList_playerOne.Remove(new barItem { itemTransform = null, barType = "improvedAmmo" });
            }

            if (player2.improvedAmmo > 0 && !barList_playerTwo.Contains(new barItem { itemTransform = null, barType = "improvedAmmo" }))
            {
                AddNewBar(2, "improvedAmmo");
            }
            else if (player2.improvedAmmo <= 0 && barList_playerTwo.Contains(new barItem { itemTransform = null, barType = "improvedAmmo" }))
            {
                barItem bar = barList_playerTwo.Find(x => x.barType == "improvedAmmo");
                bar.itemTransform.gameObject.SetActive(false);
                barList_playerTwo.Remove(new barItem { itemTransform = null, barType = "improvedAmmo" });
            }


            foreach (barItem bar in barList_playerOne)
            {
                switch (bar.barType)
                {
                    case "energy":
                        bar.itemTransform.Find("Mask").GetComponent<Image>().fillAmount = player1.energy; break;
                    case "improvedAmmo":
                        bar.itemTransform.Find("Mask").GetComponent<Image>().fillAmount = player1.improvedAmmo; break;
                    case "improvedSpeed":
                        bar.itemTransform.Find("Mask").GetComponent<Image>().fillAmount = player1.improvedSpeed; break;
                }

            }

            foreach (barItem bar in barList_playerTwo)
            {
                switch (bar.barType)
                {
                    case "energy":
                        bar.itemTransform.Find("Mask").GetComponent<Image>().fillAmount = player2.energy; break;
                    case "improvedAmmo":
                        bar.itemTransform.Find("Mask").GetComponent<Image>().fillAmount = player2.improvedAmmo; break;
                    case "improvedSpeed":
                        bar.itemTransform.Find("Mask").GetComponent<Image>().fillAmount = player2.improvedSpeed; break;
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
