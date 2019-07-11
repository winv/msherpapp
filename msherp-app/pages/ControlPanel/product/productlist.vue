<template>
	<view>
		<cu-custom bgColor="bg-gradual-pink" :isBack="true">
			<block slot="backText">返回</block>
			<block slot="content">商品列表</block>
		</cu-custom>
		<view class="cu-bar bg-white search fixed" :style="[{ top: CustomBar + 'px' }]">
			<view class="search-form round">
				<text class="cuIcon-search"></text>
				<input type="text" @input="changeType" v-model="searchDataDto.ReqBody.serachValue" placeholder="支持商品号,编号,名称"
				 confirm-type="search" />
			</view>
			<view class="action">
				<button class="cu-btn bg-gradual-green shadow-blur round" @tap="handleSubmit">搜索</button>
				<button class="cu-btn bg-gradual-red shadow-blur round" @tap="handleSubmitAdv" data-target="DialogModal2">选项</button>
			</view>
		</view>
		<view class="cu-bar bg-white solid-bottom margin-top">
			<view class="action"></view>
		</view>
		<scroll-view scroll-y class="page" :style="[{ top: CustomBar + 'px' }]">
			<view class="cu-bar bg-white solid-bottom margin-top" v-if="!loadMore.isfetchData">
				<view class="action">请输入条件查询数据！</view>
			</view>
			<view v-for="(item, index) in productList" :key="index">
				<view class="cu-bar bg-white solid-bottom margin-top">
					<view class="action">
						<text class="cuIcon-titles text-orange"></text>
						<navigator navigateTo :url="'/pages/ControlPanel/product/productdetail?productsysno=' + item.ProductSysNo">
							<view>{{ item.ProductSysNo }}-{{ item.ProductName }}-{{ item.ProductID }}</view>
						</navigator>
					</view>
					<view class="padding-sm margin-xs radius">
						<text v-if="true" class="lg text-red" :class="'cuIcon-cartfill'" @tap="showCart(item)" data-target="DialogCartModal"></text>
					</view>
				</view>
				<view class="cu-card article no-card">
					<view class="cu-item shadow">
						<view class="content">
							<view class="padding-sm flex flex-wrap">
								<view class="padding-xs">
									<view class="cu-tag radius bg-grey sm">{{ item.d0 }}</view>
									<view class="cu-tag radius bg-grey sm">{{ item.d1 }}</view>
									<view class="cu-tag radius bg-grey sm">{{ item.d2 }}</view>
									<view class="cu-tag radius bg-grey sm">{{ item.d3 }}</view>
									<view class="cu-tag radius bg-grey sm">{{ item.d4 }}</view>
									<view class="cu-tag radius bg-grey sm">{{ item.d5 }}</view>
									<view class="cu-tag radius bg-grey sm">{{ item.d6 }}</view>
									<view class="cu-tag radius bg-grey sm">{{ item.d7 }}</view>
								</view>
								<view class="padding-xs">
									<view class="cu-capsule radius">
										<view class="cu-tag bg-brown sm">app价</view>
										<view class="cu-tag line-brown sm">{{ item.AppBasicPrice }}</view>
									</view>
									<view class="cu-capsule radius">
										<view class="cu-tag bg-brown sm">门店价</view>
										<view class="cu-tag line-brown sm">{{ item.StockCurrentPrice }}</view>
									</view>
									<view class="cu-capsule radius">
										<view class="cu-tag bg-brown sm">门店会员价</view>
										<view class="cu-tag line-brown sm">{{ item.StockBasicPrice }}</view>
									</view>
									<view class="cu-capsule radius">
										<view class="cu-tag bg-brown sm">成本</view>
										<view class="cu-tag line-brown sm">{{ item.Unicost }}</view>
									</view>
								</view>
								<view class="padding-xs">
									<view class="cu-capsule radius">
										<view class="cu-tag bg-brown sm">公司</view>
										<view class="cu-tag line-brown sm">{{ item.CompanyStock }}</view>
									</view>
									<view class="cu-capsule radius">
										<view class="cu-tag bg-brown sm">总仓</view>
										<view class="cu-tag line-brown sm">{{ item.WareHouseStock }}</view>
									</view>
									<view class="cu-capsule radius">
										<view class="cu-tag bg-brown sm">门店</view>
										<view class="cu-tag line-brown sm">{{ item.AllStoreNum }}</view>
									</view>
									<view class="cu-capsule radius">
										<view class="cu-tag bg-brown sm">财务</view>
										<view class="cu-tag line-brown sm">{{ item.Accountry }}</view>
									</view>
								</view>
								<view class="padding-xs">
									<view class="cu-tag bg-red light sm radius">{{ item.V_ProductStatusName }}</view>
									<view class="cu-tag bg-green light sm radius" v-if="item.ProductLeader !== ''">{{ item.ProductLeader }}</view>
								</view>
							</view>
						</view>
					</view>
				</view>
			</view>
			<view class="cu-load bg-blue" @tap="loadMoreData" v-if="loadMore.isShowMore" :class="loadMore.loadClass">{{ loadMore.loadText }}</view>
		</scroll-view>
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
					<view class="cu-form-group margin-top">
						<view class="title">仅限本人商品：</view>
						<switch @change="isselfcheck" :class="isself?'checked':''" :checked="isself"></switch>
					</view>
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
						<view class="title">采购总价：</view>
						<input type="text" v-model="PoBasketDto.ReqBodyModel.TotalAmt" />
					</view>
					<view class="cu-form-group">
						<view class="title">采购单价：</view>
						<input type="text" v-model="PoBasketDto.ReqBodyModel.OrderPrice" />
					</view>
				</view>
				<view class="cu-bar bg-white">
					<view class="action margin-0 flex-sub text-green solid-left" @tap="hideModal">取消</view>
					<view class="action margin-0 flex-sub  solid-left" @tap="computeUnitCost">计算单价</view>
					<view class="action margin-0 flex-sub  solid-left" @tap="addToPoBasket">加入购物篮</view>
				</view>
			</view>
		</view>
		<view class="menu_box" v-if="showCartInfo.showIcon">
			<navigator navigateTo :url="'/pages/ControlPanel/product/pobasket'">
				<view class='cu-avatar sm radius' style="background-image:url(/static/img/tab-cart-current.png)">
					<view class='cu-tag badge'>{{showCartInfo.count}}</view>
				</view>
			</navigator>
		</view>
	</view>
