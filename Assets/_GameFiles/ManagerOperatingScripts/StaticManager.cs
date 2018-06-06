using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos {
	public enum GameState {
		mainMenu,
		mainGame,
		pause,
	}

	public enum AppState {
		running,
		loading
	}

    public enum LimitType
    {
        hard,
        screen,
        highlight
    }

    public enum Dir : int
    {
        up,
        right,
        down,
        left
    }


    public class StaticManager
    {
        public static AppManager appManager;
        public static InputManager inputManager;
        public static GameManager gameManager;
        public static AudioManager audioManager;
    }

}
