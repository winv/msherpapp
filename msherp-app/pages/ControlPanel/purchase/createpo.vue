<template>
	<view>
		<cu-custom bgColor="bg-gradual-pink" :isBack="true">
			<block slot="backText">返回</block>
			<block slot="content">新建采购单</block>
		</cu-custom>
		<!--采购单主界面-->
		<scroll-view scroll-y class="page" :style="[{ top: CustomBar + 'px' }]">
			<view class="cu-list menu-avatar">
				<view class="cu-form-group">
					<view class="title">采购单号：</view>
					<input type="text" v-model="PoMasterInfo.SysNo" placeholder="自动,无需填写" />
				</view>
				<view class="cu-form-group">
					<view class="title">供应商：</view>
					<input type="text" v-model="PoMasterInfo.VendorTextInfo" v-validate:PoMasterInfo.VendorTextInfo="['required']"
					 placeholder="可以直接输入供应系统号" />
					<text class="lg text-red" :class="'cuIcon-filter'" @tap="ShowVendorIndexList"></text>
				</view>
				<view class="cu-form-group">
					<view class="title">收货仓库：</view>
					<picker @change="PickerChange" :value="pickindex" :range="StockList" :range-key="'name'">
						<view class="picker pull-left">{{ StockList[pickindex].name }}</view>
					</picker>
				</view>
				<view class="cu-form-group">
					<view class="title">支付时间：</view>
					<picker mode="date" :value="PoMasterInfo.PayDate" start="2019-01-01" end="2025-12-31" @change="DateChange">
						<view class="picker">{{ PoMasterInfo.PayDate }}</view>
					</picker>
				</view>
				<view class="cu-form-group">
					<view class="title">对外备注：</view>
					<input type="text" v-model="PoMasterInfo.Note" />
				</view>
				<view class="cu-form-group">
					<view class="title">对内备注：</view>
					<input type="text" v-model="PoMasterInfo.Memo" />
				</view>
				<view class="cu-form-group">
					<view class="title">带票类型：</view>
					<picker @change="PickerInvoiceChange" :value="invoiceIndex" :range="POInvoiceTypeList" :range-key="'name'">
						<view class="picker pull-left">{{ POInvoiceTypeList[invoiceIndex].name }}</view>
					</picker>
				</view>
				<view class="cu-form-group">
					<view class="title">税点：</view>
					<input type="text" v-model="PoMasterInfo.POInvoiceTen" />
					<text>%</text>
				</view>
				<view class="cu-form-group">
					<view class="title">支付类型：</view>
					<picker @change="PickerPayTypeChange" :value="pickPayIndex" :range="PayDateTypeList" :range-key="'name'">
						<view class="picker pull-left">{{ PayDateTypeList[pickPayIndex].name }}</view>
					</picker>
				</view>
				<view class="cu-form-group">
					<view class="title">截止到货：</view>
					<picker mode="date" :value="PoMasterInfo.ExpectInTime" start="2018-01-01" end="2025-12-31" @change="arriveDateChange">
						<view class="picker">{{ PoMasterInfo.ExpectInTime }}</view>
					</picker>
				</view>
			</view>
			<!--table 采购商品列表-->
			<view :class="editPoItem.showView?'bright789_view_show':'bright789_view_hide'">
				<view class="table">
					<z-table :tableData="basePoItemList" :columns="basePoItemColumns" :showLoading='false' :showBottomSum='true'></z-table>
				</view>
			</view>
			<!--可编辑列表 table暂时不支持行单击事件-->
			<view class="cu-list card-menu menu-avatar sm" :class="!editPoItem.showView?'bright789_view_show':'bright789_view_hide'">
				<view v-for="(item, index) in basePoItemList" :key="index">
					<view class="cu-bar bg-white solid-bottom margin-top">
						<view class="action">
							<text class="cuIcon-titles text-orange" v-if="false"></text>
							<view @tap="showCart(item)" data-target="DialogCartModal">{{ item.ProductSysNo }}-{{ item.ProductName }}</view>
						</view>
						<view class="padding-sm margin-xs radius">
							<text class="lg text-gray" :class="'cuIcon-deletefill'" @tap="deletePoItem(item,index)"></text>
						</view>
					</view>
					<view class="cu-card article no-card">
						<view class="cu-item shadow">
							<view class="content">
								<view class="padding-sm flex flex-wrap">
									<view class="padding-xs">
										<view class="cu-capsule radius">
											<view class="cu-tag bg-brown xl radius">数量</view>
											<view class="cu-tag line-brown basis-xs radius"><input type="text" v-model="basePoItemList[index].OrderQty" /></view>
											<view class="basis-xs"></view>
											<view class="cu-tag bg-brown xl radius">价格</view>
											<view class="cu-tag line-brown basis-xs radius"><input type="text" v-model="basePoItemList[index].OrderPrice" /></view>
										</view>
									</view>
								</view>
							</view>
						</view>
					</view>
				</view>
			</view>
		</scroll-view>
		<view class="bg-white padding-xl"></view>
		<!--底部操作条-->
		<view class="cu-bar bg-white tabbar border shop fixedbottom">
			<view class="action" @tap="showCartListRight">
				<view class="cuIcon-cart">
					<view class="cu-tag badge">{{ pobasketcount }}</view>
				</view>
				从采购篮添加
			</view>
			<view class="btn-group">
				<button class="cu-btn bg-orange round shadow-blur" @tap="CreateToPoMaster">创建采购单</button>
				<button class="cu-btn bg-red round shadow-blur" @tap="addProduct" v-if='isShowAdd'>添加商品</button>
				<button class="cu-btn bg-red round shadow-blur" @tap="editProduct">{{editPoItem.text}}</button>
			</view>
		</view>
		<!--直接添加商品-->
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
						<input class="action" type="text" v-model="PoBasketDto.ReqBodyModel.OrderQty" />
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
		<!--侧边抽屉 从采购篮添加-->
		<view class="cu-modal drawer-modal justify-end" :class="modalName=='DrawerModalR'?'show':''" @tap="hideModal">
			<view class="cu-dialog basis-lg" @tap.stop="" :style="[{top:CustomBar+'px',height:'calc(100vh - ' + CustomBar + 'px)'}]">
				<view class="cu-list menu text-left">
					<view class="cu-item " v-for="(item,index) in cartList" :key="index">
						<view class="content">
							<view>{{item.ProductSysNo}}{{item.V_ProductName}}-单价{{item.OrderPrice}},数量{{item.Quantity}}</view>
						</view>
						<text class="lg text-red" :class="'cuIcon-roundaddfill'" @tap="addToPoFromBasketInfo(item)"></text>
					</view>
				</view>
			</view>
		</view>
	</view>
