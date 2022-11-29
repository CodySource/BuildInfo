using UnityEngine;
using TMPro;
using UnityEngine.Events;

namespace CodySource
{
    namespace BuildInfo
    {
        [ExecuteAlways()]
        public class BuildInfo : MonoBehaviour
        {

            #region PROPERTIES

            public TMP_Text Label;

            #endregion

            #region PUBLIC METHODS

            public void OnEnable() => Label.text = Version();

            public static string Version() => $"Version: {Application.version}";

            #endregion

        }
    }
}