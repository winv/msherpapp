<template>
	<view class="content">
		<view>
			<scroll-view scroll-y class="page">
				<image src="https://cdn.nlark.com/yuque/0/2019/png/280374/1552996358228-assets/web-upload/e256b4ce-d9a4-488b-8da2-032747213982.png"
				 mode="widthFix" class="response"></image>
				<view class="nav-list">
					<navigator hover-class="none" navigateTo :url="item.urls" class="nav-li" :class="'bg-'+item.color"
					 :style="[{animation: 'show ' + ((index+1)*0.2+1) + 's 1'}]" v-for="(item,index) in elements" :key="index">
						<view class="nav-title">{{item.title}}</view>
						<view class="nav-name">{{item.name}}</view>
						<text :class="'cuIcon-' + item.cuIcon"></text>
					</navigator>
				</view>
				<view class="cu-tabbar-height"></view>
			</scroll-view>
		</view>
	</view>
</template>

<script>
	import bottomMenu from '@/components/bottom-menu/bottom-menu.vue'
	import uniGrid from '@/components/grid/uni-grid.vue'
	export default {
		components: {
			bottomMenu,
			uniGrid
		},
		data() {
			return {
				PageCur: 'basics',
				options: [{
						image: 'https://img-cdn-qiniu.dcloud.net.cn/img/shu.png',
						text: '圣诞树'
					}
				],
				elements: [
					{
						title: '商品',
						name: 'Product',
						color: 'blue',
						cuIcon: 'apps',
						urls: '/pages/ControlPanel/product/productlist'
					},
					{
						title: '采购单',
						name: 'Purchase',
						color: 'brown',
						cuIcon: 'shop',
						urls: '/pages/ControlPanel/purchase/polist'
					},
					{
						title: '采购篮',
						name: 'PoBasket',
						color: 'mauve',
						cuIcon: 'goodsfill',
						urls: '/pages/ControlPanel/product/pobasket'
					},
					// {
					// 	title: '拍照识别',
					// 	name: 'Report',
					// 	color: 'purple',
					// 	cuIcon: 'font',
					// 	urls: '/pages/ControlPanel/product/productlist'
					// },
					// {
					// 	title: '订单 ',
					// 	name: 'Order',
					// 	color: 'mauve',
					// 	cuIcon: 'cuIcon',
					// 	urls: '/pages/ControlPanel/product/productlist'
					// },
					// {
					// 	title: '库存',
					// 	name: 'Inventory',
					// 	color: 'pink',
					// 	cuIcon: 'btn',
					// 	urls: '/pages/ControlPanel/'
					// },
					
					{
						title: '新建采购单',
						name: 'Create',
						color: 'purple',
						cuIcon: 'roundaddfill',
						urls: '/pages/ControlPanel/purchase/createpo'
					},
					{
						title: '拍照识别',
						name: 'Finance',
						color: 'green',
						cuIcon: 'ImageInput',
						urls: '/pages/ControlPanel/product/productAilist'
					},
					// {
					// 	title: '登陆',
					// 	name: 'Login',
					// 	color: 'olive',
					// 	cuIcon: 'icloading',
					// 	urls: '/pages/login/login'
					// }
					// {
					// 	title: '边框阴影',
					// 	name: 'shadow',
					// 	color: 'olive',
					// 	cuIcon: 'copy'
					// },
					// {
					// 	title: '加载',
					// 	name: 'loading',
					// 	color: 'green',
					// 	cuIcon: 'loading2'
					// }
				]
			}
		},
		methods: {
			tourl: function(index) {
				console.log(index);
			},
			NavChange: function(e) {
				this.PageCur = e.currentTarget.dataset.cur
			},
			toChild(e) {
				console.log(e.currentTarget.dataset.url)
				uni.navigateTo({
					url: e.currentTarget.dataset.url
				})
			},
		}
	}
</script>

<style>

</style>
