using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class menuEnergyBar : MonoBehaviour
{

    public GameObject speedBonus;
    private Transform barContainer;
    private Transform barTemplate;
    private List<barItem> barList = new List<barItem>();
    private float templateHeight = 100f;
    private ArturPlayerOneController player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player1").GetComponent<ArturPlayerOneController>();
        barContainer = GameObject.Find("right corner").GetComponent<Transform>();
        barTemplate = GameObject.Find("energybar").GetComponent<Transform>();
        barTemplate.gameObject.SetActive(false);

        // energy bar (always visible)
        Transform entryTransform = Instantiate(barTemplate, barContainer);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector3(1650, 900, 0);
        entryTransform.gameObject.SetActive(true);

        barList.Add(new barItem { itemTransform = entryTransform, barType = "energy" });
        barList[0].itemTransform.Find("Mask").GetComponent<Image>().fillAmount = 1;

        speedBonus.SetActive(false); // hier nicht hin
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.dead)
        {
                if (player.improvedAmmo > 0 && !barList.Contains(new barItem { itemTransform = null, barType = "improvedAmmo" }))
            {
                Transform entryTransform = Instantiate(barTemplate, barContainer);
                RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
                entryRectTransform.anchoredPosition = new Vector3(1650, 900 -templateHeight * barList.Count, 0);
                entryTransform.GetComponent<Image>().sprite = Resources.Load<Sprite>("menuScoreBar_GREEN");
                entryTransform.Find("Mask").GetComponent<Image>().sprite = Resources.Load<Sprite>("menuBarFill_GREEN");
                entryTransform.gameObject.SetActive(true);
                speedBonus.SetActive(true); // should activated when collectable is collected, then activated after X miliseconds TODO


                barList.Add(new barItem { itemTransform = entryTransform, barType = "improvedAmmo" });


            }
            else if (player.improvedAmmo <= 0 && barList.Contains(new barItem { itemTransform = null, barType = "improvedAmmo" }))
            {
                barItem bar = barList.Find(x => x.barType == "improvedAmmo");
                bar.itemTransform.gameObject.SetActive(false);
                barList.Remove(new barItem { itemTransform = null, barType = "improvedAmmo"});
            }

       
            foreach (barItem bar in barList)
            {
                switch (bar.barType)
                {
                    case "energy":
                        bar.itemTransform.Find("Mask").GetComponent<Image>().fillAmount = player.energy; break;
                    case "improvedAmmo":
                        bar.itemTransform.Find("Mask").GetComponent<Image>().fillAmount = player.improvedAmmo; break;
                    case "improvedSpeed":
                        bar.itemTransform.Find("Mask").GetComponent<Image>().fillAmount = player.improvedSpeed;break;
                }
               
            }
        }

    }

    private class barItem{
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
