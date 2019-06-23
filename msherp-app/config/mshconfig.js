/**
*env配置：
*development：开发环境
*production：生成环境
*/
const mshconfig = {
	devUrl: 'http://localhost/MshErp.WebApi/api/',
	productionUrl: 'https://api.mmsshh.com/api/',
	env: 'development',
	mshsessionid: "MSH_ERP_APP_TOKEN",
	mshdata_expirationName: 'mshdata_expiration',
	mshdata_expirationTime: Date.parse(new Date()) + 1800000,
	appstyle:'bg-gradual-green'
};
export default mshconfig;
