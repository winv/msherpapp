import Vue from 'vue'
import mshconfig from '../config/mshconfig.js'
const appId = ''
const systemId = '1' // 系统识别号

/**
 * @description (登录凭证+用户基本信息)
 * @param restUrl 接口地址
 * @param _restParams 接口参数
 * @returns Promise
 * @author lee
 */
function getUserInfo() {
	return new Promise((resolve, reject) => {
		uni.login({
			provider: 'weixin',
			success: (res) => {
				let wxCode = res.code
				uni.getUserInfo({
					provider: 'weixin',
					success: (res) => {
						let info = {}
						let _userInfo = res.userInfo
						info.userinfo = _userInfo
						info.code = wxCode
						info.encryptedData = res.encryptedData
						info.iv = res.iv
						console.log(`微信授权登录/更新信息`)
						console.log(info)
						resolve(info)
					}
				});
			},
			fail: (err) => {
				console.error('授权登录失败：' + JSON.stringify(err));
			}
		});
	})
};

/**
 * @description 获取手机信息
 * @author lee
 */
function getSystemInfo() {
	try {
		var data = uni.getSystemInfoSync()
		if (data) {
			return data
		}
	} catch (err) {}
	return null
}

/**
 * @description 获取屏幕高度
 * @author lee
 */
function getSysHeight() {
	let height = 0
	if (uni.getSystemInfoSync().system.indexOf('Android') !== -1) {
		height = uni.getSystemInfoSync().windowHeight
	} else {
		height = uni.getSystemInfoSync().windowHeight
	}
	return height
}

/**
 * @description 获取屏幕宽度
 * @author lee
 */
function getSysWidth() {
	var width = wepy.getSystemInfoSync().windowWidth
	if (width) {
		return width
	}
	return 0
}

/**
 * @description 获取tokenId
 * @author lee
 */
function getTokenId() {
	const tokenId = uni.getStorageSync('tokenId') || ''
	if (tokenId !== '') {
		return tokenId
	} else {
		wx.showToast({
			title: 'session已丢失，请重新登录！',
			icon: 'none',
			duration: 1500
		})
		setTimeout(() => {
			wepy.reLaunch({
				url: '/pages/login'
			})
		}, 1500)
	}
}

export default {
	getUserInfo,
	getSystemInfo,
	getSysHeight,
	getSysWidth,
	systemId,
	getTokenId
}
