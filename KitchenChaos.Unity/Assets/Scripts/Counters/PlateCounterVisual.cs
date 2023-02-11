using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class PlateCounterVisual : MonoBehaviour
    {
        [SerializeField] private Transform counterTopPoint;
        [SerializeField] private PlateCounter plateCounter;

        [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

        private List<GameObject> spawnedPlates = new List<GameObject>();

        // Start is called before the first frame update
        void Start()
        {
            plateCounter.OnPlateSpawned += PlateCounter_OnPlateSpawned;
            plateCounter.OnPlateTaken += PlateCounter_OnPlateTaken;
        }

        private void PlateCounter_OnPlateTaken(object sender, System.EventArgs e)
        {
            int index = spawnedPlates.Count - 1;
            GameObject plate = spawnedPlates[index];
            spawnedPlates.RemoveAt(index);
            GameObject.Destroy(plate);
        }

        private void PlateCounter_OnPlateSpawned(object sender, System.EventArgs e)
        {

            GameObject plate = GameObject.Instantiate(original: plateKitchenObjectSO.Prefab, parent: counterTopPoint);

            float plateOffsetY = 0.1f;

            plate.transform.localPosition += new Vector3(0.0f, plateOffsetY * this.spawnedPlates.Count, 0.0f);
            spawnedPlates.Add(plate);
        }

    }
}
