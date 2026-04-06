using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Zenject;

namespace Core.Services
{
    public class GameBootstrap
    {
        [Inject]
        public GameBootstrap(DiContainer container)
        {
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
