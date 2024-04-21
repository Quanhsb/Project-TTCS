import axios from "../../setups/custom_axios";
const getListTabernacle = () => {
    return axios.get("/tam-tru/danh-sach-tam-tru?");
}
const postTabernacle = (data) => {
    const { hoTen, diaChiThuongTru, diaChiTamTru, canCuocCongDan } = data;
    return axios.post("/tam-tru", {
        hoTen: hoTen,
        diaChiThuongTru: diaChiThuongTru,
        diaChiTamTru: diaChiTamTru,
        canCuocCongDan: canCuocCongDan,
    })
}
const getTabernacle = (maTamTru) => {
    return axios.get(`/tam-tru?maTamTru=${maTamTru}`)
}
const putTabernacle = (maTamTru, data) => {
    const { hoTen, diaChiThuongTru, diaChiTamTru, canCuocCongDan, version } = data;
    return axios.put(`/tam-tru?maTamTru=${maTamTru}`, {
        hoTen: hoTen,
        diaChiThuongTru: diaChiThuongTru,
        diaChiTamTru: diaChiTamTru,
        canCuocCongDan: canCuocCongDan,
        version: version,
    })
}
const deleteTabernacle = (maTamTru, version) => {
    return axios.delete(`/tam-tru?maTamTru=${maTamTru}&version=${version}`);
}
const tabernacleService = { getListTabernacle, postTabernacle, getTabernacle, putTabernacle, deleteTabernacle };
export default tabernacleService;
