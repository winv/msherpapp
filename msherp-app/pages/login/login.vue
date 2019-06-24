<template>
	<view>
		<cu-custom bgColor="bg-gradual-pink">
			<block slot="backText"></block>
			<block slot="content">登录</block>
		</cu-custom>
		<form>
			<view class="cu-form-group">
				<view class="title">帐号</view>
				<input type="text" clearable focus v-model="account" placeholder="请输入账号"></input>
			</view>
			<view class="cu-form-group">
				<view class="title">密码</view>
				<input type="password" displayable v-model="password" placeholder="请输入密码"></input>
			</view>
		</form>
		<view class="btn-row">
			<button type="primary" class="primary" @tap="bindLogin">登录</button>
			<button v-if="true" type="primary" class="primary" @tap="showtest">验证</button>
		</view>
		<view class="flex solid-bottom padding justify-end">
			<view class="padding-sm margin-xs radius">
				<checkbox v-model="remeberme" @tap="rememberMe"></checkbox>记住我
			</view>
		</view>
		<view class="action-row" v-if="isShowOther">
			<navigator url="../reg/reg">注册账号</navigator>
			<text>|</text>
			<navigator url="../pwd/pwd">忘记密码</navigator>
		</view>
		<view class="oauth-row" v-if="hasProvider" v-bind:style="{top: positionTop + 'px'}">
			<view class="oauth-image" v-for="provider in providerList" :key="provider.value">
				<image :src="provider.image" @tap="oauth(provider.value)"></image>
			</view>
		</view>
		<view class="cu-load load-modal" v-if="logining">
			<image src="/static/logo.png" mode="aspectFit"></image>
			<view class="gray-text">登录中...</view>
		</view>
	</view>
</template>

<script>
	import {
		mapState,
		mapMutations
	} from 'vuex'

	import mInput from '../../components/m-input.vue'
	import mCheckBox from '../../components/m-check-box.vue'
	import permisson from '../../service/permisson.service.js'

	export default {
		components: {
			mInput,
			mCheckBox
		},
		data() {
			return {
				providerList: [],
				hasProvider: false,
				account: 'lee',
				password: '000000',
				positionTop: 0,
				isShowOther: false,
				remeberme: true,
				logining:false
			}
		},
		computed: mapState(['forcedLogin']),
		methods: {
			...mapMutations(['login']),
			initProvider() {
				const filters = ['weixin'];
				uni.getProvider({
					service: 'oauth',
					success: (res) => {
						if (res.provider && res.provider.length) {
							for (let i = 0; i < res.provider.length; i++) {
								if (~filters.indexOf(res.provider[i])) {
									this.providerList.push({
										value: res.provider[i],
										image: '../../static/img/' + res.provider[i] + '.png'
									});
								}
							}
							this.hasProvider = true;
						}
					},
					fail: (err) => {
						console.error('获取服务供应商失败：' + JSON.stringify(err));
					}
				});
			},
			initPosition() {
				/**
				 * 使用 absolute 定位，并且设置 bottom 值进行定位。软键盘弹出时，底部会因为窗口变化而被顶上来。
				 * 反向使用 top 进行定位，可以避免此问题。
				 */
				this.positionTop = uni.getSystemInfoSync().windowHeight - 100;
			},
			bindLogin() {
				/**
				 * 客户端对账号信息进行一些必要的校验。
				 * 实际开发中，根据业务需要进行处理，这里仅做示例。
				 */
				if (this.account.length < 3) {
					uni.showToast({
						icon: 'none',
						title: '账号最短为 5 个字符'
					});
					return;
				}
				if (this.password.length < 6) {
					uni.showToast({
						icon: 'none',
						title: '密码最短为 6 个字符'
					});
					return;
				}
				console.log(this.SERVER_URL);
				var user = {
					Token: "",
					TimeSpan: 0,
					ReqBody: {
						"UserID": this.account,
						"Pwd": this.Md5(this.password) //注意密码经过MD5加密，服务端不需要再次加密
					}
				};
				this.logining=true;
				var validUser = false;
				//console.log(user);
				var controll = this.SERVER_URL + 'login/check/';
				var msg = "";
				this.http.post(controll, user).then(res => {
					console.log(res);
					if (res.data.Status) {
						// console.log(res.header["Set-Cookie"])
						uni.setStorageSync("MshUserSession", res.data.Data.oSession)
						uni.setStorageSync(this.MshSessionID, res.data.Data.Token);
						uni.setStorageSync(this.mshconfig.mshdata_expirationName, this.mshconfig.mshdata_expirationTime)
						this.toMain(this.account);
						this.logining=false;
					}
				});
			},
			oauth(value) {
				permisson.autologin(true).then(res => {
					console.log(res);
				});
			},
			toMain(userName) {
				this.login(userName);
				/**
				 * 强制登录时使用reLaunch方式跳转过来
				 * 返回首页也使用reLaunch方式
				 */
				if (this.forcedLogin) {
					uni.reLaunch({
						url: '../ControlPanel/main/main',
					});
				} else {
					uni.navigateBack();
				}
			},
			showtest() {
				Promise.showtest().then(res => {
					console.log(res);
				})
			},
			wxlogin(objvalue) {
				//获取与用户登录状态后改用服务端获取用户OPENID 以保证秘钥安全
				var controll = this.SERVER_URL + 'WeiXin/GetOpenId/?json_code=' + objvalue.code;
				this.http.get(controll, '').then(res => {
					//根据获取的OPENid 检测ERP侧是否绑定
					var obj = res.Data;
					console.log(obj);
					var user = {
						Token: "",
						TimeSpan: 0,
						ReqBody: {
							"openid": this.account,
						}
					};
					var controll2 = this.SERVER_URL + 'Login/CheckWithWxOpenId/'
					this.http.post(controll2, user).then(res => {
						if (res.Status) {
							uni.setStorageSync(this.MshSessionID, res.Data.Token);
							uni.setStorageSync(this.mshconfig.mshdata_expirationName, this.mshconfig.mshdata_expirationTime)
							//console.log(uni.getStorageSync(this.MshSessionID));
							this.toMain(this.account);
						}
					});
				});
			},
			rememberMe() {
				console.log("rememberMe");
			}
		},
		onReady() {
			this.initPosition();
			this.initProvider();
		}
	}
</script>
<style>
	.cu-form-group .title {
		min-width: calc(4em + 15px);
	}

	.action-row {
		display: flex;
		flex-direction: row;
		justify-content: center;
	}

	.action-row navigator {
		color: #007aff;
		padding: 0 20upx;
	}

	.oauth-row {
		display: flex;
		flex-direction: row;
		justify-content: center;
		position: absolute;
		top: 0;
		left: 0;
		width: 100%;
	}

	.oauth-image {
		width: 100upx;
		height: 100upx;
		border: 1upx solid #dddddd;
		border-radius: 100upx;
		margin: 0 40upx;
		background-color: #ffffff;
	}

	.oauth-image image {
		width: 60upx;
		height: 60upx;
		margin: 20upx;
	}
</style>
