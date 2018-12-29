package com.lcqp.lcqp.wxapi;

import com.tencent.mm.opensdk.modelbase.BaseReq;
import com.tencent.mm.opensdk.modelbase.BaseResp;
import com.tencent.mm.opensdk.modelmsg.SendAuth;
import com.tencent.mm.opensdk.openapi.IWXAPI;
import com.tencent.mm.opensdk.openapi.IWXAPIEventHandler;
import com.tencent.mm.opensdk.openapi.WXAPIFactory;
import com.unity3d.player.UnityPlayer;

import android.app.Activity;
import android.content.Context;
import android.os.Bundle;

public class WXEntryActivity extends Activity implements IWXAPIEventHandler{
	public static WechatInterface mWechatResponse;
	
	//微信回调页面初始化
	public void onCreate(Bundle savedInstanceState)
	{
		super.onCreate(savedInstanceState);
		//将我们当前的页面注册为微信回调页面
		MyApplication.WApi.handleIntent(getIntent(), this);
	}
	
	//初始化微信
	public static IWXAPI Init(Context text, String App_id)
	{
		IWXAPI sApi = WXAPIFactory.createWXAPI(text, App_id,true);
		sApi.registerApp(App_id);
		return sApi;
	}
	//微信登录
	public static void WechatLogin(WechatInterface interfaces)
	{
		UnityPlayer.UnitySendMessage("SDKMessage", "OpenCommonBox", "我是WXEntryActivity中的WechatLogin11111");
	    // 创建一个微信请求
	    SendAuth.Req req = new SendAuth.Req();
	    // 请求的作用域:登录，获取用户信息（微信id,微信昵称,微信头像,性别。。。。）
	    req.scope = "snsapi_userinfo";
	    //用来保持请求和返回的正确的状态码，再返回时将原样返回
	    req.state = "wechat_sdk_demo_test_1_12";
	    //发送请求
	    MyApplication.WApi.sendReq(req);
	    mWechatResponse = interfaces;
	    UnityPlayer.UnitySendMessage("SDKMessage", "OpenCommonBox", "我是WXEntryActivity中的WechatLogin2222");
	}

	@Override
	public void onReq(BaseReq arg0) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void onResp(BaseResp arg0) {
		// TODO Auto-generated method stub
		switch(arg0.errCode)
		{
		//返回登录成功
		case BaseResp.ErrCode.ERR_OK:
			if(mWechatResponse!=null){
				//获取微信登录的临时授权码
				String code = ((SendAuth.Resp)arg0).code;
				System.out.println(code);
				mWechatResponse.getResponse(code);
				mWechatResponse = null;
			}
			break;
		}
		finish();
	}
	//微信请求成功的接口
	public interface WechatInterface{
		void getResponse(String Code);
	}
}
