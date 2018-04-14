using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

//TODO dependency injection?
public class GameManager : MonoBehaviour {

    private const string SAVESTATE = "SaveState";

    public static GameManager instance;

    private void Awake() {
        if (GameManager.instance != null) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }

    //Resources
    public List<Sprite> PlayerSprites;
    public List<Sprite> WeaponSprites;
    public List<int> WeaponPrices;
    public List<int> ExpTable;

    //References
    public FloatingTextManager FloatingTextManager;
    public Player Player;
    //public Weapon Weapon;

    //Logic
    public int PlayerSkin = 0;
    public int Pesos = 0;
    public int Exp = 0;
    public int WeaponLevel = 0;

    public void SaveState() {
        Debug.Log("Save");
        string save = "";

        save += PlayerSkin.ToString() + "|"; //PlayerSkin
        save += Pesos.ToString() + "|"; 
        save += Exp.ToString() + "|"; 
        save += WeaponLevel.ToString();
        PlayerPrefs.SetString(SAVESTATE, save);
    }

    public void LoadState(Scene scene, LoadSceneMode mode) {
        //TODO how to use an associative array? :/
        if (!PlayerPrefs.HasKey(SAVESTATE)) {
            return;
        }

        string[] data = PlayerPrefs.GetString(SAVESTATE).Split('|');

        PlayerSkin = int.Parse(data[0]);
        Pesos = int.Parse(data[1]);
        Exp = int.Parse(data[2]);
        WeaponLevel = int.Parse(data[3]);
        Debug.Log("Load");
    }

    public void ShowText(string message, int fontSize, Color color, Vector3 position, Vector3 motion, float duration) {
        FloatingTextManager.Show(message, fontSize, color, position, motion, duration);
    }
}
