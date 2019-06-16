<template>
	<view>
		<cu-custom bgColor="bg-gradual-pink" :isBack="true">
			<block slot="backText"></block>
			<block slot="content">工作台</block>
		</cu-custom>
		<view v-if="hasLogin" class="hello">
			<view class="title">
				您好 {{userName}}，您已成功登录。
			</view>
			<view class="ul">
				<view>欢迎使用妙生活ERP小程序。</view>
				<view>在 “我的” 中点击 “退出” 可以 “注销当前账户”</view>
			</view>
			<view>
				<button type="primary" class="primary" @tap="showtest">验证</button>
			</view>
		</view>
		<view v-if="!hasLogin" class="hello">
			<view class="title">
				您好。
			</view>
			<view class="ul">
				<view>欢迎使用妙生活ERP小程序。</view>
				<view>请先登录</view>
			</view>
		</view>
	</view>
</template>

<script>
	import {
		mapState
	} from 'vuex'
	export default {
		computed: mapState(['forcedLogin', 'hasLogin', 'userName']),
		methods: {
			tologin() {
				uni.showModal({
					title: '未登录',
					content: '您未登录，需要登录后才能继续',
					/**
					 * 如果需要强制登录，不显示取消按钮
					 */
					showCancel: !this.forcedLogin,
					success: (res) => {
						if (res.confirm) {
							/**
							 * 如果需要强制登录，使用reLaunch方式
							 */
							if (this.forcedLogin) {
								uni.reLaunch({
									url: '../../login/login'
								});
							} else {
								uni.navigateTo({
									url: '../../login/login'
								});
							}
						}
					}
				});
			},
			showtest() {
				var controll = this.SERVER_URL + 'login/ShowTest/';
				this.http.post(controll, '').then(res => {

				});
			},
		},

		onLoad() {
			if (uni.getStorageSync(this.MshSessionID) === '') {
				this.tologin();
			} else {
				var timestamp = Date.parse(new Date());
				var expiration = timestamp + 1800000; //缓存30分钟
				var data_expiration = uni.getStorageSync(this.mshconfig.mshdata_expirationName);
				console.log(data_expiration)
				console.log(timestamp)
				if (data_expiration) {
					if (timestamp > data_expiration) {
						uni.clearStorageSync();
					}
				} else {
					this.hasLogin = true;
					uni.setStorageSync(this.mshconfig.mshdata_expirationName, this.mshconfig.mshdata_expirationTime)
				}
			}
		}
	}
</script>

<style>
	.hello {
		display: flex;
		flex: 1;
		flex-direction: column;
	}

	.title {
		color: #8f8f94;
		margin-top: 50upx;
	}

	.ul {
		font-size: 30upx;
		color: #8f8f94;
		margin-top: 50upx;
	}

	.ul>view {
		line-height: 50upx;
	}
</style>
