<template>
	<view>
		<cu-custom :isBack="true" bgColor="bg-gradual-pink">
			<block slot="backText">返回</block>
			<block slot="content">商品详情</block>
		</cu-custom>
		<view class="cu-bar bg-white solid-bottom margin-top">
			<view class="action">{{ productInfo.SysNo }}-{{ productInfo.ProductName }}-{{ productInfo.ProductID }}</view>
		</view>
		<view class="VerticalBox">
			<scroll-view v-if="showMenu" class="VerticalNav nav" scroll-y scroll-with-animation :scroll-top="verticalNavTop" style="height:calc(100vh - 200upx)">
				<view class="cu-item" :class="index == tabCur ? 'text-green cur' : ''" v-for="(item, index) in list" :key="index" @tap="TabSelect" :data-id="index">
					{{ item.name }}
				</view>
			</scroll-view>
			<scroll-view class="VerticalMain" scroll-y scroll-with-animation style="height:calc(100vh - 200upx)" :scroll-into-view="'main-' + mainCur" @scroll="VerticalMain">
				<view class="cu-card article no-card" :key="0" :id="'main-0'">
					<view class="cu-list menu solid-bottom shadow-lg">
						<view class="bg-gradual-green cu-item arrow" @tap="hideOrShow">
							<view class="content">
								<text class="cuIcon-title text-red"></text>
								<text>商品基础信息</text>
							</view>
						</view>
						<view class="cu-list menu-avatar">
							<view class="cu-form-group">
								<view class="title">类别：</view>
								<view class="action">{{ productInfo.V_C1Name }}->{{ productInfo.V_C2Name }}->{{ productInfo.V_C3Name }}</view>
							</view>
							<view class="cu-form-group">
								<view class="title">编号：</view>
								<input type="text" v-model="productInfo.ProductID" />
							</view>
							<view class="cu-form-group">
								<view class="title">名称：</view>
								<input type="text" v-model="productInfo.BriefName" />
							</view>
							<view class="cu-form-group">
								<view class="title">拼音码：</view>
								<input type="text" v-model="productInfo.BriefNameSpellCode" />
							</view>
							<view class="cu-form-group">
								<view class="title">包装规格：</view>
								<input type="text" v-model="productInfo.packspec" />
							</view>
							<view class="cu-form-group">
								<view class="title">单位：</view>
								<input type="text" v-model="productInfo.DenominatedUnit" />
							</view>
							<view class="cu-form-group">
								<view class="title">条码：</view>
								<input type="text" v-model="productInfo.BarCode" />
							</view>
							<view class="cu-form-group">
								<view class="title">箱规：</view>
								<input type="text" v-model="productInfo.CartonMeasurement" />
							</view>
							<view class="cu-form-group">
								<view class="title">关键字：</view>
								<input type="text" v-model="productInfo.SearchKey" />
							</view>
						</view>
					</view>
				</view>
				<view class="cu-card article no-card" :key="1" :id="'main-1'">
					<view class="cu-list menu margin-top-sm shadow-lg">
					<view class="bg-gradual-green cu-item arrow" @tap="hideOrShow">
						<view class="content">
							<text class="cuIcon-title text-red"></text>
							<text>商品要货信息</text>
						</view>
					</view>
					<view class="cu-list menu-avatar">
						<view class="cu-form-group">
							<view class="title">分货单位：</view>
							<input type="text" v-model="productInfo.DivisionUnit" />
						</view>
						<view class="cu-form-group">
							<view class="title">周转天数：</view>
							<input type="text" v-model="productInfo.TurnOverDays" />
						</view>
						<view class="cu-form-group">
							<view class="title">采购周期：</view>
							<input type="text" v-model="productInfo.PurchasePeriodWeek" />
						</view>
						<view class="cu-form-group">
							<view class="title">截单时间：</view>
							<input type="text" v-model="productInfo.StopOrderTime" />
						</view>
						<view class="cu-form-group">
							<view class="title">配送方式：</view>
							<input type="text" v-model="productInfo.BriefName" />
						</view>
						<view class="cu-form-group">
							<view class="title">保存方式：</view>
							<input type="text" v-model="productInfo.BarCode" />
						</view>
						<view class="cu-form-group">
							<view class="title">箱规：</view>
							<input type="text" v-model="productInfo.BriefName" />
						</view>
						<view class="cu-form-group">
							<view class="title">关键字：</view>
							<input type="text" v-model="productInfo.BriefName" />
						</view>
					</view>
				</view>
				</view>
			</scroll-view>
		</view>
	</view>
