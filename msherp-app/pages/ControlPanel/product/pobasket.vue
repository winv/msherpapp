<template>
	<view>
		<cu-custom bgColor="bg-gradual-pink" :isBack="true">
			<block slot="backText">返回</block>
			<block slot="content">采购篮</block>
		</cu-custom>
		<view class="cu-bar bg-white search fixed" :style="[{ top: CustomBar + 'px' }]">
			<view class="search-form round">
				<text class="cuIcon-search"></text>
				<input type="text" v-model="searchDataDto.ReqBody.SearchKey" placeholder="支持商品号搜索" confirm-type="search" />
			</view>
			<view class="action">
				<button class="cu-btn bg-gradual-green shadow-blur round" @tap="handleSubmit">搜索</button>
				<button class="cu-btn bg-gradual-red shadow-blur round" @tap="handleSubmitAdv" data-target="DialogModal2">选项</button>
			</view>
		</view>
		<view class="cu-bar bg-white solid-bottom margin-top">
			<view class="action"></view>
		</view>
		<scroll-view :scroll-y="modalName == null" class="page" :class="modalName != null ? 'show' : ''" :style="[{ top: CustomBar + 'px' }]">
			<view class="cu-bar bg-white solid-bottom margin-top" v-if="!isfetchData">
				<view class="action">请输入条件查询数据！</view>
			</view>
			<view v-for="(item, index) in productList" :key="index">
				<view class="cu-bar bg-white solid-bottom margin-top">
					<view class="action">
						<text class="cuIcon-titles text-orange" v-if="false"></text>
						<checkbox class="cb" v-model="item.SysNo" :checked="item.checked" @tap="itemCheckd"></checkbox>
						<view @tap="showCart(item)" data-target="DialogCartModal">{{ item.ProductSysNo }}-{{ item.V_ProductName }}-{{ item.V_ProductID }}</view>
					</view>
					<view class="padding-sm margin-xs radius">
						<text class="lg text-gray" :class="'cuIcon-deletefill'" @tap="showDelete(item)" data-target="DialogDeleteCartModal"></text>
					</view>
				</view>
				<view class="cu-card article no-card">
					<view class="cu-item shadow">
						<view class="content">
							<view class="padding-sm flex flex-wrap">
								<view class="padding-xs">
									<view class="cu-capsule radius">
										<view class="cu-tag bg-brown sm">采购价格</view>
										<view class="cu-tag line-brown sm">{{ item.Quantity }}</view>
									</view>
									<view class="cu-capsule radius">
										<view class="cu-tag bg-brown sm">数量</view>
										<view class="cu-tag line-brown sm">{{ item.OrderPrice }}</view>
									</view>
								</view>
								<view class="padding-xs">
									<view class="cu-tag bg-red light sm radius">{{ item.V_StockName }}</view>
									<view class="cu-tag bg-green light sm radius" v-if="item.V_VendorName !== ''">{{ item.V_VendorName }}</view>
								</view>
							</view>
						</view>
					</view>
				</view>
			</view>
		</scroll-view>
		<view class="cu-bar bg-white tabbar border shop menu_box">
			<view class="btn-group">
				<button class="cu-btn bg-orange round shadow-blur" @tap="selectAll">全选</button>
				<button class="cu-btn bg-red round shadow-blur">生成采购单</button>
			</view>
		</view>

		<view class="cu-modal" :class="modalName == 'DialogModal2' ? 'show' : ''">
			<view class="cu-dialog">
				<view class="cu-bar bg-white justify-end">
					<view class="content">高级搜索选项</view>
					<view class="action" @tap="hideModal"><text class="cuIcon-close text-red"></text></view>
				</view>
				<view class="padding-xl">
					<view class="cu-form-group margin-top">
						<view class="title">商品状态</view>
						<picker @change="PickerChange" :value="pickindex" :range="ProductStatusList" :range-key="'name'">
							<view class="picker">{{ ProductStatusList[pickindex].name }}</view>
						</picker>
					</view>
					<view class="cu-form-group margin-top">
						<view class="title">可否要货</view>
						<picker @change="PickerChangeDemand" :value="pickdemandindex" :range="ProductDemandList" :range-key="'name'">
							<view class="picker">{{ ProductDemandList[pickdemandindex].name }}</view>
						</picker>
					</view>
					<view class="cu-form-group margin-top">
						<view class="title">主子商品</view>
						<picker @change="PickerChangeMaster" :value="pickmasterindex" :range="ProductMasterList" :range-key="'name'">
							<view class="picker">{{ ProductMasterList[pickmasterindex].name }}</view>
						</picker>
					</view>
					<!-- <view class="cu-form-group margin-top">
						<view class="title">商品管理人：</view>
						<picker @change="PickerChange" :value="pickindex" :range="ProductStatusList" v-model="searchData.Status"
						 :range-key="'name'">
							<view class="picker">
								{{ProductStatusList[pickindex].name}}
							</view>
						</picker>
					</view> -->
					<view class="cu-form-group margin-top">
						<view class="title">供应商编号：</view>
						<input type="text" v-model="searchDataDto.ReqBody.VendorSysNo" />
					</view>
				</view>
				<view class="cu-bar bg-white">
					<view class="action margin-0 flex-sub text-green solid-left" @tap="hideModal">取消</view>
					<view class="action margin-0 flex-sub  solid-left" @tap="hideModal">确定</view>
				</view>
			</view>
		</view>
		<view class="cu-modal" :class="modalName == 'DialogCartModal' ? 'show' : ''">
			<view class="cu-dialog pull-left">
				<view class="cu-bar bg-white justify-end">
					<view class="content">采购篮</view>
					<view class="action" @tap="hideModal"><text class="cuIcon-close text-red"></text></view>
				</view>
				<view class="cu-list menu-avatar padding-sm no-card">
					<view class="cu-form-group margin-top-sm">
						<view class="title">商品名称：</view>
						<view class="action pull-left">{{ ProductInfo.ProductSysNo }}-{{ ProductInfo.ProductName }}-{{ ProductInfo.ProductID }}</view>
					</view>
					<view class="cu-form-group">
						<view class="title">采购数量：</view>
						<input class="action" type="text" v-model="PoBasketDto.ReqBodyModel.Quantity" />
					</view>
					<view class="cu-form-group">
						<view class="title">采购单价：</view>
						<input type="text" v-model="PoBasketDto.ReqBodyModel.OrderPrice" />
					</view>
				</view>
				<view class="cu-bar bg-white">
					<view class="action margin-0 flex-sub text-green solid-left" @tap="hideModal">取消</view>
					<view class="action margin-0 flex-sub  solid-left" @tap="addToPoBasket">加入购物篮</view>
				</view>
			</view>
		</view>
		<view class="cu-modal sm" :class="modalName == 'DialogDeleteCartModal' ? 'show' : ''">
			<view class="cu-dialog ">
				<view class="padding-xl bg-white">
					<view class="lg text-yellow" :class="'cuIcon-goodsfill'"></view>
					<view class="title">确定删除该商品吗？</view>
				</view>
				<view class="cu-bar">
					<view class="action margin-0 flex-sub text-green solid-left" @tap="hideModal">取消</view>
					<view class="action margin-0 flex-sub  solid-left" @tap="addToPoBasket">确定</view>
				</view>
			</view>
		</view>
		<view class="cu-load load-modal" v-if="loadModal">
			<image src="/static/logo.png" mode="aspectFit"></image>
			<view class="gray-text">查询中...</view>
		</view>
	</view>
