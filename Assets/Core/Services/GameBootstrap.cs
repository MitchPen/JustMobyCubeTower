using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Core.Services
{
    public class GameBootstrap
    {
        [Inject]
        public GameBootstrap(DiContainer container)
        {
            Application.targetFrameRate = 60;
            ServiceInstaller.Install(container);
            LoadGameScene().Forget();
        }
       
        private async UniTaskVoid LoadGameScene()
        {
            await UniTask.WaitForFixedUpdate();
            SceneManager.LoadScene((int)SceneTypes.MAIN, LoadSceneMode.Single);
        }
    }
}
