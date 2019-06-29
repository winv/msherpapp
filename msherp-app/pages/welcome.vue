<template>
	<view class="body">
		<cu-custom bgColor="bg-gradual-pink" :isBack="true">
			<block slot="content">妙生活</block>
		</cu-custom>
		<image class="background-img" :style="'width:'+systemWidth+'px;height:'+systemHeight+'px;'" src="https://image.weilanwl.com/gif/loading-white.gif" mode="scaleToFill" />
		<image src="https://image.weilanwl.com/gif/loading-white.gif" mode="aspectFit" class="gif-black response" ></image>
	</view>
</template>

<script>
	import permisson from '../service/permisson.service.js'
	import wxsystem from '../libs/wxsystem.js'
	export default {
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
		onLoad() {
			this.systemHeight = wxsystem.getSysHeight()
			this.systemWidth = wxsystem.getSysWidth()
		},
		mounted(){
			this.oauth()
		},
		methods: {
			oauth(value) {
				permisson.autologin(true).then(res => {
					console.log(res);
				});
			},
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