</template>

<script>
	import vueCommonData from '../../../config/VueCommonConstData.js';
	import product from '../../../service/product.service.js'
	import purchase from '../../../service/purchase.service.js'

	export default {
		data() {
			return {
				StatusBar: this.StatusBar,
				CustomBar: this.CustomBar,
				loadModal: false,
				loadMore: {
					isShowMore: false,
					loadClassList: ['', 'loading', 'over'],
					loadClass: '',
					loadText: '加载更多',
					isfetchData: false,
					isaddData: false
				},
				productList: [],
				searchResultData: {
					currentIndex: 1,
					totalCount: 100,
					PageSize: 10
				},
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
				userInfo: {},
				showCartInfo: {
					showIcon: false,
					count: 0
				},
				isself:true
			};
		},
		onLoad() {
			this.userInfo = uni.getStorageSync("MshUserSession");
			console.log(this.userInfo);
		},
		mixins: [vueCommonData],
		methods: {
			handleSubmit() {
				console.log('handleSubmit clicked');
				console.log(this.searchDataDto);
				this.searchDataDto.ReqBody.PageIndex = 0;
				this.loadMore.isfetchData = true;
				this.loadMore.isaddData = false;
				this.loadMore.isShowMore = true;
				console.log(this.isself)
				if(this.isself){
					this.searchDataDto.ReqBody.GroupSysNo = this.userInfo.User.SysNo||'';
				}else{
					this.searchDataDto.ReqBody.GroupSysNo = '';
				}
				this.fetchData();
				this.loadPobasketData();
			},
			isselfcheck(e){
				console.log(e)
				this.isself=e.detail.value
			},
			changeType(event) {
				var inputvalue = event.target.value;
				var hz = /^[\u4e00-\u9fa5]+$/; //汉字
				var zf = /^[A-Za-z].*/; //字符
				var sz = /^[0-9]*$/;
				var tm = /^(69|M).*/
				if (hz.test(inputvalue)) {
					this.searchDataDto.ReqBody.serachType = 1;
				}
				if (zf.test(inputvalue)) {
					this.searchDataDto.ReqBody.serachType = 4;
				}
				if (sz.test(inputvalue)) {
					if (inputvalue.length <= 6) {
						this.searchDataDto.ReqBody.serachType = 2;
					}
				}
				if (tm.test(inputvalue)) {
					if (inputvalue.length > 6) {
						this.searchDataDto.ReqBody.serachType = 5;
					}
				}
				if (inputvalue === "") {
					this.searchDataDto.ReqBody.serachType = 2;
				}
			},
			loadMoreData() {
				console.log(this.searchResultData);
				this.searchDataDto.ReqBody.PageIndex++;
				this.loadMore.loadClass = this.loadMore.loadClassList[1];
				this.loadMore.loadText = '';
				this.loadMore.fetchData = true;
				this.loadMore.isaddData = true;
				this.fetchData();
			},
			fetchData() {
				var self = this;
				product.QueryProductList(this.searchDataDto).then(res => {
					console.log(res);
					self.loadMore.loadClass = self.loadMore.loadClassList[0];
					if (res.Data.ReqBody.length == 0) {
						self.loadMore.loadClass = self.loadMore.loadClassList[2];
						self.loadMore.loadText = '';
					}
					if (self.loadMore.isaddData) {
						self.productList.concat(res.Data.ReqBody);
					} else {
						self.productList = res.Data.ReqBody;
					}
				});
			},
			showCart(obj) {
				this.modalName = 'DialogCartModal';
				console.log(obj);
				this.ProductInfo = obj;
				this.PoBasketDto.ReqBody.ProductSysNo = obj.ProductSysNo;
				this.PoBasketDto.ReqBody.CreateUserSysNo = this.userInfo.User.SysNo;
				this.PoBasketDto.ReqBodyModel.ProductSysNo = obj.ProductSysNo;
				this.PoBasketDto.ReqBodyModel.CreateUserSysNo = this.userInfo.User.SysNo
				console.log('cart clicked');
				purchase.QueryPoBaksetDetail(this.PoBasketDto).then(res => {
					console.log(res);
					if (res.Data.ReqBody[0] !== null) {
						var dataObj = res.Data.ReqBody[0];
						this.PoBasketDto.ReqBodyModel.Quantity = dataObj.Quantity;
						this.PoBasketDto.ReqBodyModel.OrderPrice = dataObj.OrderPrice;
					} else {
						this.PoBasketDto.ReqBodyModel.Quantity = '';
						this.PoBasketDto.ReqBodyModel.OrderPrice = '';
					}
				});
			},
			computeUnitCost(e) {
				var self = this;
				var totalamt= this.PoBasketDto.ReqBodyModel.TotalAmt;
				var currCost = totalamt/this.PoBasketDto.ReqBodyModel.Quantity
				this.PoBasketDto.ReqBodyModel.OrderPrice=currCost;
			},
			addToPoBasket(e) {
				var self = this;
				var lastCost = this.ProductInfo.BuyUniCost;
				var currCost = this.PoBasketDto.ReqBodyModel.OrderPrice;
				var costmsg = "";
				var duration = 500;
				if (currCost > lastCost) {
					costmsg = "当前采购成本：" + currCost + " 大于最后一次采购成本:" + lastCost + "，请确认采购价格！";
					duration = 4000
				}
				purchase.InsertPoBasket(this.PoBasketDto).then(res => {
					console.log(res);
					if (res.Status) {
						uni.showToast({
							icon: 'none',
							title: '采购篮添加成功!' + costmsg,
							duration: duration
						});
						this.modalName = '';
					} else {
						uni.showToast({
							icon: 'none',
							title: res.data.Message,
							duration: 3000
						});
					}
				});
				this.loadPobasketData();
			},
			loadPobasketData() {
				var self = this;
				this.PoBasketDto.ReqBody.CreateUserSysNo = this.userInfo.User.SysNo;
				purchase.GetPobasketCount(this.PoBasketDto).then(res => {
					console.log(res);
					if (res.Status && res.Data > 0) {
						self.showCartInfo.showIcon = true;
						self.showCartInfo.count = res.Data;
					} else {
						self.showCartInfo.showIcon = false;
					}
				})
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
		bottom: 150rpx;
		right: 30rpx;
		/* background-color: red; */
		text-align: center;
	}
</style>
