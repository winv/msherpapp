<template>
	<view class="bottom-menu">
		<view class="cu-bar tabbar margin-bottom-xl bg-white">
			<view class="action text-green">
				<view class="cuIcon-homefill"></view> 首页
			</view>
			<view class="action text-gray">
				<view class="cuIcon-similar"></view> 分类
			</view>
			<view class="action text-gray add-action">
				<button class="cu-btn cuIcon-add bg-green shadow"></button>
				发布
			</view>
			<view class="action text-gray">
				<view class="cuIcon-cart">
					<view class="cu-tag badge">99</view>
				</view>
				购物车
			</view>
			<view class="action text-gray">
				<view class="cuIcon-my">
					<view class="cu-tag badge"></view>
				</view>
				我的
			</view>
		</view>
	</view>
</template>
<script>
	import faIcon from "@/components/kilvn-fa-icon/fa-icon.vue"
	export default {
		components: {
			faIcon
		},
		name: "bottom-menu",
		data() {
			return {
				meunList: [{
						name: '主页',
						path: '/pages/controlpanel/index/index',
						active: false,
						icons: 'home'
					},
					{
						name: '商品',
						active: false,
						icons: 'cart-arrow-down',
						children: [{
								name: '二级菜单1',
								path: '/pages/controlpanel/index/index'
							},
							{
								name: '二级菜单2',
								path: '/pages/controlpanel/index/index'
							},
							{
								name: '二级菜单3',
								path: '/pages/controlpanel/index/index'
							}
						]
					},
					{
						name: '报表',
						icons: 'pie-chart',
						active: false,
						children: [{
							name: '问题咨询',
							path: '/pages/controlpanel/index/index'
						}]
					},
					{
						name: '我',
						icons: 'user',
						active: false,
						children: [{
							name: '绑定微信登陆',
							path: '/pages/controlpanel/index/index'
						}]
					}
				]
			};
		},
		methods: {
			goToPage(e) {
				let _this = this;
				let index = e.currentTarget.dataset.index;
				let curMenu = this.meunList[index];
				if (!!curMenu.path) {
					// 一级菜单跳转
					uni.redirectTo({
						url: curMenu.path,
						animationType: 'pop-in',
						animationDuration: 200,
						complete: function() {
							_this.initMenu();
						}
					});
				} else {
					// 二级菜单显示
					this.meunList.forEach(item => {
						if (item != curMenu) {
							item.active = false;
						}
					});
					curMenu.active = !curMenu.active;
				}
			},
			tochildPage(e) {
				let _this = this;
				let path = e.currentTarget.dataset.path;
				uni.navigateTo({
					url: path,
					animationType: 'pop-in',
					animationDuration: 200,
					complete: function() {
						_this.initMenu();
					}
				});
			},
			initMenu() {
				this.meunList.forEach(item => {
					item.active = false;
				});
			}
		}
	}
</script>
<style>
	.bottom-menu {
		/* box-sizing:border-box; */
		position: fixed;
		left: 0;
		bottom: 0;
		width: 100%;
		z-index: 999;
		display: -webkit-flex;
		display: flex;
		justify-content: center;
		height: 100upx;
		line-height: 100upx;
		background: #FFF;
		text-align: center; 
	}
	.bottom-menu-item__content {
		position: absolute;
		left: 0;
		top: 0;
		width: 100%;
		height: 100%;
		display: flex;
		flex-direction: column;
		justify-content: center;
		align-items: center
	}
	.bottom-menu-item {
		flex: 1;
		flex-direction: column;
		border-top: 1px solid #e0e0e0;
		border-right: 1px solid #e0e0e0;
		position: relative;
	}
	.bottom-menu-item-image {
		width: 52upx;
		height: 52upx
	}
	.bottom-menu-item-text {
		font-size: 25upx;
		color: #333;
		margin-top: 1upx
	}

	.bottom-menu-wrap {
		margin: 0;
		padding: 0;
		position: absolute;
		width: 100%;
		bottom: 100upx;
		left: 0;
		background: #FFFFFF;
		box-shadow: #e0e0e0 0px 0px 1px 0px;
	}

</style>
