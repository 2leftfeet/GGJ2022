using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    enum buttonType { Play, Select, Quit};

    BoxCollider buttonCollider;
    [SerializeField] buttonType thisButton;
    [SerializeField] GameObject[] skulls;

    private void Start()
    {
        skulls[0].SetActive(false);
        skulls[1].SetActive(false);
    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            

            if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == this.transform)
            {
                skulls[0].SetActive(true);
                skulls[1].SetActive(true);
                if (Input.GetMouseButtonDown(0))
                {
                    switch (thisButton)
                    {
                        case buttonType.Play:
                            Debug.Log("Click Play");
                            //start 1st level
                            break;
                        case buttonType.Select:
                            Debug.Log("Click Select");
                            //lvl select screen
                            break;
                        case buttonType.Quit:
                            Debug.Log("Click Quit");
                            Application.Quit();
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                skulls[0].SetActive(false);
                skulls[1].SetActive(false);
            }
        }
        else
        {
            skulls[0].SetActive(false);
            skulls[1].SetActive(false);
        }
    }

}
