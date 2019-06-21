import Vue from 'vue'
import mshconfig from '../config/mshconfig.js'
import httprequest from '../libs/httprequest.js'
import md5 from '../libs/md5.js'
import urls from '../libs/urls.js'
import wxsystem from '../libs/wxsystem.js'

let permisson = {};

/**
 * 自动登录接口
 * 先获取用户登录状态 再获取wxopenid 再执行登录
 * @param {*} param
 *
 */
permisson.autologin = function(tomain) {
	var istomain=tomain||false
	return new Promise(function(resolve, reject) {
		var result=getlogincode(istomain).then;
		resolve(result);
	})
}

let getlogincode = function(istomain) {
	uni.login({
		provider: 'weixin',
		success: (res) => {
			let wxCode = res.code
			return GetOpenId(wxCode,istomain)
		},
		fail: (err) => {
			console.error('授权登录失败：' + JSON.stringify(err));
		}
	});
}
let GetOpenId = function(wxCode,istomain) {
	//获取与用户登录状态后改用服务端获取用户OPENID 以保证秘钥安全
	uni.request({
		url: urls.getFullERPUrl('WeiXin/GetOpenId/?json_code=' + wxCode),
		method: 'GET',
		data: {},
		success(res) {
			console.log(res);
			var user = {
				Token: "",
				TimeSpan: 0,
				WeiXinOpenID: res.data.Data.openid,
			};
			console.log(user);
			uni.request({
				url: urls.getFullERPUrl('Login/CheckWithWxOpenId/'),
				method: 'POST',
				data: user,
				success(res) {
					console.log(res)
					if (res.data.Status) {
						uni.setStorageSync("MshUserSession", res.data.Data.oSession)
						uni.setStorageSync(mshconfig.mshsessionid, res.data.Data.Token);
						uni.setStorageSync(mshconfig.mshdata_expirationName, mshconfig.mshdata_expirationTime)
						if(istomain){
							uni.reLaunch({
								url:'/pages/ControlPanel/main/main'
							})
						}
					} else {
						uni.showToast({
							icon: 'none',
							title: '自动登录失败，系统自动跳转到登录页'
						});
						setTimeout(uni.reLaunch({
							//如果自动登录失败，强制跳转到手动登录页面
							url: '/pages/login/login'
						}), 500)
					}
				}
			})
		}
	})
}
export default permisson
