using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CapacityUnrise : MonoBehaviour
{
    public TextMeshProUGUI day;
    private Color colorDay;
    private Color colorPanel;
    public Image panelDay;

    void Start()
    {
        colorDay= Color.white;
        colorPanel= Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        colorDay.a -= Time.deltaTime * 0.4f;
        colorPanel.a -= Time.deltaTime * 0.4f;
        day.color = colorDay;
        panelDay.color = colorPanel;
    }
}
