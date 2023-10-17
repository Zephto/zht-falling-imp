using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public List<GameObject> objectPrefabs;  // Lista de objetos prefabricados
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
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnObject();
            timer = spawnInterval;
        }
    }

    private void SpawnObject()
    {
        int randomIndex = Random.Range(0, objectPrefabs.Count);

        float spawnX = Random.Range(0f, 1f); // Rango de 0 a 1 en coordenadas de pantalla

        Vector3 spawnPosition = mainCamera.ViewportToWorldPoint(new Vector3(spawnX, 1, 0));
		spawnPosition.y = this.transform.position.y;
        spawnPosition.z = 0;

        GameObject obj = Instantiate(objectPrefabs[randomIndex], spawnPosition, Quaternion.identity, this.transform);
        objects.Add(obj);
    }

	// Función pública para ajustar la velocidad de generación
    public void SetSpawnInterval(float newInterval)
    {
        spawnInterval = newInterval;
    }
}






