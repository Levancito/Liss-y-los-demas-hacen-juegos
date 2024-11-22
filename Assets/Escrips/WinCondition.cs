using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    private int winCounter;
    private int totalEnemies;
    private SceneController sceneController;

    private void Start()
    {
        sceneController = FindObjectOfType<SceneController>();

        Ship[] allShips = FindObjectsOfType<Ship>(true);
        Plane[] allPlanes = FindObjectsOfType<Plane>(true);

        //totalEnemies = 0;
        totalEnemies = 33;
        //int activeShipsCount = 0;
        //foreach (Ship ship in allShips)
        //{
        //    if (ship.gameObject.activeInHierarchy)
        //    {
        //        activeShipsCount++;
        //    }
        //}
        //int activePlanesCount = 0;
        //foreach (Plane plane in allPlanes)
        //{
        //    if (plane.gameObject.activeInHierarchy)
        //    {
        //        activePlanesCount++;
        //    }
        //}
        //totalEnemies = activeShipsCount + activePlanesCount;
    }

    public void WinAdd()
    {
        winCounter++;
        Debug.Log($"Enemigos derrotados: {winCounter}/{totalEnemies}");

        if (winCounter >= totalEnemies)
        {
            Debug.Log("¡Victoria!");
            sceneController.ReloadCurrentScene(); 

        }
    }
}