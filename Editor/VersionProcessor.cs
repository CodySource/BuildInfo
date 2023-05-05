using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
#endif

namespace CodySource
{
    namespace BuildInfo
    {
        namespace Editor
        {
#if UNITY_EDITOR
            public class VersionProcessor : IPreprocessBuildWithReport
            {

                #region PROPERTIES

                private const string defaultVersion = "0.0.0";
                public int callbackOrder => 0;

                #endregion

                #region PUBLIC METHODS

                /// <summary>
                /// Update the build version of the project
                /// </summary>
                public void OnPreprocessBuild(BuildReport report)
                {
                    string[] initialVersion = PlayerSettings.bundleVersion.Split('[', ']');
                    string version = initialVersion.Length == 1 ? defaultVersion : initialVersion[1];
                    if (int.TryParse(version.Split('.')[2], out int number))
                    {
                        int newVersion = number + 1;
                        string date = System.DateTime.Now.ToString("d");
                        PlayerSettings.bundleVersion = string.Format("[{0}.{1}.{2}]-{3}", version.Split('.')[0], version.Split('.')[1], newVersion, date);
                    }
                }

                #endregion
            }
#else
            public class VersionProcessor {}
#endif
        }
    }
}