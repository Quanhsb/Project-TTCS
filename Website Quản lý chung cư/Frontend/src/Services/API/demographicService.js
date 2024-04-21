import axios from "../../setups/custom_axios";
const getListDemographic = () => {
    return axios.get("/nhan-khau/danh-sach-nhan-khau?");
}
const postDemographic = (data) => {
    const { hoTen, canCuocCongDan, ngaySinh, noiSinh, danToc, ngheNghiep, trangThai, quanHe, ghiChu } = data;
    return axios.post("/nhan-khau", {
        hoTen: hoTen,
        canCuocCongDan: canCuocCongDan,
        ngaySinh: ngaySinh,
        noiSinh: noiSinh,
        danToc: danToc,
        ngheNghiep: ngheNghiep,
        trangThai: trangThai,
        quanHe: quanHe,
        ghiChu: ghiChu,
    })
}
const getDemographic = (maNhanKhau) => {
    return axios.get(`/nhan-khau?maNhanKhau=${maNhanKhau}`)
}
const putDemographic = (maNhanKhau, data) => {
    const { hoTen, canCuocCongDan, ngaySinh, noiSinh, danToc, ngheNghiep, trangThai, quanHe, ghiChu, version } = data;
    return axios.put(`/nhan-khau?maNhanKhau=${maNhanKhau}`, {
        hoTen: hoTen,
        canCuocCongDan: canCuocCongDan,
        ngaySinh: ngaySinh,
        noiSinh: noiSinh,
        danToc: danToc,
        ngheNghiep: ngheNghiep,
        trangThai: trangThai,
        quanHe: quanHe,
        ghiChu: ghiChu,
        version: version,
    })
}
const deleteDemographic = (maNhanKhau, version) => {
    return axios.delete(`/nhan-khau?maNhanKhau=${maNhanKhau}&version=${version}`);
}
const demographicService = { getListDemographic, postDemographic, getDemographic, putDemographic, deleteDemographic };
export default demographicService;
