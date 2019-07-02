<template>
	<view class="content">
		<cu-custom bgColor="bg-gradual-pink">
			<block slot="backText"></block>
			<block slot="content">我的</block>
		</cu-custom>
		<view class="cu-list menu-avatar margin-top margin-bottom-sm">
			<view class="cu-item">
				<view class="cu-avatar round lg" :style="'background-image:url(' + userInfo.avatarUrl + ');'"></view>
				<view class="content flex-sub">
					<view class="">{{ userInfo.nickName }}</view>
					<view class="text-sm flex justify-between">{{ userInfo.country }}</view>
				</view>
			</view>
		</view>
		<view class="cu-list menu margin-top margin-bottom-sm">
			<view class="cu-item arrow" @tap="bindWeiXin">
				<view class="content">
					<text class="cuIcon-circlefill text-grey"></text>
					<text class="">绑定微信登录</text>
				</view>
			</view>
			<view class="cu-item arrow">
				<view class="content">
					<text class="cuIcon-circlefill text-grey"></text>
					<text class="">设置</text>
				</view>
			</view>
		</view>
		<view class="padding flex flex-direction"><button class="cu-btn bg-grey lg" @tap="exitLogin">退出登录</button></view>
	</view>
</template>

<script>
	import permisson from '../../../service/permisson.service.js'
	export default {
		data() {
			return {
				userInfo: {},
				binding: false,
			};
		},
		onLoad() {
			this.getUserInfo();
		},
		methods: {
			getUserInfo() {
				var self = this;
				uni.getUserInfo({
					provider: 'weixin',
					success: infoRes => {
						self.userInfo = infoRes.userInfo;
						console.log(infoRes);
					}
				});
			},
			exitLogin() {
				console.log('logout clicked')
				permisson.logout()
			},
			bindWeiXin() {
				console.log('bind weixin clicked');
				var self = this;
				var userinfo = uni.getStorageSync("MshUserSession");
				permisson.bindweixin(userinfo).then(res => {
					console.log(res);
					if (res.Status) {
						uni.showToast({
							icon: 'none',
							title: '绑定成功'
						});
					} else {
						uni.showToast({
							icon: 'none',
							title: '绑定失败'
						});
					}
				})
			}
		}
	};
</script>

<style></style>
