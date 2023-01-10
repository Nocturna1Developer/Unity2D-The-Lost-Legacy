// /***************************************************************************************
//  * Authors: Chinamay K, 
//  *
//  * Credits: 
//  *
//  * Description: Works with the Photon API and allows the player to change scenes. If the host changes 
//  scenes then they can bring the rest of the players with them.
// ***************************************************************************************/
// //using Pun;
// using TMPro;
// using UnityEngine;
// using UnityEngine.UI;
// using System.Collections;
// using UnityEngine.Networking;
// using System.Collections.Generic;
// using UnityEngine.SceneManagement;

// public class SceneManager : MonoBehaviour
// {
//     private int  _mainMenuBuildIndex = 0;
//     private int  _multiplayerMenuBuildIndex = 1;
//     private int  _earthLevelBuildIndex = 2;
//     private int  _stationLevelBuildIndex = 3;
//     private int  _spaceLevelBuildIndex = 4;
//     private int  _venusLevelBuildIndex = 5;
//     private int  _moonLevelBuildIndex = 6;
//     private int  _marsLevelBuildIndex = 7;
//     private int  _futureMarsLevelBuildIndex = 8;
//     private int  _europaLevelBuildIndex = 9;
//     private int  _titanLevelBuildIndex = 10;
//     private int  _creditsLevelBuildIndex = 10;

//     [SerializeField] private RectTransform _faderImage;

//     private void Start()
//     {
//         _faderImage.gameObject.SetActive(true);
//         LeanTween.alpha(_faderImage, 1, 0);
//         LeanTween.alpha(_faderImage, 0, 2f).setOnComplete(() => {
//             _faderImage.gameObject.SetActive(false);
//         });
//     }

//     public void LoadNextLevel()
//     {
//         _faderImage.gameObject.SetActive(true);
//         LeanTween.alpha(_faderImage, 0, 0);
//         LeanTween.alpha(_faderImage, 1, 2f).setOnComplete(() => {
//             StartCoroutine(LoadNextlevelAfterDelay(2f));
//         });
//     }

//     public void LoadMainMenu()
//     { 
//         _faderImage.gameObject.SetActive(true);
//         LeanTween.alpha(_faderImage, 0, 0);
//         LeanTween.alpha(_faderImage, 1, 1f).setOnComplete(() => {
//             SceneManager.LoadScene(_mainMenuBuildIndex);
//         });
//     }

//     public void LoadSpaceLevel()
//     { 
//         _faderImage.gameObject.SetActive(true);
//         LeanTween.alpha(_faderImage, 0, 0);
//         LeanTween.alpha(_faderImage, 1, 1f).setOnComplete(() => {
//             SceneManager.LoadScene(_spaceLevelBuildIndex);
//         });
//     }

//     public void LoadEarthLevel()
//     { 
//         _faderImage.gameObject.SetActive(true);
//         LeanTween.alpha(_faderImage, 0, 0);
//         LeanTween.alpha(_faderImage, 1, 1f).setOnComplete(() => {
//             SceneManager.LoadScene(_earthLevelBuildIndex);
//         });
//     }

//     public void QuitApplication()
//     {
//         Application.Quit();
//     }

//     public IEnumerator LoadNextlevelAfterDelay(float waitTime)
//     {
//         yield return new WaitForSeconds(waitTime);
//         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
//     }
// }

// // public void GetJsonData()
// // {
// //     StartCoroutine(RequestWebService());
// // }

// // IEnumerator RequestWebService()
// // {
// //     string getDataUrl = "https://xrlecture.alter-learning.com/STEM_AlterLearningData.json";

// //     using (UnityWebRequest webData = UnityWebRequest.Get(getDataUrl))
// //     {
// //         yield return webData.SendWebRequest();
// //         if (webData.result != UnityWebRequest.Result.Success)
// //         {
// //             print(webData.error);
// //         }
// //         else
// //         {
// //             if (webData.isDone)
// //             {
// //                 JSONNode jsonData = JSON.Parse(System.Text.Encoding.UTF8.GetString(webData.downloadHandler.data));
// //                 if (jsonData == null)
// //                 {
// //                     print("THERE IS NO DATA");
// //                 }
// //                 else
// //                 {
// //                     string nameOfPlanet = jsonData["planetName"];
// //                     if (nameOfPlanet == "Venus")
// //                     {
// //                         SceneManager.LoadScene(1);
// //                     }
// //                     else if (nameOfPlanet == "Moon")
// //                     {
// //                         SceneManager.LoadScene(2);
// //                     }
// //                     else if (nameOfPlanet == "Mars")
// //                     {
// //                         SceneManager.LoadScene(3);
// //                     }
// //                 }
// //             }
// //         }
// //     }
// // }