package com.lcqp.lcqp.wxapi;

import com.tencent.mm.opensdk.openapi.IWXAPI;

import android.app.Application;
import android.os.Bundle;

public class MyApplication extends Application  {
	public static IWXAPI WApi;
	
	public void onCreate ()
	{
		super.onCreate();
		WApi = WXEntryActivity.Init(this, AppConst.WechatApp_ID);
	}
}
