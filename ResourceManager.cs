using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ResourceManager : MonoBehaviour
{
    public int Mulch;
    public int Wood;
    public int Spores;
    public int UsedSpores;
    public int Food;
    public int FoodCost;
    public int MulchEarning;
    public int FoodEarning;
    public int WoodEarning;

    public TextMeshProUGUI SporeText;
    public TextMeshProUGUI MulchText;
    public TextMeshProUGUI FoodText;
    public TextMeshProUGUI WoodText;

    public ClickAndPlace click;
    public bool Paused = true;
    public GameObject PausePanel;
    public GameObject WinPanel;
    public GameObject water;
    public GameObject Level1;
    public GameObject Level2;
    public AudioSource waterclip;

    public void TextUpdate()
    {
        SporeText.text = UsedSpores + " / " + Spores;
        MulchText.text = Mulch.ToString();
        FoodText.text = Food.ToString();
        WoodText.text = Wood.ToString();

    }

    void Start()
    {
        PausePanel.SetActive(true);
        TextUpdate();
        StartCoroutine(GameTickCoroutine());

    }

    public void Pauser(bool pause)
    {
        Paused = pause;
    }

    private IEnumerator GameTickCoroutine()
    {
        while (true)
        {
            if(!Paused)
            GameTick();

            yield return new WaitForSeconds(3.5f);
        }
    }

    private void GameTick()
    {
        Mulch = Mulch + MulchEarning;
        Wood = Wood + WoodEarning;
        Food = Food + FoodEarning - FoodCost;

        TextUpdate();
        click.Tick();
        WaterUpdate();
        waterclip.Play();
    }

    private void WaterUpdate()
    {
        if(water.transform.position.y < 0.45)
        water.transform.position += new Vector3(0, 0.02f, 0);



        if(water.transform.position.y > 0.12)
        {
            if (!Level1.activeSelf)
            {
                click.DeSpawn(0.12f);
            }

            Level1.SetActive(false);
        }

        if (water.transform.position.y > 0.42)
        {
            if (!Level1.activeSelf)
            {
                click.DeSpawn(0.42f);
            }

            Level2.SetActive(false);
        }
    }

    void Update()
    {



    }

    internal void PayFor(int prefabType)
    {
        if (prefabType == 0)
        {
            UsedSpores = UsedSpores + 1;
            Mulch = Mulch - 5;
            MulchEarning = MulchEarning + 3;
        }
        else if (prefabType == 3)
        {
            FoodCost = FoodCost + 1;
            Mulch = Mulch - 10;
            Spores = Spores + 3;
        }
        else if (prefabType == 6)
        {
            UsedSpores = UsedSpores + 1;
            Mulch = Mulch - 10;
            FoodEarning = FoodEarning + 2;
        }
        else if(prefabType == 9)
        {
            UsedSpores = UsedSpores + 1;
            Mulch = Mulch - 15;
            WoodEarning = WoodEarning + 1;
        }
        TextUpdate();
    }

    internal bool OpenSpores()
    {
        return Spores > UsedSpores;
    }

    public void Win()
    {
        Paused = true;
        UsedSpores = UsedSpores + 3;
        Wood = Wood - 65;
        WinPanel.SetActive(true);
    }

    public void Reload()
    {
        SceneManager.LoadScene(0);
    }

    public void BackTrack(int i)
    {
        if (i == 0)
        {
            UsedSpores = UsedSpores - 1;
            //Mulch = Mulch - 5;
            MulchEarning = MulchEarning - 3;
        }
        else if (i == 3)
        {
            FoodCost = FoodCost - 1;
            //Mulch = Mulch - 10;
            Spores = Spores - 3;
        }
        else if (i == 6)
        {
            UsedSpores = UsedSpores - 1;
            //Mulch = Mulch - 10;
            FoodEarning = FoodEarning - 2;
        }
        else if (i == 9)
        {
            UsedSpores = UsedSpores - 1;
            //Mulch = Mulch - 15;
            WoodEarning = WoodEarning - 1;
        }
        TextUpdate();


    }
}
