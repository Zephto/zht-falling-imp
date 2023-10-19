using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectSpawner : MonoBehaviour {
    public GameObject objectPrefab;  // Lista de objetos prefabricados
    public float spawnInterval = 2.0f;    // Intervalo de generación
    public float spawnDelay = 2.0f;       // Retraso inicial

    private List<GameObject> objects;      // Lista de objetos generados
    private float timer;
    private Camera mainCamera;

    private void Start()
    {
        objects = new List<GameObject>();
        timer = spawnDelay;
        mainCamera = Camera.main;

        //Move Spawner to bottom of the screen
        var screenPosition  = new Vector3(Screen.width/2, 0, mainCamera.nearClipPlane);
        var worldPosition   = mainCamera.ScreenToWorldPoint(screenPosition);

        //Set world position to current object
        this.transform.position = worldPosition;
    }

    private void Update() {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnObject();
            timer = spawnInterval;
        }
    }

    private void SpawnObject() {
        var spawnX      = Random.Range(0.2f, 0.8f); // Rango de 0 a 1 en coordenadas de pantalla
        var errorRange  = 5;

        Vector3 spawnPosition = mainCamera.ViewportToWorldPoint(new Vector3(spawnX, 1, 0));
        spawnPosition.y = this.transform.position.y - errorRange;
        spawnPosition.z = 0;

        if(objects.Count > 10){
            //Pool existing objects

            var inactiveObjects = objects.Where(obj => !obj.activeSelf).ToList();
            if(inactiveObjects.Count > 0){

                var randomValue = Random.Range(0, inactiveObjects.Count);
                var selection   = inactiveObjects[randomValue];

                selection.transform.position = spawnPosition;
                selection.SetActive(true);

            }else{
                Debug.LogError("There is no more active objects!!!");
            }


        }else{
            //Create new objects
            GameObject obj = Instantiate(objectPrefab, spawnPosition, Quaternion.identity, this.transform);
            objects.Add(obj);
        }

    }

	// Función pública para ajustar la velocidad de generación
    public void SetSpawnInterval(float newInterval)
    {
        spawnInterval = newInterval;
    }
}






