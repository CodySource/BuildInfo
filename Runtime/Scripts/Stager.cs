using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.Compilation;
#endif

namespace CodySource
{
    namespace BuildInfo
    { 
        #if UNITY_EDITOR
        [ExecuteAlways()]
        public class Stager : MonoBehaviour, IPostprocessBuildWithReport
        {

            #region PROPERTIES

            public int callbackOrder => 0;

            #endregion

            #region PUBLIC METHODS

        #if IS_STAGING
                [MenuItem("(Staging Build)/Switch To Live")]
                public static void Live() => Switch("IS_STAGING","IS_LIVE");
        #else
            [MenuItem("(Live Build)/Switch To Staging")]
                public static void Staging() => Switch("IS_LIVE","IS_STAGING");
        #endif

            /// <summary>
            /// Update the build version of the project
            /// </summary>
            public void OnPostprocessBuild(BuildReport report) => Switch("IS_LIVE", "IS_STAGING");

            #endregion

            #region PRIVATE METHODS

            /// <summary>
            /// Switches to the desired platform
            /// </summary>
            private static void Switch(string pOld, string pNew)
            {
        #if IS_STAGING
                if (pNew == "IS_STAGING") return;
        #else
                if (pNew == "IS_LIVE") return;
        #endif
                PlayerSettings.SetScriptingDefineSymbols(NamedBuildTarget.WebGL, PlayerSettings.GetScriptingDefineSymbols(NamedBuildTarget.WebGL).Replace(pOld, pNew));
                //  Recompile
                CompilationPipeline.RequestScriptCompilation();

            }

            private void Update()
            {
                string symbols = PlayerSettings.GetScriptingDefineSymbols(NamedBuildTarget.WebGL);
                if (!symbols.Contains("IS_STAGING") && !symbols.Contains("IS_LIVE"))
                {
                    symbols += ";IS_STAGING";
                    symbols = symbols.StartsWith(";") ? symbols.Substring(1) : symbols;
                    PlayerSettings.SetScriptingDefineSymbols(NamedBuildTarget.WebGL, symbols);
                    CompilationPipeline.RequestScriptCompilation();
                }
            }

            #endregion

        }
        #else
        public class Stager : MonoBehaviour {}
        #endif
    }
}