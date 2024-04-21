import axios from "../../setups/custom_axios";
const getListRoom = async () => {
    return await axios.get(`/can-ho/danh-sach-can-ho`)
}
const getRoom = async (maCanHo) => {
    return await axios.get(`/can-ho?maCanHo=${maCanHo}`)
}
const addRoom = async ({ tenCanHo, tang, dienTich, maHoKhau, moTa }) => {
    return await axios.post(`/can-ho`, { tenCanHo, tang, dienTich, maHoKhau, moTa })
}
const updateRoom = async (maCanHo, { tenCanHo, tang, dienTich, maHoKhau, moTa, version }) => {
    return await axios.put(`/can-ho?maCanHo=${maCanHo}`, { tenCanHo, tang, dienTich, maHoKhau, moTa, version })
}
const deleteRoom = async (maCanHo, version) => {
    return await axios.delete(`/can-ho?maCanHo=${maCanHo}&version=${version}`)
}
const roomService = {
    getListRoom,
    getRoom,
    addRoom,
    updateRoom,
    deleteRoom
}
export default roomService;
