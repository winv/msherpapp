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
					<view class="text-grey">{{ userInfo.nickName }}</view>
					<view class="text-gray text-sm flex justify-between">{{ userInfo.country }}</view>
				</view>
			</view>
		</view>
		<view class="cu-list menu margin-top margin-bottom-sm">
			<view class="cu-item arrow" @tap="bindWeiXin">
				<view class="content">
					<text class="cuIcon-circlefill text-grey"></text>
					<text class="text-grey">绑定微信登录</text>
				</view>
			</view>
			<view class="cu-item arrow">
				<view class="content">
					<text class="cuIcon-circlefill text-grey"></text>
					<text class="text-grey">设置</text>
				</view>
			</view>
		</view>
		<view class="padding flex flex-direction"><button class="cu-btn bg-grey lg" @tap="exitLogin">退出登录</button></view>
		<view class="cu-load load-modal" v-if="binding">
			<image src="/static/logo.png" mode="aspectFit"></image>
			<view class="gray-text">绑定中，请稍后...</view>
		</view>
	</view>
</template>

<script>
export default {
	data() {
		return {
			userInfo: {},
			binding:false,
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
		exitLogin() {},
		bindWeiXin() {
			console.log('bind weixin clicked');
			this.binding = true;
		}
	}
};
</script>

<style></style>
