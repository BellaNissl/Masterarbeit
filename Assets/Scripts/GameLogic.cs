using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    public enum Tiletype
    {
        city,
        farm, 
        wood, 
        field
    };

    public enum Event
    {
        wind,
        sun,
        bad_wind,
        bad_sun,
        none
    };

public class GameLogic : MonoBehaviour
{
    public static GameLogic Instance { get; private set; }

    // game constants
    public const int YEARS = 10;
    public const int SEASONS = 4;
    public const int MAX_VALUE = 100;


    // current game timeline
    [SerializeField] private int _year;
    [SerializeField] private int _season;

    // values of resources
    [SerializeField] private int _energy;
    [SerializeField] private int _biodiversity;
    [SerializeField] private int _happiness;
    [SerializeField] private int _money;
    [SerializeField] private GameObject _energy_bar;
    [SerializeField] private GameObject _biodiversity_bar;
    [SerializeField] private GameObject _happiness_bar;
    [SerializeField] private GameObject _money_bar;

    // values of energy
    [SerializeField] private int _pvEnergy;
    [SerializeField] private int _solarEnergy;

    // buttons
    public Button _pv_button;
    public Button _wind_button;


    // set ressource values at start
    void Start(){
        _energy = MAX_VALUE / 2;
        _biodiversity = MAX_VALUE;
        _money = MAX_VALUE;
        _happiness = MAX_VALUE;
        UpdateResources(0, 0, 0, 0);
    }

    private void Awake(){
        Instance = this;
    }

    // end current year
    private void EndYear() {
        if(_year < YEARS) {
            _season = 0;
            _year++;
        } else {
            // end game
        }
    }

    // end current season
    private void EndSeason() {
        if(_season < SEASONS) {
            _season++;
        } else {
            EndYear();
        }
    }

    // display message to players
    public void DisplayMessage(string message) {

    }

    // update ressource values
    public void UpdateResources(int energy, int biodiversity, int happiness, int money) {
        _energy += energy;
        _biodiversity += biodiversity;
        _happiness += happiness;
        _money += money;
        UpdateBars();

        if(_energy <= 0 || _biodiversity <= 0 || _happiness <= 0 || _money <= 0 ) {
            GameOver();
        }
    }

    // update ressource bars
    private void UpdateBars()
    {
        _energy_bar.GetComponent<Bar>().SetValue(_energy);
        _biodiversity_bar.GetComponent<Bar>().SetValue(_biodiversity);
        _happiness_bar.GetComponent<Bar>().SetValue(_happiness);
        _money_bar.GetComponent<Bar>().SetValue(_money);
    }

    private void GameOver() {
        Debug.Log("Game Over");
    }
}
