import axios from "../../setups/custom_axios";
const getListAbsent = () => {
    return axios.get("/tam-vang/danh-sach-tam-vang");
}
const postAbsent = (data) => {
    const { maNhanKhau, thoiHan, lyDo } = data;
    return axios.post("/tam-vang", {
        maNhanKhau: maNhanKhau,
        thoiHan: thoiHan,
        lyDo: lyDo,
    })
}
const getAbsent = (maTamVang) => {
    return axios.get(`/tam-vang?maTamVang=${maTamVang}`)
}
const putAbsent = (maTamVang, data) => {
    const { maNhanKhau, lyDo, thoiHan, version } = data;
    return axios.put(`/tam-vang?maTamVang=${maTamVang}`, {
        maNhanKhau: maNhanKhau,
        thoiHan: thoiHan,
        lyDo: lyDo,
        version: version,
    })
}
const deleteAbsent = (maTamVang, version) => {
    return axios.delete(`/tam-vang?maTamVang=${maTamVang}&version=${version}`);
}
const absentService = { getListAbsent, postAbsent, getAbsent, putAbsent, deleteAbsent };
export default absentService;
