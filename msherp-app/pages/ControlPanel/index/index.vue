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
					<view class="">今日门店销售情况</view>
					<view class="text-sm flex justify-between">
						销量第一：江湾店
					</view>
				</view>
			</view>
		</view>
		<view class="cu-list menu margin-top margin-bottom-sm">
			<view class="cu-item arrow">
				<view class="content">
					<text class="cuIcon-circlefill "></text>
					<text class="">待处理盘点单</text>
				</view>
				<view class="action">
					<view class="cu-tag round bg-orange light">99</view>
				</view>
			</view>
			<view class="cu-item arrow">
				<view class="content">
					<text class="cuIcon-circlefill "></text>
					<text class="">待处理移库单</text>
				</view>
				<view class="action">
					<view class="cu-tag round bg-orange light text-red">99</view>
				</view>
			</view>
			<view class="cu-item arrow">
				<view class="content">
					<text class="cuIcon-circlefill "></text>
					<text class="">未完结损益单</text>
				</view>
				<view class="action">
					<view class="cu-tag round bg-orange light text-red">99</view>
				</view>
			</view>
			<view class="cu-item arrow">
				<view class="content">
					<text class="cuIcon-circlefill"></text>
					<text class="">采购篮</text>
				</view>
				<view class="action">
					<view class="cu-tag round bg-orange light text-red">99</view>
				</view>
			</view>
		</view>
	</view>
</template>

<script>

	import VueCommonConstData from '../../../config/VueCommonConstData.js'
	export default {
		methods: {},
		mixins:[VueCommonConstData],
		onLoad() {
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
