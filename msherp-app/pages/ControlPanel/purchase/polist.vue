<template>
	<view>
		<cu-custom bgColor="bg-gradual-pink" :isBack="true">
			<block slot="backText">返回</block>
			<block slot="content">采购单</block>
		</cu-custom>
		<view class="cu-bar bg-white search fixed" :style="[{ top: CustomBar + 'px' }]">
			<view class="search-form round">
				<text class="cuIcon-search"></text>
				<input type="text" v-model="searchDataDto.SysNo" placeholder="支持采购单号搜索" confirm-type="search" />
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
			<view class="cu-bar bg-white solid-bottom margin-top" v-if="!loadMore.isfetchData">
				<view class="action">请输入条件查询数据！</view>
			</view>
			<view class="cu-list menu" v-for="(item, index) in productList" :key="index">
				<view class="cu-item arrow">
					<view class="content">
						<navigator navigateTo :url="'/pages/ControlPanel/purchase/editpo?sysno=' + item.SysNo">
							<text class="cuIcon-titles text-orange"></text>
							<text>{{ item.SysNo }}-{{ item.V_VendorName }}</text>
						</navigator>
					</view>
				</view>

				<view class="cu-card article no-card">
					<view class="cu-item shadow">
						<view class="content">
							<view class="padding-sm flex flex-wrap">
								<view class="padding-xs">
									<view class="cu-tag radius bg-red">{{ item.V_Status }}</view>
									<view class="cu-tag radius bg-grey">{{ item.V_CreateName }}</view>
									<view class="cu-tag radius bg-blue">{{ item.V_StockName }}</view>
									<view class="cu-tag text-price radius bg-red">{{ item.TotalAmt }}</view>
									<view class="cu-tag radius bg-red">{{ item.CreateTime|formatShortDate }}</view>
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
						<view class="title">商品号：</view>
						<input type="text" v-model="searchDataDto.ProductSysNo" />
					</view>
					<view class="cu-form-group margin-top">
						<view class="title">采购状态</view>
						<picker @change="PickerStatusChange" :value="pickdemandindex" :range="PoStatusList" :range-key="'name'">
							<view class="picker">{{ PoStatusList[pickdemandindex].name }}</view>
						</picker>
					</view>
					<view class="cu-form-group margin-top">
						<view class="title">仅限本人创建：</view>
						<switch @change="isselfcheck" :class="isself?'checked':''" :checked="isself"></switch>
					</view>
					<view class="cu-form-group margin-top">
						<view class="title">供应商编号：</view>
						<input type="text" v-model="searchDataDto.VendorSysNo" />
					</view>
					<view class="cu-form-group margin-top">
						<view class="title">创建开始时间：</view>
						<picker mode="date" :value="searchDataDto.CreateTimeFrom" start="2019-01-01" end="2025-12-31" @change="DateChangeFrom">
							<view class="picker">{{ searchDataDto.CreateTimeFrom }}</view>
						</picker>
					</view>
					<view class="cu-form-group margin-top">
						<view class="title">创建结束时间：</view>
						<picker mode="date" :value="searchDataDto.CreateTimeTo" start="2019-01-01" end="2025-12-31" @change="DateChangeTo">
							<view class="picker">{{ searchDataDto.CreateTimeTo }}</view>
						</picker>
					</view>
				</view>
				<view class="cu-bar bg-white">
					<view class="action margin-0 flex-sub text-green solid-left" @tap="hideModal">取消</view>
					<view class="action margin-0 flex-sub  solid-left" @tap="hideModal">确定</view>
				</view>
			</view>
		</view>
	</view>
</template>
<script>
	import vueCommonData from '../../../config/VueCommonConstData.js';
	import purchase from '../../../service/purchase.service.js'
	import dateFormat from '../../../libs/dateformat.js'

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
				PoStatusList: [{
						name: '全部',
						value: ''
					},
					{
						name: '初始',
						value: '1'
					},
					{
						name: '已摊销',
						value: '2'
					},
					{
						name: '待入库',
						value: '3'
					},
					{
						name: '已入库',
						value: '4'
					},
					{
						name: '待收货',
						value: '5'
					},
					{
						name: '待上架',
						value: '6'
					},
					{
						name: '待PMD审核',
						value: '7'
					},
					{
						name: '待PML审核',
						value: '8'
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
					IsPaging: true,
					PageSize: 10,
					PageIndex: 1,
					Status: '',
					CreateTimeFrom: dateFormat.formatTimeToStr(dateFormat.AddDays(-15), 'yyyy-MM-dd'),
					CreateTimeTo: dateFormat.formatTimeToStr(dateFormat.AddDays(0), 'yyyy-MM-dd')
				},
				userInfo: {},
				isself: true,
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
				this.searchDataDto.PageIndex = 0;
				this.loadMore.isfetchData = true;
				this.loadMore.isaddData = false;
				this.loadMore.isShowMore = true;
				this.fetchData();

			},
			isselfcheck(e) {
				console.log(e)
				this.isself = e.detail.value
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
				var searchDataDto = self.baseRequestDto
				searchDataDto.ReqBody = self.searchDataDto;
				if (self.isself) {
					searchDataDto.ReqBody.CreateUserSysNo = self.userInfo.User.SysNo || ''
				} else {
					searchDataDto.ReqBody.CreateUserSysNo = ''
				}
				purchase.QueryPoList(searchDataDto).then(res => {
					console.log(res);
					self.loadMore.loadClass = self.loadMore.loadClassList[0];
					if (res.Data.ResMasterBody.length == 0) {
						self.loadMore.loadClass = self.loadMore.loadClassList[2];
						self.loadMore.loadText = '';
					}
					if (self.loadMore.isaddData) {
						self.productList.concat(res.Data.ResMasterBody);
					} else {
						self.productList = res.Data.ResMasterBody;
					}
				});
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
			PickerStatusChange(e) {
				this.pickdemandindex = e.detail.value;
				this.searchDataDto.Status = this.ProductDemandList[this.pickdemandindex].value;
			},
			DateChangeFrom(e) {
				this.searchDataDto.CreateTimeFrom = e.detail.value;
			},
			DateChangeTo(e) {
				this.searchDataDto.CreateTimeTo = e.detail.value;
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
