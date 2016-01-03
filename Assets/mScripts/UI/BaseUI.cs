using UnityEngine;
using System.Collections;

public abstract class BaseUI : MonoBehaviour {




	public abstract void Show();

    public abstract void OnMsg(MsgType msgType,object msg);

	public abstract void Close();

}
