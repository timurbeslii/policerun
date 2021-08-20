using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private float groundTileLength;
    [SerializeField] private GameObject groundTilePrefab;
    [SerializeField] private GameObject[] obstacleTilePrefabs;
    [SerializeField] private Transform groundParent;
    [SerializeField] private GameObject finalTile;
    [SerializeField] private GameObject prisonTile;
    [SerializeField] private MeshRenderer verticalFogMaterial;
    [SerializeField] private GameObject[] planeBG;
    public int finalTileCount;

    public int groundTileCount;
    public bool isDroneVariation;
    public bool isMonkeyVariation;
    private int specialTileCooldown_temp;
    public int specialTileCooldown;
    private int levelIndex;

    public SkinnedMeshRenderer policeHairMR;
    
    public SkinnedMeshRenderer policeUniformMR;

    public SkinnedMeshRenderer policeBootMR;


    


    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        levelIndex = PlayerPrefs.GetInt("levelIndex");
        if (levelIndex >= GameManager.gm.levels.Length-1)
        {
            levelIndex = Random.Range(5, GameManager.gm.levels.Length-1);
        }
        specialTileCooldown_temp = specialTileCooldown;

        yield return new WaitForEndOfFrame();
        GenerateGround();
        AdjustColors();
    }
  

    private void AdjustColors()
    {
        GameManager.gm.levels[levelIndex].Environment.SetActive(true);

        policeBootMR.sharedMaterial = GameManager.gm.levels[levelIndex].policeBootMat;
        policeHairMR.sharedMaterial = GameManager.gm.levels[levelIndex].policeHairMat;
        policeUniformMR.sharedMaterial = GameManager.gm.levels[levelIndex].policeUniformMat;
        

        RenderSettings.fogColor = GameManager.gm.levels[levelIndex].fogColor;
       // RenderSettings.skybox = GameManager.gm.levels[levelIndex].skyBoxMaterial;
        verticalFogMaterial.sharedMaterial = GameManager.gm.levels[levelIndex].verticalFogMaterial;
        GameManager.gm.levels[levelIndex].planeBG.SetActive(true);

    }
    private void GenerateGround()
    {
        isDroneVariation = GameManager.gm.levels[levelIndex].isDroneTileAvailable;
        isMonkeyVariation = GameManager.gm.levels[levelIndex].isMonkeyTileAvailable;

        Vector3 spawnPos = new Vector3();
        spawnPos=Vector3.zero;
        
        for (int i = 0; i < GameManager.gm.levels[levelIndex].levelTileLength; i++)
        {
          
            if (Random.Range(0, 1) == 0 &&  i > 2)
            {
                GameObject tile = obstacleTilePrefabs[Random.Range(0, obstacleTilePrefabs.Length)];
               
               
                if ((tile.CompareTag("AirTile") || tile.CompareTag("DroneTile")))
                {
                    
                    if ((tile.CompareTag("AirTile") && isMonkeyVariation) || (tile.CompareTag("DroneTile") && isDroneVariation))
                    {

                        if (specialTileCooldown_temp == 0)
                        {
                            specialTileCooldown_temp = specialTileCooldown;
                            Instantiate(tile, spawnPos, groundTilePrefab.transform.rotation, groundParent);
                            spawnPos.z += groundTileLength * 2;
                        }
                     
                    }
                   
                }
                else
                {

                    if (specialTileCooldown_temp > 0)
                    {
                        specialTileCooldown_temp--;
                    }
                    Instantiate(tile, spawnPos, groundTilePrefab.transform.rotation, groundParent);
                    spawnPos.z += groundTileLength;
                  
                }
                
            }
            else
            {
                if (specialTileCooldown_temp > 0)
                {
                    specialTileCooldown_temp--;
                }
                Instantiate(groundTilePrefab, spawnPos, groundTilePrefab.transform.rotation, groundParent);
                spawnPos.z += groundTileLength;
               
            }

        }


        //spawnPos.z -= groundTileLength;
        for (int i =0; i < GameManager.gm.levels[levelIndex].levelEndTileLength ; i++)
        {
          
        
            GameObject tile=Instantiate(finalTile, spawnPos, groundTilePrefab.transform.rotation, groundParent);
            spawnPos.z += groundTileLength;
            tile.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = (i + 1) + "X";

            if (i == 0)
            {
                tile.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            }
        }

         Instantiate(prisonTile, spawnPos, groundTilePrefab.transform.rotation, groundParent);
    }
   

   
}
