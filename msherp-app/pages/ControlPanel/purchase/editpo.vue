<template>
	<view>
		<cu-custom bgColor="bg-gradual-pink" :isBack="true">
			<block slot="backText">返回</block>
			<block slot="content">采购单编辑</block>
		</cu-custom>
		<view class="bg-white padding">
			<view class="cu-steps">
				<view v-if='PoMasterInfo.Status===0' class="cu-item" :class="index>PoMasterInfo.Status?'':'text-blue'" v-for="(item,index) in numList"
				 :key="index">
					<text class="num" :class="index==0?'err':''" :data-index="index + 1"></text> {{item.name}}
				</view>
				<view v-if='PoMasterInfo.Status!==0' class="cu-item" :class="item.value===PoMasterInfo.Status?'text-blue':''" v-for="(item,index) in numList2"
				 :key="index">
					<text class="num" :data-index="index + 1"></text> {{item.name}}
				</view>
			</view>
		</view>
		<view v-if="true">
			<scroll-view :scroll-y="modalName == null" class="page" :class="modalName != null ? 'show' : ''" :style="[{ top: CustomBar + 'px' }]">
				<!--采购单主界面-->
				<view class="cu-list menu-avatar">
					<view class="cu-form-group">
						<view class="title">采购单号：</view>
						<input type="text" v-model="PoMasterInfo.SysNo" :disabled="true" placeholder="自动,无需填写" />
					</view>
					<view class="cu-form-group">
						<view class="title">供应商：</view>
						<input type="text" v-model="PoMasterInfo.VendorTextInfo" v-validate:PoMasterInfo.VendorTextInfo="['required']"
						 placeholder="可以直接输入供应系统号" :disabled="true" />
						<text v-if="false" class="lg text-red" :class="'cuIcon-filter'" @tap="ShowVendorIndexList"></text>
					</view>
					<view class="cu-form-group">
						<view class="title">收货仓库：</view>
						<picker @change="PickerChange" :value="pickindex" :range="StockList" :range-key="'name'" :disabled="!isinitStatus">
							<view class="picker pull-left">{{ StockList[pickindex].name }}</view>
						</picker>
					</view>
					<view class="cu-form-group">
						<view class="title">支付时间：</view>
						<picker mode="date" :disabled="!isinitStatus" :value="PoMasterInfo.PayDate" start="2019-01-01" end="2025-12-31"
						 @change="DateChange">
							<view class="picker">{{ PoMasterInfo.PayDate }}</view>
						</picker>
					</view>
					<view class="cu-form-group">
						<view class="title">对外备注：</view>
						<input type="text" v-model="PoMasterInfo.Memo" />
					</view>
					<view class="cu-form-group">
						<view class="title">对内备注：</view>
						<input type="text" v-model="PoMasterInfo.Note" />
					</view>
					<view class="cu-form-group">
						<view class="title">带票类型：</view>
						<picker :disabled="!isinitStatus" @change="PickerInvoiceChange" :value="invoiceIndex" :range="POInvoiceTypeList"
						 :range-key="'name'">
							<view class="picker pull-left">{{ POInvoiceTypeList[invoiceIndex].name }}</view>
						</picker>
					</view>
					<view class="cu-form-group">
						<view class="title">支付类型：</view>
						<picker :disabled="!isinitStatus" @change="PickerPayTypeChange" :value="pickPayIndex" :range="PayDateTypeList"
						 :range-key="'name'">
							<view class="picker pull-left">{{ PayDateTypeList[pickPayIndex].name }}</view>
						</picker>
					</view>
					<view class="cu-form-group">
						<view class="title">截止到货：</view>
						<picker :disabled="!isinitStatus" mode="date" :value="PoMasterInfo.ExpectInTime" start="2018-01-01" end="2025-12-31"
						 @change="arriveDateChange">
							<view class="picker">{{ PoMasterInfo.ExpectInTime }}</view>
						</picker>
					</view>
				</view>
				<!--table 采购商品列表-->
				<view class="cu-list sm margin-bottom-xl" :class="editPoItem.showView?'bright789_view_show':'bright789_view_hide'">
					<view class="table">
						<z-table :tableData="basePoItemList" :columns="basePoItemColumns" :showLoading='false' :showBottomSum='true'></z-table>
					</view>
				</view>
				<!--可编辑列表 table暂时不支持行单击事件-->
				<view class="cu-list card-menu menu-avatar sm margin-bottom-xl" :class="!editPoItem.showView?'bright789_view_show':'bright789_view_hide'">
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
												<view class="cu-tag line-brown basis-xs radius"><input type="text" v-model='basePoItemList[index].OrderQty' /></view>
												<view class="basis-xs"></view>
												<view class="cu-tag bg-brown xl radius">价格</view>
												<view class="cu-tag line-brown basis-xs radius">
													<input type="text" v-model='basePoItemList[index].OrderPrice' />
												</view>
											</view>
										</view>
									</view>
								</view>
							</view>
						</view>
					</view>
				</view>
			</scroll-view>
			<!--底部空白占位附，为了解决fixed 遮挡内容问题，实际应该由其他解决办法-->
			<view class="bg-white padding"></view>
			<!--非初始状态其他操作按钮-->
			<view v-if="!isinitStatus" class="cu-bar bg-white tabbar border shop fixedbottom">
				<view class="btn-group" v-if="PoMasterInfo.Status===2">
					<button class="cu-btn bg-blue round shadow-blur" @tap="CancelverifyPo">取消摊销</button>
				</view>
				<view class="btn-group" v-if="PoMasterInfo.Status===2">
					<button class="cu-btn bg-red round shadow-blur" @tap="verifyPo">审核</button>
				</view>
				<view class="btn-group" v-if="PoMasterInfo.Status===0">
					<button class="cu-btn bg-red round shadow-blur" @tap="cancelabandonPo">取消作废</button>
				</view>
			</view>
			<!--底部操作条-->
			<view v-if="isinitStatus" class="cu-bar bg-white tabbar border shop fixedbottom">
				<view class="action" @tap="showCartListRight">
					<view class="cuIcon-cart">
						<view class="cu-tag badge">{{ pobasketcount }}</view>
					</view>
					从采购篮添加
				</view>
				<view class="btn-group">
					<button class="cu-btn bg-orange round shadow-blur" @tap="UpdateToPoMaster" v-if='false'>更新采购单</button>
					<button class="cu-btn bg-red round shadow-blur" @tap="editProduct">{{editPoItem.text}}</button>
					<button class="cu-btn bg-yellow round shadow-blur" @tap="submitEditListData" v-if='!editPoItem.showView'>提交更改</button>
					<button class="cu-btn bg-red round shadow-blur" @tap="shareCost">摊销</button>
					<button class="cu-btn bg-gradual-purple round shadow-blur" @tap="verifyPo">审核</button>
					<button class="cu-btn bg-gradual-purple round shadow-blur" @tap="abandonPo">作废</button>
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
			<!--摊销模态框-->
			<view class="cu-modal" :class="modalName == 'DialogshareCostModal' ? 'show' : ''">
				<view class="cu-dialog pull-left">
					<view class="cu-bar bg-white justify-end">
						<view class="content">摊销</view>
						<view class="action" @tap="hideModal"><text class="cuIcon-close text-red"></text></view>
					</view>
					<view class="cu-list menu-avatar no-card">
						<view class="cu-form-group solid-bottom sm">
							<view class="title">摊销科目：</view>
							<picker @change="PickerSubjectChange" :value="shareCostData.subjectindex" :range="shareCostData.subject"
							 :range-key="'name'">
								<view class="picker">{{ shareCostData.subject[shareCostData.subjectindex].name }}</view>
							</picker>
						</view>
						<view class="cu-form-group solid-bottom sm">
							<view class="title">摊销类型：</view>
							<picker @change="PickerTypeChange" :value="shareCostData.typeindex" :range="shareCostData.type" :range-key="'name'">
								<view class="picker">{{ shareCostData.type[shareCostData.typeindex].name }}</view>
							</picker>
						</view>
						<view class="cu-form-group solid-bottom sm">
							<view class="title text-sm">摊销费用：</view>
							<input type="text" v-model="shareCostData.shareItem.ExpenseAmt" />
						</view>
						<view class="cu-form-group ">
							<view class="title text-sm">摊销费用明细，如果不摊销，请取消勾选</view>
						</view>
						<view v-for="(item,index) in basePoItemList" :key="index">
							<view class="bg-white solid-bottom">
								<view class="text-left text-sm padding-xs">
									<checkbox :checked="true" class="checkbox-sm"></checkbox>{{item.ProductSysNo}}-{{item.ProductName}}
								</view>
							</view>
						</view>
						<view v-for="(item,index) in sharelist" :key="index">
							<view class="bg-white solid-bottom">
								<view class="text-left text-sm padding-xs" @tap="deleteCostItem(index)">
									<text class="lg text-red" :class="'cuIcon-deletefill'"></text>
									<text>{{item.sname}}  {{item.tname}}  摊销  {{item.ExpenseAmt}} </text>
								</view>
							</view>
						</view>
					</view>
					<view class="cu-bar bg-white">
						<view class="action margin-0 flex-sub solid-left" @tap="hideModal">取消</view>
						<view class="action margin-0 flex-sub text-blue solid-left" @tap="toShare">摊销</view>
						<view class="action margin-0 flex-sub text-bold text-red  solid-left" @tap="toShareSave">提交保存</view>
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
	var Enumerable = require('linq');
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
				SysNo: 0,
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
					PayDateType: 0,
					StockSysNo: 6,
					VendorTextInfo: '',
					VendorSysNo: 0,
					CreateUserSysNo: 0,
					poItems: [],
					Status: 1
				},
				basketItemList: [],
				ProductInfo: {},
				userInfo: {},
				StockList: [{
					name: '上海总仓',
					value: '6'
				}],
				poMaster: {},
				isShowAdd: true,
				pickindex: 0,
				basePoItemList: [],
				basePoItemColumns: [],
				baseHtml: '<z-table :tableData="baseTableData" :columns="baseColumns"></z-table>',
				editPoItem: {
					isedit: false,
					text: '编辑模式',
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
				num: 0,
				numList: [{
						name: '已作废',
						value: 0
					},
					{
						name: '初始',
						value: 1
					},
					{
						name: '已摊销',
						value: 2
					}, {
						name: '待收货',
						value: 5
					}, {
						name: '已入库',
						value: 4
					},
				],
				numList2: [{
						name: '初始',
						value: 1
					},
					{
						name: '已摊销',
						value: 2
					}, {
						name: '待收货',
						value: 5
					}, {
						name: '已入库',
						value: 4
					},
				],
				cartList: [],
				isinitStatus: true,
				shareCostData: {
					type: [{
						name: '按金额',
						value: 0
					}, {
						name: '按数量',
						value: 1
					}],
					subject: [{
						name: '运费',
						value: 1
					}, {
						name: '装车费',
						value: 2
					}, {
						name: '耗材费	',
						value: 3
					}, {
						name: '人工费',
						value: 4
					}],
					shareItem: {
						ApportionType: 0,
						ApportionSubjectSysNo: 1,
						POSysNo:0,
						ExpenseAmt: 0,
						sname: '运费',
						tname: '按金额',
					},
					subjectindex: 0,
					typeindex: 0
				},
				sharelist: [],
				verifyContent:{}
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
			this.basePoItemColumns = columns.PoItemMainCol(this);
			this.PoMasterInfo.CreateUserSysNo = this.userSession.User.SysNo;
			this.initData();
		},
		methods: {
			initParam(options) {
				var data = options || '';
				console.log(options.sysno)
				this.SysNo = options.sysno || 0
			},
			initData() {
				var self = this;
				var reqdto = this.baseRequestDto;
				reqdto.ReqBody.CreateUserSysNo = this.userSession.User.SysNo;
				reqdto.ReqBody.SysNo = this.SysNo;
				purchase.QueryPoMaster(reqdto).then(res => {
					this.poMaster = res;
					console.log(res);
					this.num = this.poMaster.Data.ResMasterBody[0].Status - 1
					this.PoMasterInfo = this.poMaster.Data.ResMasterBody[0]
					if (this.PoMasterInfo.Status !== 1) {
						this.isinitStatus = false
					}
					else{
						this.isinitStatus=true
					}
					this.PoMasterInfo.PayDate = dateFormat.formatTimeToStr(this.PoMasterInfo.PayDate, 'yyyy-MM-dd')
					this.PoMasterInfo.ExpectInTime = dateFormat.formatTimeToStr(this.PoMasterInfo.ExpectInTime, 'yyyy-MM-dd hh:mm')
					this.PoMasterInfo.VendorTextInfo = this.PoMasterInfo.VendorSysNo + '-' + this.PoMasterInfo.V_VendorName;
					this.basePoItemList = this.PoMasterInfo.poItems
					console.log(this.PoMasterInfo)
				}).then(res => {
					console.log('this is second then')
					//根据采购单状态 否显示采购篮
					var poinfo = this.PoMasterInfo;
					if (poinfo.Status === 1) {
						this.PoBasketDto.ReqBody.CreateUserSysNo = this.userSession.User.SysNo;
						console.log(this.PoBasketDto)
						purchase.QueryPoBaksetList(this.PoBasketDto).then(res => {
							console.log(res);
							self.cartList = res.Data.ReqBody;
							self.pobasketcount = self.cartList.length;
						})
					}
				})
			},
			addProduct() {
				this.modalName = 'DialogCartModal';
			},
			editProduct() {
				this.editPoItem.showView = !this.editPoItem.showView;
				if (this.editPoItem.showView) {
					this.editPoItem.text = "编辑模式"
					console.log('应该是这里提交数据')

				} else {
					this.editPoItem.text = "列表模式"
				}
			},
			submitEditListData() {
				var self = this
				//删除物理数据
				var searchDto = {
					reqbody: {},
					ReqBodyDTO: {}
				}
				searchDto.ReqBodyDTO = self.PoMasterInfo
				console.log(searchDto)
				purchase.UpdatePoitem(searchDto).then(res => {
					console.log(res)
					if (res.Data.RetrunMsg !== '') {
						uni.showToast({
							icon: 'none',
							title: res.Data.RetrunMsg,
							duration: 3000
						})
					}
				})
			},
			deletePoItem(item, index) {
				var self = this;
				uni.showModal({
					title: '提示',
					content: '确认删除已选商品吗？',
					success: function(res) {
						if (res.confirm) {
							console.log('confirmed')
							//从数组删除，不用再重新读取服务端数据
							self.basePoItemList.splice(index, 1);
							self.confirmDeletePoItem(item)
						} else if (res.cancel) {
							console.log('canceld')
						}
					}
				})
			},
			confirmDeletePoItem(item, isshow) {
				var isshowmsg = isshow || true
				var self = this
				//删除物理数据
				var searchDto = {
					reqbody: {},
					ReqBodyDTO: {}
				}
				searchDto.reqbody.ProductSysNo = item.ProductSysNo
				searchDto.ReqBodyDTO = self.PoMasterInfo
				console.log(searchDto)
				purchase.DeletePoItem(searchDto).then(res => {
					console.log(res)
					if (isshowmsg) {
						uni.showToast({
							title: '删除成功',
							icon: 'none'
						})
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
				var self = this
				var existitem = Enumerable.from(this.basePoItemList).where(function(x) {
					return x.ProductSysNo === item.ProductSysNo
				}).toArray();
				if (existitem.length > 0) {
					uni.showModal({
						title: '提示',
						content: '该商品已存在，继续操作则会删除原商品重新添加，返回请按取消',
						success: function(res) {
							if (res.confirm) {
								//确认后，先删除，再继续添加商品
								self.confirmDeletePoItem(item, false)
								self.confirmAddPoItem(item)
							} else if (res.cancel) {
								console.log('cancel clicked')
							}
						}
					})
				} else {
					this.confirmAddPoItem(item)
				}
			},
			confirmAddPoItem(item) {
				var searchDto = {
					reqbody: {}
				}
				var sysno = item.SysNo
				var productsysno = item.ProductSysNo
				searchDto.reqbody.SysNo = sysno
				searchDto.reqbody.ProductSysNo = productsysno
				var self = this
				var oldlist = self.PoMasterInfo.poItems;
				var iscontinue = true
				purchase.getPoBasketItem(searchDto).then(res => {
					console.log(res);
					//self.basePoItemList = res.Data.ResBody;
					self.PoMasterInfo.poItems = res.Data.ResBody;
					console.log(self.basePoItemList);
				}).then(res2 => {
					//直接操作采购单子表
					this.baseRequestDto.ReqBodyDTO = this.PoMasterInfo;
					console.log(this.baseRequestDto)
					if (this.mshconfig.isWriteData) {
						purchase.InsertPoItem(this.baseRequestDto).then(res => {
							if (res.Data.RetrunMsg !== '') {
								uni.showToast({
									icon: 'none',
									title: res.Data.RetrunMsg,
									duration: 3000
								})
								iscontinue = false
							}
							console.log(res)
						}).then(res => {
							// console.log(iscontinue)
							if (iscontinue) {
								this.initData()
							} else {
								self.basePoItemList = oldlist
								self.PoMasterInfo.poItems = oldlist
							}
						});
					} else {
						uni.showToast({
							icon: 'none',
							title: '非生产环境，为避免数据污染，不执行新建操作',
							duration: 1500
						})
					}
				});
			},
			UpdateToPoMaster() {
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
					purchase.UpdateToPoMaster(this.baseRequestDto).then(res => {
						if (res.Data.RetrunMsg !== '') {
							uni.showToast({
								icon: 'none',
								title: res.Data.RetrunMsg,
								duration: 3000
							})
						}
						console.log(res)
					});
				} else {
					uni.showToast({
						icon: 'none',
						title: '非生产环境，为避免数据污染，不执行新建操作',
						duration: 1500
					})
				}
			},
			shareCost() {
				this.modalName = 'DialogshareCostModal'
			},
			toShare() {
				var self = this
				if(self.shareCostData.shareItem.ExpenseAmt<=0){
					uni.showToast({
						title: '摊销费用必须>0',
						icon: 'none',
						duration: 1000
					})
					return;
				}
				
				if (self.sharelist.length !== 0) {
					var checkresult = Enumerable.from(self.sharelist).where(function(item) {
						return item.subject === self.shareCostData.shareItem.subject
					}).toArray()
					if (checkresult.length !== 0) {
						uni.showToast({
							title: '改摊销科目已存在，请先删除',
							icon: 'none',
							duration: 1000
						})
						return;
					}
				}
				self.shareCostData.shareItem.POSysNo = this.SysNo
				var itemlist = []
				self.basePoItemList.forEach(function(item){
					var itemobj = {productsysno:item.ProductSysNo,ApportionSysNo:0,POSysNo:self.SysNo}
					itemlist.push(itemobj)
				})
				var obj = Object.assign({},self.shareCostData.shareItem)
				obj.itemlist = itemlist
				self.sharelist.push(obj)
				uni.showToast({
					title:'注意:最后须提交保存，摊销方可生效',
					icon:'none',
				})
			},
			toShareSave(){
				var self=this
				this.PoMasterInfo.appList = this.sharelist
				var dto=this.baseRequestDto;
				dto.ReqBodyDTO=this.PoMasterInfo
				purchase.POApportionCost(dto).then(res => {
					console.log(res);
					if(res.Status&&res.Message===''){
						uni.showToast({
							title:'摊销成功',
							icon:'none',
							duration:1000
						})
						self.modalName=''
						self.initData()
					}
				});
				console.log(this.sharelist)
			},
			deleteCostItem(index){
				var self=this
				uni.showModal({
					title: '提示',
					content: '确认删除该摊销费用吗？',
					success: function(res) {
						if (res.confirm) {
							self.sharelist.splice(index)
						} else if (res.cancel) {
							console.log('canceld');
						}
					}
				});
			},
			verifyPo(){
				var self=this
				var dto=this.baseRequestDto;
				dto.ReqBodyDTO=this.PoMasterInfo
				var result=true
				purchase.GetVerifyContent(dto).then(res => {
					console.log(res);
					result=res
				}).then(res=>{
					if(result.Status&&result.Message===''){
						
					}
				});
			},
			CancelverifyPo(){
				var self=this
				this.PoMasterInfo.appList = this.sharelist
				var dto=this.baseRequestDto;
				dto.ReqBodyDTO=this.PoMasterInfo
				purchase.CancelVerifyPo(dto).then(res => {
					console.log(res);
					if(res.Status&&res.Message===''){
						uni.showToast({
							title:'取消审核成功',
							icon:'none',
							duration:1000
						})
						self.modalName=''
						self.initData()
					}
				});
				
			},
			abandonPo(){
				var self=this
				this.PoMasterInfo.appList = this.sharelist
				var dto=this.baseRequestDto;
				dto.ReqBodyDTO=this.PoMasterInfo
				purchase.AbandonPo(dto).then(res => {
					console.log(res);
					if(res.Status&&res.Message===''){
						uni.showToast({
							title:'作废成功',
							icon:'none',
							duration:1000
						})
						self.modalName=''
						self.initData()
					}
				});
			},
			cancelabandonPo(){
				var self=this
				this.PoMasterInfo.appList = this.sharelist
				var dto=this.baseRequestDto;
				dto.ReqBodyDTO=this.PoMasterInfo
				purchase.CancelAbandonPo(dto).then(res => {
					console.log(res);
					if(res.Status&&res.Message===''){
						uni.showToast({
							title:'取消作废成功',
							icon:'none',
							duration:1000
						})
						self.modalName=''
						self.initData()
					}
				});
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
			},
			PickerPayTypeChange(e) {
				this.pickPayIndex = e.detail.value;
				this.PoMasterInfo.PayDateType = this.PayDateTypeList[this.pickPayIndex].value;
			},
			PickerSubjectChange(e) {
				this.shareCostData.subjectindex = e.detail.value;
				this.shareCostData.shareItem.ApportionSubjectSysNo = this.shareCostData.subject[e.detail.value].value
				this.shareCostData.shareItem.sname = this.shareCostData.subject[e.detail.value].name

			},
			PickerTypeChange(e) {
				this.shareCostData.typeindex = e.detail.value;
				this.shareCostData.shareItem.ApportionType = this.shareCostData.type[e.detail.value].value
				this.shareCostData.shareItem.tname = this.shareCostData.type[e.detail.value].name
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
