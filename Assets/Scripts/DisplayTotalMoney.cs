using UnityEngine;
using UnityEngine.UI;

public class DisplayTotalMoney : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Text>().text = "$" + PlayerPrefs.GetInt("money", 0).ToString();
	}
	

}
