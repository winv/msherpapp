/**
*env配置：
*development：开发环境
*production：生成环境
* // devUrl: 'http://localhost/MshErp.WebApi/api/',
*/ 
const mshconfig = {
	//devUrl:'https://testwxservice.mmsshh.com/api/',
	devUrl: 'http://localhost/MshErp.WebApi/api/',
	productionUrl: 'http://localhost:809/api/',
	env: 'development',
	isDebug: true,
	isWriteData:false,
	mshsessionid: "MSH_ERP_APP_TOKEN",
	mshdata_expirationName: 'mshdata_expiration',
	mshdata_expirationTime: Date.parse(new Date()) + 1800000,
	appstyle:'bg-gradual-green'
};
export default mshconfig;
