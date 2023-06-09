using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;

public class Loaddata : MonoBehaviour
{
    public CinemachineVirtualCamera cam;
    public Image image;
    void Start()
    {
        cam = GameObject.Find("Isometric Camera").GetComponent<CinemachineVirtualCamera>();
        if (cam.Follow == null)
        {
            cam.Follow = MovementPlayer.instance.transform;
        }

        if(MovementPlayer.instance.uiPunch == null)
        {
        MovementPlayer.instance.uiPunch = GameObject.Find("DelayImagePunch");
         image = GameObject.Find("HealWarning").GetComponent<Image>();

        }
        if (MovementPlayer.instance.warning_health == null)
        {
            MovementPlayer.instance.warning_health = image;
            MovementPlayer.instance.healbar = GameObject.Find("HealBar").GetComponent<HealBar>(); 
            MovementPlayer.instance.SetHealBar();
        }
        if (MovementPlayer.instance.joyStick == null)
        {
            MovementPlayer.instance.joyStick = GameObject.Find("Floating Joystick").GetComponent<FloatingJoystick>();
        MovementPlayer.instance.transform.position = new Vector3(0,0,0);
        }
        if (MovementPlayer.instance.attackButton == null)
        {
            MovementPlayer.instance.attackButton = GameObject.Find("ButtonPunch2").GetComponent<Button>();
            MovementPlayer.instance.attackButton.onClick.AddListener(MovementPlayer.instance.OnPunchButton);
        }
        if (MovementPlayer.instance.changeWeapon == null)
        {
            MovementPlayer.instance.changeWeapon = GameObject.Find("ButtonChangeWeapon").GetComponent<Button>();
            MovementPlayer.instance.changeWeapon.onClick.AddListener(GameObject.Find("R_hand_container").GetComponent<WeaponSwitching>().OnOffSwitchButton);
        }
        if (MovementPlayer.instance.buttonNextWave == null)
        {
            MovementPlayer.instance.buttonNextWave = GameObject.Find("ButtonNextWave").GetComponent<Button>();
            MovementPlayer.instance.buttonNextWave.onClick.AddListener(SpawnManager.instance.NextWave);
        }


        if (SpawnManager.instance.buttonNextWave == null)
        {
            SpawnManager.instance.buttonNextWave = GameObject.Find("ButtonNextWave").GetComponent<Button>();
            SpawnManager.instance.timeSpawn = GameObject.Find("Time: ").GetComponent<TextMeshProUGUI>();
        }
        if(SpawnManager.instance.spawnzombie == null)
        {
            SpawnManager.instance.countWave = 0;
            SpawnManager.instance.check = true;
            //SpawnManager.instance.spawnzombie = StartCoroutine(SpawnManager.instance.DelaySpawnZombie());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
