using System.Linq;
using JHI.Dict.Providers;
using JHI.Dict.Serialization;
using JHI.Dict.Services;
using JHI.Dict.UI;
using JHI.Dict.Utils;
using UnityEngine;

namespace JHI.Dict
{
    public class Starter : MonoBehaviour
    {
        private WordSerializer _wordSerializer;
        private AllWordsProvider _allWordsProvider;
        
        private async void Awake()
        {
            _wordSerializer = new WordSerializer();
            var wordsJson = PlayerPrefsDataStorage.GetString(Consts.WORDS_PREFS_KEY);
            var words = _wordSerializer.Deserialize(wordsJson);

            _allWordsProvider = new AllWordsProvider(words);
            ServiceLocator.RegisterService<IAddingWordService>(_allWordsProvider);

            var unexploredWordsProvider = new UnexploredWordsProvider(_allWordsProvider);
            ServiceLocator.RegisterService(unexploredWordsProvider);
            var exploredWordsProvider = new ExploredWordsProvider(_allWordsProvider);
            ServiceLocator.RegisterService(exploredWordsProvider);

            await SceneLoader.Load(Consts.MAIN_SCENE_PATH);

            var windowService = FindFirstObjectByType<WindowService>();
            ServiceLocator.RegisterService<IWindowService>(windowService);
            windowService.OpenWindow<MainWindow>();
        }

        private void OnDestroy()
        {
            var wordsJson = _wordSerializer.Serialize(_allWordsProvider.GetWords().ToList());
            PlayerPrefsDataStorage.SaveString(Consts.WORDS_PREFS_KEY, wordsJson);
#if UNITY_EDITOR
            EditorJsonFileSaver.SaveTestFile(wordsJson);
#endif
        }
    }
}