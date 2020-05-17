using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Common
{
    public class DebugConsole : MonoBehaviour
    {
        public Vector2 ButtonSize = new Vector2(100, 40);
        public bool Enabled;
        public bool StackTrace;

        private bool _expand;
        private bool _subscribed;
        private Vector2 _scrollPos;
        private readonly StringBuilder _log = new StringBuilder();
        private readonly StringBuilder _fullLog = new StringBuilder();

        public static DebugConsole Instance;

        public void Awake()
        {
            Instance = this;
            Subscribe();
        }

        public void Subscribe()
        {
            if (!_subscribed)
            {
                Application.logMessageReceived += HandleLog;
                _subscribed = true;
            }
        }

        public void HandleLog(string message, string stackTrace, LogType type)
        {
            _log.AppendLine(message);

            if (StackTrace)
            {
                _log.AppendLine(stackTrace);
            }

            _fullLog.AppendLine(message);
            _fullLog.AppendLine(stackTrace);
        }

        public void OnGUI()
        {
            if (!Enabled) return;

            GUILayout.BeginArea(new Rect(0, 0, ButtonSize.x, ButtonSize.y));

            if (GUILayout.Button("Console", GUILayout.Width(ButtonSize.x), GUILayout.Height(ButtonSize.y)))
            {
                Expand();
            }

            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(ButtonSize.x, 0, ButtonSize.x, ButtonSize.y));

            if (GUILayout.Button("Clear", GUILayout.Width(ButtonSize.x), GUILayout.Height(ButtonSize.y)))
            {
                _log.Clear();
            }

            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(2 * ButtonSize.x, 0, ButtonSize.x, ButtonSize.y));

            if (GUILayout.Button("Send", GUILayout.Width(ButtonSize.x), GUILayout.Height(ButtonSize.y)))
            {
                ReportBug();
            }

            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(3 * ButtonSize.x, 0, ButtonSize.x, ButtonSize.y));

            if (GUILayout.Button("Close", GUILayout.Width(ButtonSize.x), GUILayout.Height(ButtonSize.y)))
            {
                Enabled = false;
            }

            GUILayout.EndArea();

            if (_expand)
            {
                GUILayout.BeginArea(new Rect(0, ButtonSize.y, Screen.width, Screen.height - ButtonSize.y));
                _scrollPos = GUILayout.BeginScrollView(_scrollPos);
                GUILayout.TextField(_log.ToString());
                GUILayout.EndScrollView();
                GUILayout.EndArea();
            }
        }

        public string GetLogs()
        {
            return _fullLog.ToString();
        }

        private void Expand()
        {
            _expand = !_expand;

            foreach (var eventSystem in FindObjectsOfType<EventSystem>())
            {
                eventSystem.enabled = !_expand;
            }
        }

        private void ReportBug()
        {
            var newLine = System.Environment.NewLine;
            var logs = GetLogs().Replace(newLine + newLine, newLine);
            const int maxLength = 50000;

            if (logs.Length > maxLength)
            {
                logs = "<cut>" + logs.Substring(logs.Length - maxLength, maxLength);
            }

            var envInfo = $"Version={Application.version}\nDevice={SystemInfo.deviceModel}";
            var body = $"{envInfo}\nLogs={logs}\nMessage=Please tell us what happened...";

            const string supportEmail = "hippogamesunity@gmail.com";
            const string emailSubject = "Pixel Studio Issue";
            var url = $"mailto:{supportEmail}?subject={Escape(emailSubject)}&body={Escape(body)}";

            Application.OpenURL(url);
        }

        private static string Escape(string value)
        {
            return WWW.EscapeURL(value).Replace("+", "%20");
        }
    }
}