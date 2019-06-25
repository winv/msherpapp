import Request from '../libs/request'
import mshconfig from '../config/mshconfig.js'
import permisson from '../service/permisson.service.js'
const httprequest = new Request();
httprequest.setConfig((config) => { /* 设置全局配置 config 为默认全局配置*/
	config.header = {
		'Content-Type': 'application/json;charset=UTF-8',
		'Access-Control-Allow-Origin': 'http://localhost/',
		'Access-Control-Allow-Credentials': true,
		'Access-Control-Allow-Headers': 'x-requested-with,content-type',
		'Token': uni.getStorageSync(mshconfig.mshsessionid),
		'MshUserID':uni.getStorageSync('MshUserID'),
	}
	return config
})

//拦截器
httprequest.interceptor.request((config, cancel) => { /* 请求之前拦截器，cancel 为函数，如果调用会取消本次请求。需要注意：调用cancel,本次请求的catch仍会执行。必须return config */
	config.header = {
		'Content-Type': 'application/json;charset=UTF-8',
		'Access-Control-Allow-Origin': 'http://localhost/',
		'Access-Control-Allow-Credentials': true,
		'Access-Control-Allow-Headers': 'x-requested-with,content-type',
		'Token': uni.getStorageSync(mshconfig.mshsessionid),
		'MshUserID':uni.getStorageSync('MshUserID'),
	}
	/* 如果token不存在，调用cancel 会取消本次请求，但是该函数的catch() 仍会执行*/
	if (uni.getStorageSync(mshconfig.mshsessionid)==='') { 
		cancel('token 存在 自动登录中...') // 接收一个参数，会传给catch((err) => {}) err.errMsg === 'token 不存在'
		permisson.autologin();
	}
	return config;
})

/* 请求之后拦截器,对返回错误统一处理,必须return response*/
httprequest.interceptor.response((res) => { 
	let {
		data
	} = res;
	//401 未登录
	if (res.statusCode == 401) {
		// console.log(this.MshSessionID);
		// uni.removeStorage(this.MshSessionID);
		uni.showToast({
			icon: 'none',
			title: '未登陆或登陆已过期，请重新登陆',
		});
		uni.reLaunch({
			url: '../../login/login',
		});
		return false;
	}
	if (res.statusCode == 404) {
		uni.showToast({
			icon: 'none',
			title: data.Message,
		});
		return false;
	}
	//501 自定义的错误
	if (res.statusCode == 501) {
		uni.showToast({
			icon: 'none',
			title: data.Message,
		});
		return false;
	}
	//非200 其他错误
	if (res.statusCode != 200) {
		uni.showToast({
			icon: 'none',
			title: data.Message,
		});
		return false;
	}
	//请求正常但是状态不对
	if (res.statusCode == 200) {
		if (!data.Status) {
			uni.showToast({
				icon: 'none',
				title: data.Data.msg,
			});
			return res;
		}
		return res;
	}
	return res;

})
export default httprequest;
