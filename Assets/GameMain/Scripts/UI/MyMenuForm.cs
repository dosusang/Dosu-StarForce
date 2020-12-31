
using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce {
    //GF UIForm模块Test
    //notice ui 打开调用 GameEntry.UI.OpenUIForm(UIFormId.MyMenuForm, this); 将ui的设置做成配置，底层去调用
    //  m_UIManager.OpenUIForm(uiFormAssetName, uiGroupName, priority, pauseCoveredUIForm, userData);
    //notice 按钮的文字text文字k 以xml形式做成配置key是prefab上text组件的文字value是需要显示的文字 运行时替换，
    //notice 在uiform上存当前的流程Procedure，可以交互ui的时候控制流程
    //
    //
    public class MyMenuForm : UGuiForm {
        private ProcedureMenu m_ProcedureMenu;
        //private GameObject m_QuitButton;

        // Start is called before the first frame update
        public void OnStartButtonClick() {
            m_ProcedureMenu.StartGame();
        }

        public void OnQuitButtonClick() {
            GameEntry.UI.OpenDialog(new DialogParams() {
                Mode = 2,
                Title = GameEntry.Localization.GetString("AskQuitGame.Title"),
                Message = GameEntry.Localization.GetString("AskQuitGame.Message"),
                OnClickConfirm = delegate (object userData) { UnityGameFramework.Runtime.GameEntry.Shutdown(ShutdownType.Quit); },
            });
        }

        protected override void OnOpen(object userData) {
            base.OnOpen(userData);
            m_ProcedureMenu = (ProcedureMenu)userData;
            if (m_ProcedureMenu == null) {
                Log.Warning("ProcedureMenu is invalid when open MenuForm.");
                return;
            }

            //m_QuitButton.SetActive(Application.platform != RuntimePlatform.IPhonePlayer);
        }
        protected override void OnClose(bool isShutdown, object userData) {
            base.OnClose(isShutdown, userData);
            m_ProcedureMenu = null;
        }

    }
}