</template>

<script>
	export default {
		data() {
			return {
				StatusBar: this.StatusBar,
				CustomBar: this.CustomBar,
				loadModal: false,
				productList: [],
				modalName: null,
				gridCol: 3,
				gridBorder: false,
				menuBorder: false,
				menuArrow: false,
				menuCard: false,
				skin: false,
				listTouchStart: 0,
				listTouchDirection: null,
				ProductStatusList: [{
						name: '全部',
						value: ''
					},
					{
						name: '在售',
						value: '1'
					},
					{
						name: '未上架',
						value: '0'
					},
					{
						name: '下架',
						value: '-1'
					},
					{
						name: '冻结',
						value: '0'
					}
				],
				ProductMasterList: [{
						name: '全部',
						value: ''
					},
					{
						name: '主商品',
						value: 'true'
					},
					{
						name: '子商品',
						value: 'false'
					}
				],
				ProductDemandList: [{
						name: '全部',
						value: ''
					},
					{
						name: '可以要货',
						value: '1'
					},
					{
						name: '不能要货',
						value: '0'
					}
				],
				pickindex: 0,
				pickdemandindex: 0,
				pickmasterindex: 0,
				PoBasketDto: {
					Token: uni.getStorageSync(this.MshSessionID),
					TimeSpan: uni.getStorageSync(this.mshconfig.mshdata_expirationName),
					ReqBody: {
						StockSysNo: 6,
						ProductSysNo: 0,
						CreateUserSysNo: 0
					},
					ReqBodyModel: {
						StockSysNo: 6,
						ProductSysNo: 0,
						CreateUserSysNo: 0,
						Quantity: '',
						OrderPrice: '',
					},
				},
				ProductInfo: {},
				userInfo: {},
				isfetchData: false,
			};
		},
		onLoad() {
			this.userInfo = uni.getStorageSync("MshUserSession");
			console.log(this.userInfo);
		},
		methods: {
			handleSubmit() {
				this.loadModal = true;
				console.log('handleSubmit clicked');
				this.PoBasketDto.ReqBody.CreateUserSysNo = this.userInfo.User.SysNo
				this.fetchData();
			},
			loadMoreData() {
				console.log(this.searchResultData);
				this.fetchData();
			},
			fetchData() {
				var self = this;
				var controll = this.SERVER_URL + 'Purchase/QueryPoBaksetList';
				this.isfetchData = true;
				this.http.post(controll, this.PoBasketDto).then(res => {
					console.log(res);
					self.productList = res.data.Data.ReqBody;
					self.loadModal = false;
				});
			},
			itemCheckd(e){
				console.log(e);
				
			},
			selectAll(item){
				console.log(item)
				console.log(this.productList);
			},
			showCart(obj) {
				this.modalName = 'DialogCartModal';
				console.log(obj);
				this.ProductInfo = obj;
				this.PoBasketDto.ReqBody.ProductSysNo = obj.ProductSysNo;
				this.PoBasketDto.ReqBodyModel.ProductSysNo = obj.ProductSysNo;
				this.PoBasketDto.ReqBodyModel.CreateUserSysNo = this.userInfo.User.SysNo
				this.PoBasketDto.ReqBody.CreateUserSysNo = this.userInfo.User.SysNo
				console.log('cart clicked');
				var controll = this.SERVER_URL + 'Purchase/QueryPoBaksetDetail';
				this.http.post(controll, this.PoBasketDto).then(res => {
					console.log(res);
					if (res.data.Data.ReqBody[0] !== null) {
						var dataObj = res.data.Data.ReqBody[0];
						this.PoBasketDto.ReqBodyModel.Quantity = dataObj.Quantity;
						this.PoBasketDto.ReqBodyModel.OrderPrice = dataObj.OrderPrice;
					} else {
						this.PoBasketDto.ReqBodyModel.Quantity = '';
						this.PoBasketDto.ReqBodyModel.OrderPrice = '';
					}
				});
			},
			addToPoBasket(e) {
				var controll = this.SERVER_URL + 'Purchase/InsertPoBasket';
				this.http.post(controll, this.PoBasketDto).then(res => {
					console.log(res);
					if (res.data.Status) {
						uni.showToast({
							icon: 'none',
							title: '采购篮添加成功'
						});
						this.modalName = '';

					} else {
						uni.showToast({
							icon: 'none',
							title: res.data.Message
						});
					}
				});
			},
			showDelete() {
				this.modalName = "DialogDeleteCartModal";
			},
			showModal(e) {
				this.modalName = e.currentTarget.dataset.target;
			},
			hideModal(e) {
				this.modalName = null;
			},
			Gridchange(e) {
				this.gridCol = e.detail.value;
			},
			Gridswitch(e) {
				this.gridBorder = e.detail.value;
			},
			MenuBorder(e) {
				this.menuBorder = e.detail.value;
			},
			MenuArrow(e) {
				this.menuArrow = e.detail.value;
			},
			MenuCard(e) {
				this.menuCard = e.detail.value;
			},
			SwitchSex(e) {
				this.skin = e.detail.value;
			},
			// ListTouch触摸开始
			ListTouchStart(e) {
				console.log(e);
				this.listTouchStart = e.touches[0].pageX;
			},
			// ListTouch计算方向
			ListTouchMove(e) {
				this.listTouchDirection = e.touches[0].pageX - this.listTouchStart > 0 ? 'right' : 'left';
			},
			// ListTouch计算滚动
			ListTouchEnd(e) {
				if (this.listTouchDirection == 'left') {
					this.modalName = e.currentTarget.dataset.target;
				} else {
					this.modalName = null;
				}
				this.listTouchDirection = null;
			},
			handleSubmitAdv(e) {
				console.log('handleSubmitAdv clicked');
				this.showModal(e);
			},
			PickerChange(e) {
				this.pickindex = e.detail.value;
				this.searchDataDto.ReqBody.SearchStatus = [this.ProductStatusList[this.pickindex].value];
			},
			PickerChangeDemand(e) {
				this.pickdemandindex = e.detail.value;
				this.searchDataDto.ReqBody.IsProductDemand = this.ProductDemandList[this.pickdemandindex].value;
			},
			PickerChangeMaster(e) {
				this.pickmasterindex = e.detail.value;
				this.searchDataDto.ReqBody.IsMasterProductSysNo = this.ProductDemandList[this.pickmasterindex].value;
			}
		}
	};
</script>

<style>
	.page {
		height: 100vh;
		width: 100vw;
	}

	.page.show {
		overflow: hidden;
	}

	.switch-sex::after {
		content: '\e716';
	}

	.switch-sex::before {
		content: '\e7a9';
	}

	.switch-music::after {
		content: '\e66a';
	}

	.switch-music::before {
		content: '\e6db';
	}

	.menu_box {
		position: fixed;
		bottom: 0rpx;
		width: 100%;
		/* background-color: red; */
		text-align: center;
	}

	.cb {
		transform: scale(0.6, 0.6);
	}
</style>
