using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitching : MonoBehaviour
{
    public int selectWeapon;
    public int previousSelect;
    private bool onButtonSwitch;
    private void Start()
    {
        selectWeapon = 0;
        previousSelect = selectWeapon;
        onButtonSwitch= false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || onButtonSwitch)
        {
            if (previousSelect == transform.childCount - 1)
            {
                selectWeapon = 0;
            }
            else
            {
                selectWeapon++;
            }
            previousSelect = selectWeapon;

            SwitchingWeapon();
        }
        onButtonSwitch= false;

    }

    void SwitchingWeapon()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i == selectWeapon)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

        }
    }

    public void OnOffSwitchButton()
    {
        onButtonSwitch= true;
    }
}
