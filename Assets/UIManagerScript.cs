using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManagerScript : MonoBehaviour {

	public void StartGame() {
		SceneManager.LoadScene ("Level 01");
	}
}
