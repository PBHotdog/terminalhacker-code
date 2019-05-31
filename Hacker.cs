using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour
{
    // Start is called before the first frame update
    //Game config data
    string[] level1pass = { "wallet", "phone", "gum", "book", "water" };
    string[] level2pass = { "computer", "monitor", "ethernet", "microphone", "headset" };
    string[] level3pass = { "basketball", "controller", "thunderstorm", "skeleton", "firefighter" };
    const string menuHint = "Type menu to return to the menu";
    //Game State
    int level;
    string password;
    enum Screen { MainMenu, Password, Game, Win, Close }
    Screen currentScreen;
    void Start()
    {
        ShowMainMenu();
    }
    //Shows the main menu
    void ShowMainMenu()
    {
        Screen currentScreen = Screen.MainMenu;
        //Start of game terminal
        Terminal.WriteLine("Help me solve these encryptions");
        Terminal.WriteLine("");
        //difficulty selection
        Terminal.WriteLine("Press 1 for Easy");
        Terminal.WriteLine("Press 2 for Medium");
        Terminal.WriteLine("Press 3 for Hard");
        Terminal.WriteLine("");
        //Ending selection statement
        Terminal.WriteLine("Enter your selection here");
        Terminal.WriteLine("Oh... and good luck");
    }
    void OnUserInput(string input)
    {
        if (input == "menu") //can always return to main menu
        {
            Terminal.ClearScreen();
            level = 0;
            ShowMainMenu();
            currentScreen = Screen.MainMenu;
            string password = "";
        } //TODO handle differently depending on screen
        else if (input == "quit" || input == "close" || input == "stop")
        {
            Terminal.WriteLine("If on the web, close the tab");
            Application.Quit();
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password)
        {
            StartPass(input);
        }
    }

    void AskForPassword()
    {
        int index;
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
        SetRandomPassword();
        Terminal.WriteLine("Enter Password, hint: " + password.Anagram());
        Terminal.WriteLine("");
    }
    void SetRandomPassword()
    {
        switch (level)
        {
            case 1:
                password = level1pass[Random.Range(0, level1pass.Length)];
                break;
            case 2:
                password = level2pass[Random.Range(0, level2pass.Length)];
                break;
            case 3:
                password = level3pass[Random.Range(0, level3pass.Length)];
                break;
            default:
                Debug.LogError("invalid level number");
                break;
        }
    }
    void RunMainMenu(string input)
    {
        bool isValidLevelNumber = (input == "1" || input == "2" || input == "3");
        if (isValidLevelNumber)
        {
            level = int.Parse(input);
            AskForPassword();
        }
        else
        {
            Terminal.WriteLine("Incorrect input, try again");
            Terminal.WriteLine(menuHint);
        }
    }
    void StartPass(string input)
    {
        currentScreen = Screen.Game;
        PassCheck(input);
    }
    void PassCheck(string input)
    {
        if(input == password)
        {
            DisplayWinScreen();
        }
        else
        {
            incorrectPass();
        }
    }
    void incorrectPass()
    {
        Terminal.WriteLine("Error: Password incorrect. Try again.");
        currentScreen = Screen.Password;
    }
    void DisplayWinScreen()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
        Terminal.WriteLine(menuHint);
    }
    void ShowLevelReward()
    {
        switch(level)
        {
            case 1:
                Terminal.WriteLine("Congrats, easy mode has been achieved");
                Terminal.WriteLine(@"
O     O
   ^
/-----\ ");

                break;
            case 2:
                Terminal.WriteLine("Congrats, medium mode has been achieved");
                Terminal.WriteLine(@"
O     O
   ^
------- ");

                break;
            case 3:
                Terminal.WriteLine("Congrats, hard mode has been achieved");
                Terminal.WriteLine(@"
O     O
   ^
\-----/ ");
                break;
            default:
                Debug.LogError("How did we get here?");
                break;
        }
    }
}