</template>

<script>
export default {
	data() {
		return {
			list: [
				{ id: 0, name: '基础' },
				{ id: 1, name: '要货' },
				{ id: 2, name: '采购' },
				{ id: 3, name: '价格' },
				{ id: 4, name: '门店' },
				{ id: 5, name: '主子商品' },
				{ id: 6, name: '采购蓝' }
			],
			tabCur: 0,
			mainCur: 0,
			verticalNavTop: 0,
			load: true,
			productsysno: 0,
			showMenu: false,
			searchDataDto: {
				Token: uni.getStorageSync(this.MshSessionID),
				TimeSpan: uni.getStorageSync(this.mshconfig.mshdata_expirationName),
				ReqBody: {
					IsPaging: true,
					PageSize: 10,
					PageIndex: 1,
					SearchStatus: [0, 1]
				}
			},
			productInfo: {}
		};
	},
	onLoad(options) {
		console.log(options);
		this.productsysno = options.productsysno;
		this.searchDataDto.ReqBody.ProductSysNo = this.productsysno;
		this.initData();
		uni.showLoading({
			title: '加载中...',
			mask: true
		});
		this.listCur = this.list[0];
	},
	onReady() {
		this.loadProductData();
		uni.hideLoading();
	},
	methods: {
		hideOrShow() {
			this.showMenu = !this.showMenu;
		},
		initData() {},
		loadProductData() {
			var self = this;
			var controll = this.SERVER_URL + 'Product/QueryProductDetail';

			this.http.post(controll, this.searchDataDto).then(res => {
				console.log(res);
				self.productInfo = res.data.Data.ReqBody;
			});
		},
		TabSelect(e) {
			this.tabCur = e.currentTarget.dataset.id;
			this.mainCur = e.currentTarget.dataset.id;
			this.verticalNavTop = (e.currentTarget.dataset.id - 1) * 50;
		},
		VerticalMain(e) {
			// #ifdef MP-ALIPAY
			return false; //支付宝小程序暂时不支持双向联动
			// #endif
			let that = this;
			let tabHeight = 0;
			if (this.load) {
				for (let i = 0; i < this.list.length; i++) {
					let view = uni.createSelectorQuery().select('#main-' + this.list[i].id);
					view.fields(
						{
							size: true
						},
						data => {
							this.list[i].top = tabHeight;
							tabHeight = tabHeight + data.height;
							this.list[i].bottom = tabHeight;
						}
					).exec();
				}
				this.load = false;
			}
			let scrollTop = e.detail.scrollTop + 10;
			for (let i = 0; i < this.list.length; i++) {
				if (scrollTop > this.list[i].top && scrollTop < this.list[i].bottom) {
					this.verticalNavTop = (this.list[i].id - 1) * 50;
					this.tabCur = this.list[i].id;
					console.log(scrollTop);
					return false;
				}
			}
		}
	}
};
</script>

<style>
.fixed {
	position: fixed;
	z-index: 99;
}

.VerticalNav.nav {
	width: 200upx;
	white-space: initial;
}

.VerticalNav.nav .cu-item {
	width: 100%;
	text-align: center;
	background-color: #fff;
	margin: 0;
	border: none;
	height: 50px;
	position: relative;
}

.VerticalNav.nav .cu-item.cur {
	background-color: #f1f1f1;
}

.VerticalNav.nav .cu-item.cur::after {
	content: '';
	width: 8upx;
	height: 30upx;
	border-radius: 10upx 0 0 10upx;
	position: absolute;
	background-color: currentColor;
	top: 0;
	right: 0upx;
	bottom: 0;
	margin: auto;
}

.VerticalBox {
	display: flex;
}

.VerticalMain {
	background-color: #f1f1f1;
	flex: 1;
}
</style>
