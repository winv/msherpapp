import mshconfig from '../config/mshconfig.js'
let serverurl=mshconfig.env === "production" ? mshconfig.productionUrl : mshconfig.devUrl;
/**
 * @description 获取erp接口路径
 * @param path 接口地址
 * @author lee
 */
function getFullERPUrl(path) {
  if (path.indexOf('http') !== 0) {
      return `${serverurl}${path}`
    } else {
      return path
    }
}

export default {
  getFullERPUrl,
}