</template>
<script>
	import purchase from '../../../service/purchase.service.js';
	import zTable from '../../../components/z-table/z-table.vue';
	import dateFormat from '../../../libs/dateformat.js';
	import columns from '../../../config/columns.js';
	import vueCommonData from '../../../config/VueCommonConstData.js';
	export default {
		components: {
			zTable
		},
		data() {
			return {
				StatusBar: this.StatusBar,
				CustomBar: this.CustomBar,
				pomaster: {
					reqbody: {
						VendorTextInfo: ''
					}
				},
				searchDataDto: {
					reqbody: {
						SysNo: '',
						ProductSysNo: ''
					}
				},
				PoMasterInfo: {
					SysNo: -999999,
					PayDate: dateFormat.formatTimeToStr(dateFormat.AddDays(1), 'yyyy-MM-dd'),
					ExpectInTime: dateFormat.formatTimeToStr(dateFormat.AddDays(3), 'yyyy-MM-dd'),
					POInvoiceType: 0,
					POInvoiceTen: 0,
					PayDateType: 0,
					StockSysNo: 6,
					VendorTextInfo: '',
					VendorSysNo: 0,
					CreateUserSysNo: 0,
					poItems: []
				},
				basketItemList: [],
				ProductInfo: {},
				userInfo: {},
				StockList: [{
					name: '上海总仓',
					value: '6'
				}],
				isShowAdd: false,
				pickindex: 0,
				basePoItemList: [],
				basePoItemColumns: [],
				baseHtml: '<z-table :tableData="baseTableData" :columns="baseColumns"></z-table>',
				editPoItem: {
					isedit: false,
					text: '编辑商品',
					showView: true
				},
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
				cartList: []
			};
		},
		mixins: [vueCommonData],
		onLoad(options) {
			this.userInfo = uni.getStorageSync('MshUserSession');
			this.initParam(options);
		},
		onShow() {
			var vinfo = uni.getStorageSync('vendorinfo');
			if (vinfo !== '') {
				this.PoMasterInfo.VendorTextInfo = vinfo.SysNo + '-' + vinfo.VendorName;
				this.PoMasterInfo.VendorSysNo = vinfo.SysNo;
				uni.removeStorageSync('vendorinfo');
			}
		},
		mounted() {
			if (this.basketItemList.length > 0) {
				this.initBasketData();
			}
			this.basePoItemColumns = columns.PoItemCol(this);
			this.PoMasterInfo.CreateUserSysNo = this.userSession.User.SysNo;
			this.initData();
		},
		methods: {
			initParam(options) {
				var data = options || '';
				if (Object.keys(data).length > 0) {
					var sysnos = [];
					var productsysnos = [];
					this.basketItemList = JSON.parse(options.data);
					this.basketItemList.forEach(item => {
						sysnos.push(item.SysNo);
						productsysnos.push(item.ProductSysNo);
					});
					this.searchDataDto.reqbody.SysNo = sysnos.join(',');
					this.searchDataDto.reqbody.ProductSysNo = productsysnos.join(',');
					this.isShowAdd = false;
					console.log(this.basketItemList);
				}
			},
			initBasketData() {
				var self = this;
				purchase.getPoBasketItem(this.searchDataDto).then(res => {
					console.log(res);
					self.basePoItemList = res.Data.ResBody;
					self.PoMasterInfo.poItems = self.basePoItemList;
					console.log(self.basePoItemList);
				});
			},
			initData() {
				var self = this;
				var reqdto = this.baseRequestDto;
				reqdto.ReqBody.CreateUserSysNo = this.userSession.User.SysNo;
				reqdto.ReqBody.StockSysNo = 6;
				purchase.GetPobasketCount(reqdto).then(res => {
					console.log(res);
				});
				//初始化采购篮商品拉取
				this.PoBasketDto.ReqBody.CreateUserSysNo = this.userSession.User.SysNo;
				purchase.QueryPoBaksetList(this.PoBasketDto).then(res => {
					console.log(res);
					self.cartList = res.Data.ReqBody;
					self.pobasketcount = self.cartList.length;
				})
			},
			addProduct() {
				this.modalName = 'DialogCartModal';
			},
			editProduct() {
				this.editPoItem.showView = !this.editPoItem.showView;
				if (this.editPoItem.showView) {
					this.editPoItem.text = "编辑商品"
				} else {
					this.editPoItem.text = "完成编辑"
				}
			},
			deletePoItem(item, index) {
				var self = this;
				uni.showModal({
					title: '提示',
					content: '确认删除已选商品吗？',
					success: function(res) {
						if (res.confirm) {
							console.log('confirmed')
							//如果是新建采购单 直接删除数组内容即可
							self.basePoItemList.splice(index, 1);
							//如果是已建采购单编辑 删除逻辑需要调用服务从采购单子表中删除

						} else if (res.cancel) {
							console.log('canceld')
						}
					}
				})
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
			addToPoFromBasketInfo(item) {
				console.log(item)
				var sysno = this.searchDataDto.reqbody.SysNo === '' ? item.SysNo : this.searchDataDto.reqbody.SysNo + ',' + item.SysNo
				var productsysno = this.searchDataDto.reqbody.ProductSysNo === '' ? item.ProductSysNo : this.searchDataDto.reqbody.ProductSysNo +
					',' + item.ProductSysNo
				this.searchDataDto.reqbody.SysNo = sysno
				this.searchDataDto.reqbody.ProductSysNo = productsysno
				this.initBasketData()
			},
			CreateToPoMaster() {
				if (this.PoMasterInfo.VendorSysNo === 0) {
					uni.showToast({
						icon: 'none',
						title: '供应商必须选择！',
						duration: 1500
					})
					return;
				}
				this.baseRequestDto.ReqBodyDTO = this.PoMasterInfo;
				console.log(this.baseRequestDto);

				if (this.mshconfig.isWriteData) {
					purchase.CreateToPoMaster(this.baseRequestDto).then(res => {
						console.log(res)
						if (res.Data.RetrunMsg !== '') {
							uni.showToast({
								icon: 'none',
								title: res.Data.RetrunMsg,
								duration: 3000
							})
						} else {
							var sysno = res.Data.ResMasterBody[0].SysNo
							uni.redirectTo({
								url: '/pages/ControlPanel/purchase/editpo?sysno=' + sysno
							})
						}
					});
				} else {
					uni.showToast({
						icon: 'none',
						title: '非生产环境，为避免数据污染，不执行新建操作',
						duration: 1500
					})
				}
			},
			showCartListRight() {
				this.modalName = 'DrawerModalR'
			},
			PickerChange(e) {
				this.pickindex = e.detail.value;
				this.PoMasterInfo.VendorSysNo = this.ProductStatusList[this.pickindex].value;
			},
			ShowVendorIndexList(e) {
				uni.navigateTo({
					url: '../vendor/vendorindexlist'
				});
			},
			DateChange(e) {
				this.PoMasterInfo.PayDate = e.detail.value;
			},
			arriveDateChange(e) {
				this.PoMasterInfo.ExpectInTime = e.detail.value;
			},
			PickerInvoiceChange(e) {
				this.invoiceIndex = e.detail.value;
				this.PoMasterInfo.POInvoiceType = this.POInvoiceTypeList[this.invoiceIndex].value;
				if (e.detail.value === "2") {
					this.PoMasterInfo.POInvoiceTen = 17
				} else {
					this.PoMasterInfo.POInvoiceTen = 0
				}
			},
			PickerPayTypeChange(e) {
				this.pickPayIndex = e.detail.value;
				this.PoMasterInfo.PayDateType = this.PayDateTypeList[this.pickPayIndex].value;
			}
		}
	};
</script>
<style>
	.bright789_view_hide {
		display: none;
	}

	.bright789_view_show {
		display: block;
	}
</style>
