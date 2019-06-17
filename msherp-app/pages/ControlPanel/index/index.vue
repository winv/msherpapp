<template>
	<view>
		<cu-custom bgColor="bg-gradual-pink">
			<block slot="backText"></block>
			<block slot="content">工作台</block>
		</cu-custom>
		<view class="cu-list menu-avatar margin-top margin-bottom-sm">
			<view class="cu-item">
				<view class="cu-avatar round lg" style="'background-image:url('');'"></view>
				<view class="content flex-sub">
					<view class="text-grey">今日门店销售情况</view>
					<view class="text-gray text-sm flex justify-between">
						销量第一：江湾店
					</view>
				</view>
			</view>
		</view>
		<view class="cu-list menu margin-top margin-bottom-sm">
			<view class="cu-item arrow">
				<view class="content">
					<text class="cuIcon-circlefill text-grey"></text>
					<text class="text-grey">待处理盘点单</text>
				</view>
				<view class="action">
					<view class="cu-tag round bg-orange light">99</view>
				</view>
			</view>
			<view class="cu-item arrow">
				<view class="content">
					<text class="cuIcon-circlefill text-grey"></text>
					<text class="text-grey">待处理移库单</text>
				</view>
				<view class="action">
					<view class="cu-tag round bg-orange light">99</view>
				</view>
			</view>
			<view class="cu-item arrow">
				<view class="content">
					<text class="cuIcon-circlefill text-grey"></text>
					<text class="text-grey">未完结损益单</text>
				</view>
				<view class="action">
					<view class="cu-tag round bg-orange light">99</view>
				</view>
			</view>
			<view class="cu-item arrow">
				<view class="content">
					<text class="cuIcon-circlefill text-grey"></text>
					<text class="text-grey">采购篮</text>
				</view>
				<view class="action">
					<view class="cu-tag round bg-orange light">99</view>
				</view>
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
