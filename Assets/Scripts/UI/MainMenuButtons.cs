using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    enum buttonType { Play, Select, Quit};

    BoxCollider buttonCollider;
    [SerializeField] buttonType thisButton;
    [SerializeField] GameObject[] skulls;
    [SerializeField] GameObject levelSelect;

    bool inSelectLevel = false;

    private void Start()
    {
        skulls[0].SetActive(false);
        skulls[1].SetActive(false);

    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (!levelSelect.activeInHierarchy)
        {
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
                                SceneManager.LoadScene(1);
                                break;
                            case buttonType.Select:
                                levelSelect.SetActive(true);
                                inSelectLevel = true;
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
    public void ExitLevelSelect()
    {
        levelSelect.SetActive(false);
        inSelectLevel = false;
    }

}
