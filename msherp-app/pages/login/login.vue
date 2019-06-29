<template>
	<view class="body">
		<cu-custom bgColor="bg-gradual-pink" :isBack="true">
			<block slot="backText"></block>
			<block slot="content">登录</block>
		</cu-custom>
		<image class="background-img" :style="'width:'+systemWidth+'px;height:'+systemHeight+'px;'" src="../../static/msh-login-pic.png"
		 mode="scaleToFill" />
		<image class="title-img" src="../../static/login-title-icon.png" mode="scaleToFill" />
		<view class="form-view">
			<view style="position: relative;">
				<input class="inner-input1" confirm-type="next" type="text" v-model="account" placeholder="请输入帐号" placeholder-style="color:#000000;font-size:31rpx;" />
				<image class="bottom-line-img1" src="../../static/underscore-icon.png" mode="scaleToFill" />
				<input class="inner-input2" confirm-type="done" :type="'password'" v-model="password" placeholder="请输入密码"
				 placeholder-style="color:#000000;font-size:31rpx;" />
				<image class="bottom-line-img2" src="../../static/underscore-icon.png" mode="scaleToFill" />
				<button hover-class="hover-btn" class="inner-btn" open-type="getUserInfo" @tap="accountLogin">登录</button>
				<view class="inner-bottom-view">
					<view style="width:145rpx;">
						<checkbox v-model="remeberme" checked="true"></checkbox>记住我
					</view>
				</view>
			</view>
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
	import wxsystem from '../../libs/wxsystem.js'

	export default {
		components: {
			mInput,
			mCheckBox
		},
		data() {
			return {
				systemHeight: 0,
				systemWidth: 0,
				providerList: [],
				hasProvider: false,
				account: '',
				password: '',
				positionTop: 0,
				isShowOther: false,
				remeberme: true,
				logining: false
			}
		},
		computed: mapState(['forcedLogin']),
		onLoad() {
			if (this.remeberme) {
				this.account = uni.getStorageSync('mshuseraccountid')
				this.password = uni.getStorageSync('mshuseraccountpwd')
			}
			this.systemHeight = wxsystem.getSysHeight()
			this.systemWidth = wxsystem.getSysWidth()
		},
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
			accountLogin() {
				if (this.account.length < 3) {
					uni.showToast({
						icon: 'none',
						title: '账号最短为 3 个字符'
					});
					return;
				}
				if (this.password.length < 5) {
					uni.showToast({
						icon: 'none',
						title: '密码最短为 5 个字符'
					});
					return;
				}
				var user = {
					Token: "",
					TimeSpan: 0,
					ReqBody: {
						"UserID": this.account,
						"Pwd": this.Md5(this.password) //注意密码经过MD5加密，服务端不需要再次加密
					}
				};
				if (this.remeberme) {
					this.rememberMe();
				}
				this.logining = true;
				permisson.accountlogin(user).then(res => {
					console.log(res);
					this.logining = false;
					if (res.Status) {
						// console.log(res.header["Set-Cookie"])
						uni.setStorageSync("MshUserSession", res.Data.oSession)
						uni.setStorageSync(this.MshSessionID, res.Data.Token);
						uni.setStorageSync(this.mshconfig.mshdata_expirationName, this.mshconfig.mshdata_expirationTime)
						this.toMain(this.account);
					}
				})
			},
			oauth(value) {
				this.logining = true;
				permisson.autologin(true).then(res => {
					console.log(res);
					this.logining = false;
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
			rememberMe() {
				uni.setStorageSync('mshuseraccountid', this.account);
				uni.setStorageSync('mshuseraccountpwd', this.password)
			}
		},
		onReady() {
			this.initPosition();
			this.initProvider();
		}
	}
</script>
<style lang="less">
	page {
		background-color: #f3f5f2;
	}

	.body {
		height: 100%;
		font-family: "\5FAE\8F6F\96C5\9ED1", arial;
		overflow: hidden;
	}

	.background-img {
		position: fixed;
		top: 0px;
		left: 0px;
		z-index: 100;
	}

	.form-view {
		position: fixed;
		width: 616rpx;
		z-index: 102;
		top: 550rpx;
		left: 68rpx;
	}

	.title-img {
		position: fixed;
		width: 290rpx;
		height: 116rpx;
		top: 264rpx;
		left: 230rpx;
		z-index: 102;
	}

	.inner-input1 {
		width: 466rpx;
		height: 80rpx;
		line-height: 80rpx;
		padding-left: 24rpx;
		color: #000000;
	}

	.inner-input2 {
		width: 466rpx;
		height: 80rpx;
		line-height: 80rpx;
		margin-top: 48rpx;
		padding-left: 24rpx;
		color: #000000;
	}

	.bottom-line-img1 {
		position: absolute;
		width: 616rpx;
		height: 8rpx;
		top: 71rpx;
		left: 0px;
	}

	.bottom-line-img2 {
		position: absolute;
		width: 616rpx;
		height: 8rpx;
		top: 196rpx;
		left: 0px;
	}

	.inner-btn {
		margin-top: 50rpx;
		width: 616rpx;
		height: 88rpx;
		line-height: 88rpx;
		border-radius: 11rpx;
		font-size: 31rpx;
		background: transparent;
		color: #fff;
		background-color: rgba(205, 22, 22, 0.5);
		border: 1rpx solid #C5343A;
		text-align: center;
	}

	.inner-select {
		height: 40rpx;
		line-height: 40rpx;
		font-size: 28rpx;
		color: #ffffff;
		display: flex;
		margin-top: 36rpx;
	}

	.select-pic-view {
		width: 40rpx;
		height: 40rpx;
	}

	.eye-icon-view {
		width: 48rpx;
		height: 40rpx;
		position: absolute;
		right: 24rpx;
		top: 148rpx;
	}

	.hover-btn {
		background-color: RGBA(205, 22, 22, 0.7);
		border: 1rpx solid #B1282D;
	}

	.inner-bottom-view {
		display: flex;
		justify-content: flex-end;
		width: 616rpx;
		margin-top: 30rpx;
		font-size: 28rpx;
		color: #DFDFDF;
	}
</style>
