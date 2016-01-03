using UnityEngine;
using System.Collections;

public class BlackUI : BaseUI {


	public override void Show ()
	{
		gameObject.SetActive(true);
	}

	public override void Close ()
	{
		gameObject.SetActive(false);
	}

	public override void OnMsg (MsgType msgType, object msg)
	{

	}

}
