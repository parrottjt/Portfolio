using System;
using System.Collections;
using System.Collections.Generic;
using FrikinCore.AI.Enemy;
using FrikinCore.Input;
using FrikinCore.Player;
using FrikinCore.Sfx;
using FrikinCore.Utils;
using TMPro;
using UICore;
using UICore.SharkFact;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace FrikinCore.ScenesManagement
{
    public class SceneTransitionController : MonoBehaviour
    {
        Animator trannyAnim;

        [SerializeField] bool overrideNextLevelLoad;

        [DrawIf("overrideNextLevelLoad", true)]
        public string levelToLoadNext;

        [SerializeField] GameObject loadingScreenPanel,
            progressText,
            sharkFacts,
            extraLifeUI,
            loadingTvParts,
            staticScreen,
            sceneTrannyCanvasFade;

        [SerializeField] Slider loadingSlider;

        bool hasSharkFactsAppeared, sharksFirstFact;
        float sharkTimer, staticScreenTimer, fakeLoadBarTimer;
        float fakeLoadBarSlider;
        bool loadFinished, staticCheck;
        double fakeLoadPercent;

        //Lets move shark facts out to have potential to use them in other locations
        SharkFact[] facts;
        List<string> factList = new List<string>();

        private void Start()
        {
            trannyAnim = sceneTrannyCanvasFade.GetComponent<Animator>();
        }

        public void OverrideNextLevelLoad() => overrideNextLevelLoad = true;

        public void TrannyFade()
        {
            trannyAnim.SetBool("end", true);
            Invoke(nameof(TimeStop), 1f);
        }

        public void LoadScene()
        {
            if (!overrideNextLevelLoad) LoadNextScene();
            else
            {
                LoadSpecificScene(levelToLoadNext);
                overrideNextLevelLoad = false;
            }
        }

        public void LoadNextScene()
        {
            StartCoroutine(LoadAsynchronously_Update(!GameManager.GameStatesDictionary[GameStates.BossRush]
                ? SceneManagement.GetNextLevelToLoad()
                : GameManager.instance.bossRushManager.NextLevelName()));
        }

        public void LoadSpecificScene(string levelName)
        {
            StartCoroutine(LoadAsynchronously_Update(levelName));
        }

        public void SetLevelToLoadNext_BossRush()
        {
            if (GameManager.GameStatesDictionary[GameStates.BossRush])
            {
                levelToLoadNext = GameManager.instance.bossRushManager.NextLevelName();
            }
        }

        private IEnumerator LoadAsynchronously(string scene)
        {
            yield return null;
            int sceneInt;
            var isNumber = int.TryParse(scene, out sceneInt);
            var coroutineName = "LoadAsynchronously";
            if (isNumber)
            {
                if (GameManager.GameStatesDictionary[GameStates.Menu])
                    sceneInt = GameManager.instance.menuSceneInt;
                if (GameManager.GameStatesDictionary[GameStates.Story])
                    sceneInt = GameManager.instance.storySceneInt + 3;
                if (GameManager.instance.inProGen)
                    sceneInt = GameManager.instance.progenSceneInt + 36;
            }

            AsyncOperation operation =
                isNumber ? SceneManager.LoadSceneAsync(sceneInt) : SceneManager.LoadSceneAsync(scene);
            sharksFirstFact = true;
            loadingScreenPanel.SetActive(true);
            SoundManager.instance.PlayBackgroundMusic(SoundManager.instance.menu_MainTheme_Bgm.clip);
            operation.allowSceneActivation = false;
            while (!operation.isDone)
            {
                SharkFactsAndLoadingBarProtocol(operation, coroutineName);
                yield return null;
            }
        }

        //This is the new version that connects to SceneManagement
        private IEnumerator LoadAsynchronously_Update(string scene)
        {
            yield return null;
            var coroutineName = nameof(LoadAsynchronously_Update);

            AsyncOperation operation = SceneManagement.LoadLevel(scene);
            sharksFirstFact = true;
            loadingScreenPanel.SetActive(true);
            SoundManager.instance.PlayBackgroundMusic(SoundManager.instance.menu_MainTheme_Bgm.clip);
            operation.allowSceneActivation = false;
            while (!operation.isDone)
            {
                SharkFactsAndLoadingBarProtocol(operation, coroutineName);
                yield return null;
            }
        }

        private void SharkFactsAndLoadingBarProtocol(AsyncOperation operation, string coroutineName)
        {
            float progress = (float)Math.Round(Mathf.Clamp01(fakeLoadBarSlider), 2);
            loadingSlider.value = progress;
            progressText.GetComponent<TextMeshProUGUI>().text = "Loading " + (progress * 100f) + "%" + " Completed";
            Debug.Log(operation.progress * 100f + "%");

            if (operation.progress >= 0f && sharksFirstFact)
            {
                Handlers.SetActiveOnGameObjectsTo(false, sceneTrannyCanvasFade,
                    UIManager.instance.InfoHolder.upperLeftCorner, UIManager.instance.InfoHolder.bottomLeftCorner);
                SharkFactsText();
                sharksFirstFact = false;
                TimeStop();
            }

            if (UnityEngine.Input.anyKeyDown) SharkFactsText();
            if (operation.progress >= .9f && loadFinished)
            {
                if (!InputManager.instance.ControllerInUse)
                {
                    progressText.GetComponent<TextMeshProUGUI>().text = "Press the space bar to continue";

                    if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
                    {
                        SceneTransitionEnd(operation, coroutineName);
                    }
                }
                else
                {
                    progressText.GetComponent<TextMeshProUGUI>().text = "Press the A button to continue";

                    if (UnityEngine.Input.GetKeyDown(KeyCode.JoystickButton0))
                    {
                        SceneTransitionEnd(operation, coroutineName);
                    }
                }
            }
        }

        void SceneTransitionEnd(AsyncOperation operation, string loadAsyncName)
        {
            operation.allowSceneActivation = true;
            loadingScreenPanel.SetActive(false);
            Handlers.SetActiveOnGameObjectsTo(true, sceneTrannyCanvasFade, extraLifeUI,
                UIManager.instance.InfoHolder.upperLeftCorner, UIManager.instance.InfoHolder.bottomLeftCorner);

            hasSharkFactsAppeared = false;
            loadFinished = false;
            fakeLoadBarSlider = 0;
            GameManager.instance.currentScene = "";

            StopCoroutine(loadAsyncName);
        }

        //Can this be abstracted out and put into the Async Load
        private void Update()
        {
            if (hasSharkFactsAppeared)
            {
                fakeLoadPercent = Math.Round(Random.Range(.15f, .35f), 2);
                GameManager.GameStatesDictionary[GameStates.Menu] = true;
                extraLifeUI.SetActive(false);
                if (GameManager.GameSettings[Settings.IsBossScene])
                    UIManager.instance.InfoHolder.bossHealthBar.SetActive(false);
                sharkTimer += Time.unscaledDeltaTime;
                fakeLoadBarTimer += Time.unscaledDeltaTime;
                if (staticCheck)
                {
                    staticScreenTimer += Time.unscaledDeltaTime;

                    if (staticScreenTimer >= .2)
                    {
                        staticScreen.SetActive(false);
                        loadingTvParts.SetActive(true);
                        staticScreenTimer = 0;
                        staticCheck = false;
                    }
                }

                if (sharkTimer >= 6)
                {
                    SharkFactsText();
                    sharkTimer = 0;
                }

                if (fakeLoadBarTimer >= 1)
                {
                    fakeLoadBarSlider += (float)fakeLoadPercent;
                    fakeLoadBarTimer = 0;
                }
            }

            if (sceneTrannyCanvasFade == null)
                sceneTrannyCanvasFade = GameObject.Find("fade");
            if (fakeLoadBarSlider >= .99)
                loadFinished = true;
        }

        public void TimeStop()
        {
            foreach (var enemy in FindObjectsOfType<DataEnemy>())
            {
                enemy.pawnHolder.SetActive(false);
            }

            PlayerManager.instance.Player.gameObject.SetActive(false);
        }

        public void SharkFactsText()
        {
            staticScreen.SetActive(true);
            loadingTvParts.SetActive(false);
            staticCheck = true;
            hasSharkFactsAppeared = true;
            sharkTimer = 0;
            sharkFacts.GetComponent<TextMeshProUGUI>().text = SharkFactController.GetRandomSharkFact();
        }
    }
}