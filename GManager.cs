using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GManager : MonoBehaviour
{
    public Text speedText;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        GameObject Kart = GameObject.Find("Kart");
        KartController controller = Kart.GetComponent<KartController>();
        speed = controller.currentSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        GameObject Kart = GameObject.Find("Kart");
        KartController controller = Kart.GetComponent<KartController>();
        speed = controller.currentSpeed;

        speedText.text = speed.ToString("F0");
    }

}
